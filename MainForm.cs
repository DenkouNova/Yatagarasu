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
        
        public MainForm()
        {
            InitializeComponent();

            ResetControlsTextAndStuff();

            _logger = GlobalObjects.Logger;
            _dbSession = GlobalObjects.DbSession;
        }

        private void ResetControlsTextAndStuff()
        {
            this.lbCurrentGame.Text = "";
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
            this.lbCurrentGame.Text = gameName;
            float fontSize = 150F / gameName.Length;
            this.lbCurrentGame.Font = new System.Drawing.Font("MS Mincho", fontSize, 
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
                            var newDemon = new Domain.Demon(demonLevel, demonName, demonRace);
                            _dbSession.Save(newDemon);
                            demonRace.Demons.Add(newDemon);
                            RemoveHandlers();
                            this.dgvRaces.Rows[this.dgvRaces.Rows.Count - 2].Cells[DGV_RACE_COL_ID].Value = newDemon.Id;
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
                                var newRace = new Domain.Race(raceName, GlobalObjects.CurrentGame);
                                _dbSession.Save(newRace);
                                GlobalObjects.CurrentGame.Races.Add(newRace);
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

    }
}
