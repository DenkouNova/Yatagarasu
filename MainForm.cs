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

        private bool _addingRace = false;
        private bool _cellRaceChanged = false;

        private const int DGV_RACE_COL_ID = 0;
        private const int DGV_RACE_COL_NAME = 1;

        private const int DGV_DEMON_COL_ID = 0;
        private const int DGV_DEMON_COL_LEVEL = 1;
        private const int DGV_DEMON_COL_RACE = 2;
        private const int DGV_DEMON_COL_NAME = 3;

        private const int IMPOSSIBLE_TO_FUSE_RACE = 0;

        private bool _changesWereDone = false;

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
                UpdateGameLabel(currentGame.Name);
                UpdateRacesGrid();
                UpdateDemonsGrid();
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
            RemoveHandlers();
            var allRaces = GlobalObjects.CurrentGame.Races;
            foreach(Domain.Race oneRace in allRaces)
            {
                var raceRow = new Object[2];
                raceRow[0] = oneRace.Id;
                raceRow[1] = oneRace.Name;
                this.dgvRaces.Rows.Add(raceRow);
            }
            AddHandlers();
        }

        private void UpdateDemonsGrid()
        {
            RemoveHandlers();
            var allDemons = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).OrderBy(y => y.Level).ThenBy(y => y.Name);

            foreach (Domain.Demon oneDemon in allDemons)
            {
                var demonRow = new Object[4];

                demonRow[DGV_DEMON_COL_ID] = oneDemon.Id;
                demonRow[DGV_DEMON_COL_LEVEL] = oneDemon.Level;
                demonRow[DGV_DEMON_COL_RACE] = oneDemon.Race.Name;
                demonRow[DGV_DEMON_COL_NAME] = oneDemon.Name;

                this.dgvDemons.Rows.Add(demonRow);
            }
            AddHandlers();
        }







        /*
         * this.colLevel.DefaultCellStyle =
                this.colRace.DefaultCellStyle =
                this.colName.DefaultCellStyle =
                GlobalObjects.GetDefaultDgvcStyle(16);*/


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
                
                if (_addingDemon)
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
                    var demonExists = demonRace.Demons.Where(x => x.Name == demonName).FirstOrDefault() != null; // TODO search through all races

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
                            _changesWereDone = true;

                            var allDemonsSoFar = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons);

                            var newDemon = new Domain.Demon(demonLevel, demonName, demonRace);
                            _dbSession.Save(newDemon);
                            demonRace.Demons.Add(newDemon);

                            foreach (Domain.Demon oneDemon in allDemonsSoFar)
                            {
                                var newFusion = new Domain.FusionDemon(
                                    GlobalObjects.CurrentGame.Id, oneDemon.Id, newDemon.Id, null);
                                GlobalObjects.CurrentGame.FusionDemons.Add(newFusion);
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
        }

        private void dgvDemons_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _addingDemon = true;
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
                    SaveChanges();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveChanges()
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
