using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

using Feathers;

namespace Yatagarasu
{
    public partial class MainForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;

        private bool _addingDemon = false;
        private bool _cellDemonChanged = false;
        private bool _demonFusionStatusChanged = false;

        private bool _cellFusionChanged = false;

        private bool _addingRace = false;
        private bool _cellRaceChanged = false;

        private bool _needToRecalculateRowFusionIndexes = false;

        private const int DGV_RACE_COL_ID = 0;
        private const int DGV_RACE_COL_NAME = 1;

        private const int DGV_DEMON_COL_ID = 0;
        private const int DGV_DEMON_COL_FUSE = 1;
        private const int DGV_DEMON_COL_INPARTY = 2;
        private const int DGV_DEMON_COL_LEVEL = 3;
        private const int DGV_DEMON_COL_RACE = 4;
        private const int DGV_DEMON_COL_NAME = 5;

        private const int DGV_DEMONFUSION_COL_ID = 0;
        private const int DGV_DEMONFUSION_COL_LEVEL1 = 1;
        private const int DGV_DEMONFUSION_COL_RACE1 = 2;
        private const int DGV_DEMONFUSION_COL_NAME1 = 3;
        private const int DGV_DEMONFUSION_COL_LEVEL2 = 4;
        private const int DGV_DEMONFUSION_COL_RACE2 = 5;
        private const int DGV_DEMONFUSION_COL_NAME2 = 6;
        private const int DGV_DEMONFUSION_COL_LEVEL3 = 7;
        private const int DGV_DEMONFUSION_COL_RACE3 = 8;
        private const int DGV_DEMONFUSION_COL_NAME3 = 9;

        private const int IMPOSSIBLE_TO_FUSE_RACE = 0;

        private bool _changesWereDone = false;

        private const int DGV_FUSIONS_FONT_SIZE = 12;
        private const int DGV_DEMONS_FONT_SIZE = 12;

        private Dictionary<int, int> DataGridFusionRowIndexesPerFusionId;

        public MainForm()
        {
            InitializeComponent();

            ResetControlsTextAndStuff();

            _logger = GlobalObjects.Logger;
            _dbSession = GlobalObjects.DbSession;
        }

        private void ResetControlsTextAndStuff()
        {
            this.btnCurrentGame.Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (var chooseGameForm = new UserControls.ChooseGame())
            {
                chooseGameForm.ShowDialog();
                GlobalObjects.Exiting = chooseGameForm.exitWasChosen;
                GlobalObjects.CurrentGame = chooseGameForm.chosenGame;
            }

            if (GlobalObjects.Exiting)
            {
                Application.Exit();
            }
            else
            {
                var currentGame = GlobalObjects.CurrentGame;

                currentGame.demonsById = currentGame.Races.SelectMany(x => x.Demons)
                    .ToDictionary(d => d.Id);

                GlobalObjects.ImpossibleToFuseRace = _dbSession.Get<Domain.Race>(0);

                RemoveHandlers();

                UpdateGameLabel(currentGame.Name);
                UpdateRacesGrid();
                UpdateDemonsGrid();
                UpdateDemonFusionsGrid();

                RecalculateFusionRowIndexes();

                AddHandlers();
            }
        }

        private void AddHandlers()
        {
            this.dgvRaces.CellValidating +=
                new DataGridViewCellValidatingEventHandler(dgvRaces_CellValidating);
            this.dgvRaces.CellValueChanged +=
                new DataGridViewCellEventHandler(dgvRaces_CellValueChanged);
            this.dgvRaces.RowsAdded +=
                new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvRaces_RowsAdded);

            this.dgvDemons.CellValidating +=
                new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged +=
                new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.RowsAdded +=
                new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvDemons_RowsAdded);

            this.dgvFusions.CellValidating +=
                new DataGridViewCellValidatingEventHandler(dgvFusions_CellValidating);
            this.dgvFusions.CellValueChanged +=
                new DataGridViewCellEventHandler(dgvFusions_CellValueChanged);
            this.dgvFusions.Sorted += new EventHandler(dgvFusions_Sorted);

        }

        private void RemoveHandlers()
        {
            this.dgvRaces.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvRaces_CellValidating);
            this.dgvRaces.CellValueChanged -=
                new DataGridViewCellEventHandler(dgvRaces_CellValueChanged);
            this.dgvRaces.RowsAdded -= 
                new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvRaces_RowsAdded);

            this.dgvDemons.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged -=
                new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.RowsAdded -=
                new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvDemons_RowsAdded);

            this.dgvFusions.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvFusions_CellValidating);
            this.dgvFusions.CellValueChanged -=
                new DataGridViewCellEventHandler(dgvFusions_CellValueChanged);
            this.dgvFusions.Sorted -= new EventHandler(dgvFusions_Sorted);
        }

        private void dgvFusions_Sorted(object sender, EventArgs e)
        {
            _needToRecalculateRowFusionIndexes = true;
        }

        private void UpdateGameLabel(string gameName)
        {
            this.btnCurrentGame.Text = gameName;
            float fontSize = 150F / gameName.Length;
            this.btnCurrentGame.Font = new System.Drawing.Font("MS Mincho", fontSize, 
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void UpdateRacesGrid()
        {
            var allRaces = GlobalObjects.CurrentGame.Races;
            foreach(Domain.Race oneRace in allRaces)
            {
                var raceRow = new Object[2];
                raceRow[0] = oneRace.Id;
                raceRow[1] = oneRace.Name;
                this.dgvRaces.Rows.Add(raceRow);
            }
        }

        private void UpdateDemonsGrid()
        {
            var allDemons = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).OrderBy(y => y.Level).ThenBy(y => y.Name);

            foreach (Domain.Demon oneDemon in allDemons)
            {
                var demonRow = new Object[6];

                demonRow[DGV_DEMON_COL_ID] = oneDemon.Id;
                demonRow[DGV_DEMON_COL_FUSE] = oneDemon.IsFused;
                demonRow[DGV_DEMON_COL_INPARTY] = oneDemon.IsInParty;
                demonRow[DGV_DEMON_COL_LEVEL] = oneDemon.Level;
                demonRow[DGV_DEMON_COL_RACE] = oneDemon.Race.Name;
                demonRow[DGV_DEMON_COL_NAME] = oneDemon.Name;

                this.dgvDemons.Rows.Add(demonRow);
            }

            foreach(DataGridViewRow dr in this.dgvDemons.Rows)
            {
                dr.Cells[DGV_DEMON_COL_INPARTY].Style =
                dr.Cells[DGV_DEMON_COL_FUSE].Style =
                GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: true);

                dr.Cells[DGV_DEMON_COL_LEVEL].Style =
                    dr.Cells[DGV_DEMON_COL_RACE].Style =
                    dr.Cells[DGV_DEMON_COL_NAME].Style =
                    GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: true);

                dr.Cells[DGV_DEMON_COL_INPARTY].ReadOnly =
                    dr.Cells[DGV_DEMON_COL_FUSE].ReadOnly = false;
            }

        }



        private void UpdateDemonFusionsGrid()
        {
            try
            {
                this.dgvFusions.Rows.Clear();
                foreach (Domain.FusionDemon oneFusion in GlobalObjects.CurrentGame.FusionDemons)
                {
                    var demonFusionRow = new Object[12];

                    demonFusionRow[DGV_DEMONFUSION_COL_ID] = oneFusion.Id;

                    demonFusionRow[DGV_DEMONFUSION_COL_LEVEL1] = oneFusion.Demon1.Level;
                    demonFusionRow[DGV_DEMONFUSION_COL_RACE1] = oneFusion.Demon1.Race.Name;
                    demonFusionRow[DGV_DEMONFUSION_COL_NAME1] = oneFusion.Demon1.Name;

                    demonFusionRow[DGV_DEMONFUSION_COL_LEVEL2] = oneFusion.Demon2.Level;
                    demonFusionRow[DGV_DEMONFUSION_COL_RACE2] = oneFusion.Demon2.Race.Name;
                    demonFusionRow[DGV_DEMONFUSION_COL_NAME2] = oneFusion.Demon2.Name;

                    demonFusionRow[DGV_DEMONFUSION_COL_LEVEL3] =
                        oneFusion.Demon3 != null ? oneFusion.Demon3.Level.ToString() :
                        oneFusion.Level3 != null ? oneFusion.Level3.ToString() + "+" : "";
                    demonFusionRow[DGV_DEMONFUSION_COL_RACE3] = 
                        oneFusion.Demon3 != null ? oneFusion.Demon3.Race.Name.ToString() :
                        oneFusion.Race3 != null ? oneFusion.Race3.Name : "";
                    demonFusionRow[DGV_DEMONFUSION_COL_NAME3] = 
                        oneFusion.Demon3 != null ? oneFusion.Demon3.Name :
                        oneFusion.Race3 != null ? "?" : "";

                    var rowIndex = this.dgvFusions.Rows.Add(demonFusionRow);

                    if (!oneFusion.Demon1.IsFused || !oneFusion.Demon2.IsFused)
                    {
                        this.dgvFusions.Rows[rowIndex].Visible = false;
                    }

                    UpdateFusionRowDisplay(this.dgvFusions.Rows[rowIndex],
                        oneFusion.Demon1, oneFusion.Demon2, oneFusion.Demon3);
                }

                if (this.dgvFusions.Rows.Count > 0)
                {
                    this.dgvFusions.Sort(this.dgvFusions_Level1, ListSortDirection.Ascending);
                    this.dgvFusions.Sort(this.dgvFusions_Level3, ListSortDirection.Ascending);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_logger.Error(ex));
            }
            
        }

        private void UpdateFusionRowDisplay(
            DataGridViewRow currentFusionRow, Domain.Demon demon1, Domain.Demon demon2, Domain.Demon demon3)
        {
            currentFusionRow.Cells[DGV_DEMONFUSION_COL_ID].Style = GlobalObjects.GetDefaultDgvcStyle(DGV_FUSIONS_FONT_SIZE);

            currentFusionRow.Cells[DGV_DEMONFUSION_COL_LEVEL1].ReadOnly =
                currentFusionRow.Cells[DGV_DEMONFUSION_COL_RACE1].ReadOnly =
                currentFusionRow.Cells[DGV_DEMONFUSION_COL_NAME1].ReadOnly =
                currentFusionRow.Cells[DGV_DEMONFUSION_COL_LEVEL2].ReadOnly =
                currentFusionRow.Cells[DGV_DEMONFUSION_COL_RACE2].ReadOnly =
                currentFusionRow.Cells[DGV_DEMONFUSION_COL_NAME2].ReadOnly = true;

            currentFusionRow.Cells[DGV_DEMONFUSION_COL_LEVEL1].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_RACE1].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_NAME1].Style = GlobalObjects.GetDefaultDgvcStyle
                (fontSize: DGV_FUSIONS_FONT_SIZE, enabled: false, inParty: demon1.IsInParty);

            currentFusionRow.Cells[DGV_DEMONFUSION_COL_LEVEL2].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_RACE2].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_NAME2].Style = GlobalObjects.GetDefaultDgvcStyle
                (fontSize: DGV_FUSIONS_FONT_SIZE, enabled: false, inParty: demon2.IsInParty);

            currentFusionRow.Cells[DGV_DEMONFUSION_COL_LEVEL3].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_RACE3].Style =
              currentFusionRow.Cells[DGV_DEMONFUSION_COL_NAME3].Style = GlobalObjects.GetDefaultDgvcStyle (
                fontSize: DGV_FUSIONS_FONT_SIZE,
                enabled: demon3 == null,
                inParty: demon3 == null ? false : demon3.IsInParty);
        }

        private void RecalculateFusionRowIndexes()
        {
            DataGridFusionRowIndexesPerFusionId = new Dictionary<int, int>();
            for (int i = 0; i < this.dgvFusions.Rows.Count; i++)
            {
                var fusionId = Convert.ToInt32(this.dgvFusions.Rows[i].Cells[DGV_DEMONFUSION_COL_ID].Value);
                DataGridFusionRowIndexesPerFusionId.Add(fusionId, i);
            }
            _needToRecalculateRowFusionIndexes = false;
        }


        #region event handlers for demons
        private void dgvDemons_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvDemons.Rows[e.RowIndex];

            int numberOfNonNullColumns =
                (currentRow.Cells[DGV_DEMON_COL_LEVEL].Value == null ? 0 : 1) +
                (currentRow.Cells[DGV_DEMON_COL_RACE].Value == null ? 0 : 1) +
                (currentRow.Cells[DGV_DEMON_COL_NAME].Value == null ? 0 : 1) ;

            var oldValue = currentRow.Cells[e.ColumnIndex].Value;
            if (oldValue != null) oldValue = oldValue.ToString();
            var newValue = e.FormattedValue;
            if (newValue != null) newValue = newValue.ToString();            
            _logger.Info("oldValue = " + (oldValue == null ? "(null)" : "'" + oldValue.ToString() + "'"));
            _logger.Info("newValue = " + (newValue == null ? "(null)" : "'" + newValue.ToString() + "'"));

            _logger.Info("numberOfNonNullColumns = " + numberOfNonNullColumns);

            _cellDemonChanged = (numberOfNonNullColumns >= 2 && oldValue != newValue);

            _logger.Info("Cell changed set to " + _cellDemonChanged);
            _logger.CloseSection(location);
        }

        private void dgvDemons_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellDemonChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvDemons.Rows[e.RowIndex];
                
                if (!_addingDemon)
                {
                    var id = currentRow.Cells[DGV_DEMON_COL_ID].Value.ToString();
                    id = id + id;
                }
                else
                {
                    var raceName = this.dgvDemons.Rows[this.dgvDemons.Rows.Count - 2].Cells[DGV_DEMON_COL_RACE].Value.ToString();
                    var demonRace = GlobalObjects.CurrentGame.Races.Where(x => x.Name == raceName).FirstOrDefault();

                    if (demonRace == null)
                    {
                        MessageBox.Show("Race '" + raceName + "' does not exist.");
                        RemoveHandlers();
                        this.dgvDemons.Rows.RemoveAt(this.dgvDemons.Rows.Count - 2);
                        AddHandlers();
                        return;
                    }

                    var demonName = this.dgvDemons.Rows[this.dgvDemons.Rows.Count - 2].Cells[DGV_DEMON_COL_NAME].Value.ToString();
                    var demonExists = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons)
                        .Where(x => x.Name == demonName).FirstOrDefault() != null;

                    if (demonExists)
                    {
                        MessageBox.Show("This demon already exists.");
                        RemoveHandlers();
                        this.dgvRaces.Rows.RemoveAt(this.dgvRaces.Rows.Count - 2);
                        AddHandlers();
                        return;
                    }

                    int demonLevel = -1;
                    if (!Int32.TryParse(
                        this.dgvDemons.Rows[this.dgvDemons.Rows.Count - 2].Cells[DGV_DEMON_COL_LEVEL].Value.ToString(), out demonLevel))
                    {
                        MessageBox.Show("Invalid level.");
                        RemoveHandlers();
                        this.dgvRaces.Rows.RemoveAt(this.dgvRaces.Rows.Count - 2);
                        AddHandlers();
                        return;
                    }

                    try
                    {
                        using (var transaction = _dbSession.BeginTransaction())
                        {
                            if (!_changesWereDone)
                            {
                                foreach(DataGridViewRow dr in dgvDemons.Rows)
                                {
                                    dr.Cells[DGV_DEMON_COL_INPARTY].Style = 
                                        dr.Cells[DGV_DEMON_COL_FUSE].Style =
                                        GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: false);

                                    dr.Cells[DGV_DEMON_COL_LEVEL].Style =
                                        dr.Cells[DGV_DEMON_COL_RACE].Style =
                                        dr.Cells[DGV_DEMON_COL_NAME].Style =
                                        GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: true);

                                    dr.Cells[DGV_DEMON_COL_INPARTY].ReadOnly =
                                        dr.Cells[DGV_DEMON_COL_FUSE].ReadOnly = false;
                                }
                            }
                            _changesWereDone = true;

                            var allDemonsSoFar = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).ToList();
                            var fgg = allDemonsSoFar.Count();

                            var newDemon = new Domain.Demon(demonLevel, demonName, demonRace);
                            _dbSession.Save(newDemon);
                            demonRace.Demons.Add(newDemon);

                            foreach (Domain.Demon oneDemon in allDemonsSoFar)
                            {
                                var fusionRaceObject = GlobalObjects.CurrentGame.FusionRaces.Where
                                    (x => 
                                        (x.IdRace1 == oneDemon.Race.Id && x.IdRace2 == newDemon.Race.Id) ||
                                        (x.IdRace2 == oneDemon.Race.Id && x.IdRace1 == newDemon.Race.Id))
                                        .FirstOrDefault();

                                var resultDemon = fusionRaceObject == null ? null :
                                    FindDemonFromFusion(oneDemon, newDemon, fusionRaceObject).Item1;

                                var newFusion = newDemon.Level > oneDemon.Level ?
                                    new Domain.FusionDemon(GlobalObjects.CurrentGame.Id, oneDemon, newDemon, resultDemon) :
                                    new Domain.FusionDemon(GlobalObjects.CurrentGame.Id, newDemon, oneDemon, resultDemon);
                                GlobalObjects.CurrentGame.FusionDemons.Add(newFusion);
                            }

                            // Update fusions where the new demon might be the result of a previously unknown fusion
                            // e.g. fusion says Gouki 7+ unknown, then add Gouki 12 Kushi-mitama
                            var demonFusionsToUpdate = GlobalObjects.CurrentGame.FusionDemons.Where
                                (x => /*x.Demon3 == null && fgg: doit considérer le cas où quelqu'un insère 
                                       * le level 23 Gouki avant le level 12 Gouki, et là les fusions
                                       * 5+6 pointent vers le level 23*/ x.Race3 != null && x.Race3.Id == newDemon.Race.Id);
                            foreach(Domain.FusionDemon fusionToUpdate in demonFusionsToUpdate)
                            {
                                if (newDemon.Level >= fusionToUpdate.Level3)
                                {
                                    var fusionRaceObject = GlobalObjects.CurrentGame.FusionRaces.Where
                                    (x =>
                                        (x.IdRace1 == fusionToUpdate.Demon1.Race.Id && x.IdRace2 == fusionToUpdate.Demon2.Race.Id) ||
                                        (x.IdRace2 == fusionToUpdate.Demon1.Race.Id && x.IdRace1 == fusionToUpdate.Demon2.Race.Id))
                                        .FirstOrDefault();
                                    var demonFusionTuple = FindDemonFromFusion(fusionToUpdate.Demon1, fusionToUpdate.Demon2, fusionRaceObject);

                                    if (demonFusionTuple.Item1 == null)
                                    {
                                        fusionToUpdate.Demon3 = newDemon;
                                    }
                                    else if (fusionToUpdate.Demon3 != null && 
                                        demonFusionTuple.Item1!= null &&
                                        demonFusionTuple.Item1.Id != fusionToUpdate.Demon3.Id)
                                    {
                                        fusionToUpdate.Demon3 = newDemon;
                                    }
                                }
                                
                            }

                            RemoveHandlers();
                            this.dgvDemons.Rows[this.dgvDemons.Rows.Count - 2].Cells[DGV_DEMON_COL_ID].Value = newDemon.Id;
                            AddHandlers();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        throw;
                    }
                    
                    _addingDemon = false;
                }
                
            }

            if (_demonFusionStatusChanged)
            {
                var currentGame = GlobalObjects.CurrentGame;

                var currentRow = dgvDemons.Rows[dgvDemons.SelectedCells[0].RowIndex];

                var selectedDemonId = Convert.ToInt32(currentRow.Cells[DGV_DEMON_COL_ID].Value);
                var useInFusions = (bool)(currentRow.Cells[DGV_DEMON_COL_FUSE].Value);
                var fggcell = currentRow.Cells[DGV_DEMON_COL_INPARTY];

                var inParty = (bool?)(currentRow.Cells[DGV_DEMON_COL_INPARTY].Value) ?? false;

                var selectedDemon = currentGame.demonsById[selectedDemonId];

                try
                {
                    using (var transaction = _dbSession.BeginTransaction())
                    {
                        selectedDemon.IsFused = useInFusions;
                        selectedDemon.IsInParty = inParty;
                        _dbSession.Save(selectedDemon);
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    throw;
                }

                var affectedFusions = currentGame.FusionDemons.Where(
                    x => x.Demon1.Id == selectedDemonId || x.Demon2.Id == selectedDemonId || 
                        (x.Demon3 != null && x.Demon3.Id == selectedDemonId)).ToList();

                foreach (Domain.FusionDemon oneFusion in affectedFusions)
                {
                    var currentFusionRow = this.dgvFusions.Rows[DataGridFusionRowIndexesPerFusionId[oneFusion.Id]];
                    currentFusionRow.Visible = oneFusion.Demon1.IsFused && oneFusion.Demon2.IsFused;
                    UpdateFusionRowDisplay(currentFusionRow, oneFusion.Demon1, oneFusion.Demon2, oneFusion.Demon3);
                }
                _demonFusionStatusChanged = false;
            }
        }

        private void dgvDemons_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _addingDemon = true;
        }

        private void dgvDemons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDemons_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDemons.IsCurrentCellDirty)
            {
                Type cellType = dgvDemons.Columns[dgvDemons.SelectedCells[0].ColumnIndex].CellType;

                if (cellType == typeof(DataGridViewCheckBoxCell))
                {
                    var id = dgvDemons.Rows[dgvDemons.SelectedCells[0].RowIndex].Cells[DGV_DEMON_COL_ID].Value;
                    if (id != null)
                    {
                        _demonFusionStatusChanged = true;
                        dgvDemons.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
            }
        }
        #endregion

        #region event handlers for races
        private void dgvRaces_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvRaces.Rows[e.RowIndex];

            var oldValue = currentRow.Cells[e.ColumnIndex].Value;
            if (oldValue != null) oldValue = oldValue.ToString();
            var newValue = e.FormattedValue;
            if (newValue != null) newValue = newValue.ToString();

            _logger.Info("oldValue = " + (oldValue == null ? "(null)" : "'" + oldValue.ToString() + "'"));
            _logger.Info("newValue = " + (newValue == null ? "(null)" : "'" + newValue.ToString() + "'"));
            _cellRaceChanged = (oldValue != newValue);
            _logger.Info("Cell changed set to " + _cellRaceChanged);
            
            _logger.CloseSection(location);
        }

        private void dgvRaces_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellRaceChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvRaces.Rows[e.RowIndex];

                if (_addingRace)
                {
                    var raceName = this.dgvRaces.Rows[this.dgvRaces.Rows.Count - 2].Cells[DGV_RACE_COL_NAME].Value.ToString();

                    var raceExists = GlobalObjects.CurrentGame.Races.Where(x => x.Name == raceName).FirstOrDefault() != null;

                    if (raceExists)
                    {
                        MessageBox.Show("This race already exists.");
                        RemoveHandlers();
                        this.dgvRaces.Rows.RemoveAt(this.dgvRaces.Rows.Count - 2);
                        AddHandlers();
                    }
                    else
                    {
                        try
                        {
                            using (var transaction = _dbSession.BeginTransaction())
                            {
                                _changesWereDone = true;
                                var allRacesSoFar = GlobalObjects.CurrentGame.Races
                                    .Where(x => x.Id != IMPOSSIBLE_TO_FUSE_RACE).ToList();

                                var newRace = new Domain.Race(raceName, GlobalObjects.CurrentGame);
                                _dbSession.Save(newRace);
                                GlobalObjects.CurrentGame.Races.Add(newRace);

                                foreach (Domain.Race oneRace in allRacesSoFar)
                                {
                                    var newFusion = new Domain.FusionRace(
                                        GlobalObjects.CurrentGame.Id, oneRace.Id, newRace.Id, null);
                                    GlobalObjects.CurrentGame.FusionRaces.Add(newFusion);
                                }

                                RemoveHandlers();
                                this.dgvRaces.Rows[this.dgvRaces.Rows.Count - 2].Cells[DGV_RACE_COL_ID].Value = newRace.Id;
                                AddHandlers();
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                            throw;
                        }
                    }
                    
                    _addingRace = false;
                }
            }
        }

        private void dgvRaces_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _addingRace = true;
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changesWereDone)
            {
                var result = MessageBox.Show("Save changes?", "Pressing OK will quit and save changes.", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    SaveChanges(exiting: true);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }





        #region event handlers for fusions
        private void dgvFusions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvFusions.Rows[e.RowIndex];

            int numberOfNonNullColumns =
                (currentRow.Cells[DGV_DEMONFUSION_COL_LEVEL3].Value == null ? 0 : 1) +
                (currentRow.Cells[DGV_DEMONFUSION_COL_RACE3].Value == null ? 0 : 1) +
                (currentRow.Cells[DGV_DEMONFUSION_COL_NAME3].Value == null ? 0 : 1) ;

            var oldValue = currentRow.Cells[e.ColumnIndex].Value;
            if (oldValue != null) oldValue = oldValue.ToString();
            var newValue = e.FormattedValue;
            if (newValue != null) newValue = newValue.ToString();            
            _logger.Info("oldValue = " + (oldValue == null ? "(null)" : "'" + oldValue.ToString() + "'"));
            _logger.Info("newValue = " + (newValue == null ? "(null)" : "'" + newValue.ToString() + "'"));

            _logger.Info("numberOfNonNullColumns = " + numberOfNonNullColumns);

            _cellFusionChanged = (numberOfNonNullColumns >= 2 && oldValue != newValue);

            _logger.Info("Cell changed set to " + _cellFusionChanged);
            _logger.CloseSection(location);
        }

        private void dgvFusions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellFusionChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvFusions.Rows[e.RowIndex];

                var race1Name = currentRow.Cells[DGV_DEMONFUSION_COL_RACE1].Value.ToString();
                var race2Name = currentRow.Cells[DGV_DEMONFUSION_COL_RACE2].Value.ToString();
                var demon3Name = currentRow.Cells[DGV_DEMONFUSION_COL_NAME3].Value.ToString();

                if (!String.IsNullOrEmpty(demon3Name))
                {
                    var idFusion = currentRow.Cells[DGV_DEMONFUSION_COL_ID].Value.ToString();
                    var fusionObject = GlobalObjects.CurrentGame.FusionDemons
                        .Where(x => x.Id == Convert.ToInt32(idFusion)).FirstOrDefault();

                    var resultDemonObject = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons)
                        .Where(x => x.Name == demon3Name).FirstOrDefault();

                    if (resultDemonObject == null)
                    {
                        MessageBox.Show("This demon does not exist.");
                        return;
                    }

                    var raceObject1 = GlobalObjects.CurrentGame.Races.Where(x => x.Name == race1Name)
                        .FirstOrDefault();
                    var raceObject2 = GlobalObjects.CurrentGame.Races.Where(x => x.Name == race2Name)
                        .FirstOrDefault();
                    var raceObject3 = resultDemonObject.Race;

                    var fusionRaceObject = GlobalObjects.CurrentGame.FusionRaces.Where(x =>
                        (x.IdRace1 == raceObject1.Id && x.IdRace2 == raceObject2.Id) ||
                        (x.IdRace1 == raceObject2.Id && x.IdRace2 == raceObject1.Id))
                        .FirstOrDefault();
                    fusionRaceObject.IdRace3 = raceObject3.Id;

                    var fusionDemonsToUpdate = GlobalObjects.CurrentGame.FusionDemons.Where(x =>
                        (x.Demon1.Race.Id == raceObject1.Id && x.Demon2.Race.Id == raceObject2.Id) ||
                        (x.Demon1.Race.Id == raceObject2.Id && x.Demon2.Race.Id == raceObject1.Id)).ToList();

                    RemoveHandlers();

                    if (_needToRecalculateRowFusionIndexes)
                    {
                        RecalculateFusionRowIndexes();
                    }

                    // calulate fusions
                    foreach (var oneFusionDemon in fusionDemonsToUpdate)
                    {
                        var fusionTuple = 
                            FindDemonFromFusion(oneFusionDemon.Demon1, oneFusionDemon.Demon2, fusionRaceObject);

                        if (fusionTuple.Item1 != null)
                        {
                            oneFusionDemon.Demon3 = fusionTuple.Item1;
                        }
                        else
                        {
                            oneFusionDemon.Level3 = fusionTuple.Item2;
                            oneFusionDemon.Race3 = fusionTuple.Item3;
                        }

                        var gridIndex = this.DataGridFusionRowIndexesPerFusionId[oneFusionDemon.Id];
                        var fusionRowToUpdate = this.dgvFusions.Rows[gridIndex];

                        fusionRowToUpdate.Cells[DGV_DEMONFUSION_COL_LEVEL3].Value =
                            (fusionTuple.Item2 > 0 ? fusionTuple.Item2.ToString() : "");
                        fusionRowToUpdate.Cells[DGV_DEMONFUSION_COL_RACE3].Value =
                            fusionTuple.Item3.Name;
                        fusionRowToUpdate.Cells[DGV_DEMONFUSION_COL_NAME3].Value =
                            (oneFusionDemon.Demon3 != null ? oneFusionDemon.Demon3.Name : "?");

                        _changesWereDone = true;
                    }
                    AddHandlers();
                }
            }
        }
        #endregion


        private Tuple<Domain.Demon, int, Domain.Race> FindDemonFromFusion(
            Domain.Demon d1, Domain.Demon d2, Domain.FusionRace f)
        {
            // TODO rework
            Domain.Demon returnDemon = null;
            int returnLevel = -1;
            Domain.Race returnRace = GlobalObjects.ImpossibleToFuseRace;

            if (f != null)
            {
                if (f.IdRace3 == GlobalObjects.ImpossibleToFuseRace.Id)
                {
                    returnDemon = _dbSession.Get<Domain.Demon>(0);
                }
                else
                {
                    returnRace = GlobalObjects.CurrentGame.Races.Where(x => x.Id == f.IdRace3).FirstOrDefault();

                    returnDemon = GlobalObjects.CurrentGame.demonsById.Values
                        .Where(x =>
                        x.Race.Id == f.IdRace3 &&
                        x.Level > (d1.Level + d2.Level) / 2)
                    .OrderBy(x => x.Level)
                    .FirstOrDefault();

                    returnLevel = returnDemon != null ? returnDemon.Level : 
                        (int)Math.Ceiling((double)((int)(d1.Level + d2.Level) / 2));
                }

            }
            return new Tuple<Domain.Demon, int, Domain.Race>(returnDemon, returnLevel, returnRace);
        }


        private void SaveChanges(bool exiting = false)
        {
            using (var transaction = _dbSession.BeginTransaction())
            {
                foreach (var oneFusionRace in GlobalObjects.CurrentGame.FusionRaces)
                {
                    _dbSession.Save(oneFusionRace);
                }
                foreach (var oneFusionDemon in GlobalObjects.CurrentGame.FusionDemons)
                {
                    _dbSession.Save(oneFusionDemon);
                }
                _changesWereDone = false;
                transaction.Commit();
            }
            if (!exiting)
            {
                var currentGame = GlobalObjects.CurrentGame;
                currentGame.demonsById = currentGame.Races.SelectMany(x => x.Demons)
                    .ToDictionary(d => d.Id);
                UpdateDemonFusionsGrid();
                RecalculateFusionRowIndexes();

                foreach (DataGridViewRow dr in dgvDemons.Rows)
                {
                    dr.Cells[DGV_DEMON_COL_INPARTY].Style =
                        dr.Cells[DGV_DEMON_COL_FUSE].Style =
                        GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: true);

                    dr.Cells[DGV_DEMON_COL_LEVEL].Style =
                        dr.Cells[DGV_DEMON_COL_RACE].Style =
                        dr.Cells[DGV_DEMON_COL_NAME].Style =
                        GlobalObjects.GetDefaultDgvcStyle(fontSize: DGV_DEMONS_FONT_SIZE, enabled: true);

                    dr.Cells[DGV_DEMON_COL_INPARTY].ReadOnly =
                        dr.Cells[DGV_DEMON_COL_FUSE].ReadOnly = false;
                }
            }
            MessageBox.Show("Saved.");
        }

        private void btnCurrentGame_Click(object sender, EventArgs e)
        {
            if (_changesWereDone)
            {
                MessageBox.Show("Will save changes");
                SaveChanges();
            }
            else
            {
                MessageBox.Show("No changes to save.");
            }
        }



    }
}
