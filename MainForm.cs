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

        private bool _addingRace = false;
        private bool _cellChanged = false;

        private const int DGV_RACE_COL_ID = 0;
        private const int DGV_RACE_COL_NAME = 1;
        
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
        }

        private void RemoveHandlers()
        {
            this.dgvRaces.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvRaces_CellValidating);
            this.dgvRaces.CellValueChanged -=
                new DataGridViewCellEventHandler(dgvRaces_CellValueChanged);
            this.dgvRaces.RowsAdded -= 
                new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvRaces_RowsAdded);
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

        #region event handlers
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
            _cellChanged = (oldValue != newValue);
            _logger.Info("Cell changed set to " + _cellChanged);
            
            _logger.CloseSection(location);
        }

        private void dgvRaces_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvRaces.Rows[e.RowIndex];

                /*
                Domain.Demon rowDemon = null;
                Domain.Demon databaseDemon = null;
                bool insertedNewDemon = false;

                using (var transaction = _dbSession.BeginTransaction())
                {
                    rowDemon = GetDemonFromDataGridViewRowDemonColumn(currentRow);
                    this.RemoveHandlers();
                    if (e.ColumnIndex == COLUMN_NAME) this.dgvDemons.Rows.Remove(currentRow);
                    if (rowDemon != null)
                    {
                        if (e.ColumnIndex == COLUMN_IN_PARTY)
                        {
                            rowDemon.InParty = Math.Abs(1 - rowDemon.InParty);
                            _dbSession.SaveOrUpdate(rowDemon);
                        }
                        else if (e.ColumnIndex == COLUMN_NAME)
                        {
                            if (rowDemon.UseInFusionCalculatorBoolean)
                            {
                                MessageBox.Show("Cannot have more than one " + rowDemon.Name + ".");
                            }
                            else
                            {
                                _logger.Info("Adding the following demon to fusion calculator: '" + rowDemon.ToString() + "'");
                                rowDemon.UseInFusionCalculator = 1;
                                _dbSession.SaveOrUpdate(rowDemon);
                                _logger.Info("Added to fusion calculator.");

                                _logger.Info("Updating interface...");
                                this.dgvDemons.Rows.Add(CreateRow(rowDemon));
                                _logger.Info("Updated.");
                            }
                        }
                        SetDataGridViewReadOnlyPropertyAndColors();
                    } // if (rowDemon != null)

                    this.AddHandlers();
                    transaction.Commit();
                } // using (var transaction = _dbSession.BeginTransaction())
                */
                if (_addingRace)
                {
                    var raceName = this.dgvRaces.Rows[this.dgvRaces.Rows.Count - 2].Cells[DGV_RACE_COL_NAME].Value.ToString();
                    var raceAlreadyExists = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>()
                        .Where(x => x.Name == raceName).ToList().Count > 0;

                    if (raceAlreadyExists)
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
