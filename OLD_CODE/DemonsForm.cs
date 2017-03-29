using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Feathers;

using NHibernate;

namespace Yatagarasu
{
    public partial class DemonsForm : Form
    {
        const int COLUMN_ID = 0;
        const int COLUMN_IN_PARTY = 1;
        const int COLUMN_LEVEL = 2;
        const int COLUMN_RACE = 3;
        const int COLUMN_NAME = 4;

        FeatherLogger _logger;
        ISession _dbSession;
        int _maxDemonId;
        bool _cellChanged = false;
        bool _addingRow = false;

        DataGridViewCheckBoxColumn colInParty;
        DataGridViewTextBoxColumn colId, colLevel, colRace, colName;

        public DemonsForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            InitializeColumnsAndStuff();

            LoadData();

            _logger.CloseSection(location);
        }

        private void InitializeColumnsAndStuff()
        {
            this.colId = new DataGridViewTextBoxColumn() { HeaderText = "Id", Name = "colId", Width = 40, ReadOnly = true };
            this.colInParty = new DataGridViewCheckBoxColumn { HeaderText = "InParty", Name = "colInParty", Width = 50 };
            this.colLevel = new DataGridViewTextBoxColumn() { HeaderText = "Level", Name = "colLevel", Width = 80, ReadOnly = true };
            this.colRace = new DataGridViewTextBoxColumn() { HeaderText = "Race", Name = "colRace", Width = 120, ReadOnly = true };
            this.colName = new DataGridViewTextBoxColumn() { HeaderText = "Name", Name = "colName", Width = 240 };

            this.dgvDemons.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dgvDemons.AllowUserToResizeRows = false;
            this.dgvDemons.RowTemplate.Height = 40;
            this.dgvDemons.RowTemplate.MinimumHeight = 40;
            this.dgvDemons.AllowUserToAddRows = true;
            
            this.colLevel.DefaultCellStyle =
                this.colRace.DefaultCellStyle =
                this.colName.DefaultCellStyle =
                GlobalObjects.GetDefaultDgvcStyle(16);

            this.colId.DefaultCellStyle =
                GlobalObjects.GetDefaultDgvcStyle(16, false);

            this.dgvDemons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.colId,
                this.colInParty,
				this.colLevel,
				this.colRace,
				this.colName});

            // for the eventual combobox
            // var racesList = new List<FeatherItem>();
            // _game.Races.ToList().ForEach(x => racesList.Add(new FeatherItem(x.Name, x)));
            // this.colRace.DataSource = racesList;
        }

        public void LoadData()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            RemoveHandlers();

            var game = GlobalObjects.CurrentGame;
            this.dgvDemons.Enabled = (game != null);
            if (game == null)
            {
                _logger.Info("No data to load; no game is chosen.");
                this.dgvDemons.Rows.Clear();
            }
            else
            {
                _logger.Info("Game '" + game.Name + "' chosen; will load list of demons for use in fusion calculator.");
                var allDemons = 
                    GlobalObjects.CurrentGame.Races
                    .SelectMany(x => x.Demons)
                    .Where(y => y.UseInFusionCalculator > 0)
                    .OrderBy(y => y.Level).ThenBy(z => z.Race.Id)
                    .ToList();
                _maxDemonId = allDemons.Select(x => x.Id).Max().GetValueOrDefault();
                foreach (Domain.Demon d in allDemons)
                {
                    _logger.Info("Loaded this demon: " + d.ToString());
                    this.dgvDemons.Rows.Add(CreateRow(d));
                }
                _logger.Info("Data load complete. Adding event handlers...");
                AddHandlers();
                SetDataGridViewReadOnlyPropertyAndColors();
                _logger.Info("Adding complete.");
            }

            _logger.CloseSection(location);
        }


        private void SetDataGridViewReadOnlyPropertyAndColors()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            for (int i = 0; i < this.dgvDemons.Rows.Count - 1; i++)
            {
                //this.dgvDemons.Rows[i].ReadOnly = true;

                var inParty = this.dgvDemons.Rows[i].Cells[COLUMN_IN_PARTY].Value;
                this.dgvDemons.Rows[i].DefaultCellStyle.BackColor = 
                    inParty != null && (bool)inParty ?
                    GlobalObjects.InPartyCell :
                    GlobalObjects.CannotEditCell;
                this.dgvDemons.Rows[i].Cells[COLUMN_ID].ReadOnly = true;
                this.dgvDemons.Rows[i].Cells[COLUMN_LEVEL].ReadOnly = true;
                this.dgvDemons.Rows[i].Cells[COLUMN_RACE].ReadOnly = true;
                this.dgvDemons.Rows[i].Cells[COLUMN_NAME].ReadOnly = true;
            }

            _logger.CloseSection(location);
        }
    
        private void RemoveHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating -= new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged -= new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
            this.dgvDemons.RowsAdded -= new DataGridViewRowsAddedEventHandler(this.dgvDemons_RowsAdded);
        }


        private void AddHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating += new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged += new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
            this.dgvDemons.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgvDemons_RowsAdded);
        }

        private object[] CreateRow(Domain.Demon d)
        {
            var returnObjectArray = new object[5];
            returnObjectArray[COLUMN_IN_PARTY] = (d.InParty > 0);
            returnObjectArray[COLUMN_ID] = d.Id;
            returnObjectArray[COLUMN_LEVEL] = d.Level;
            returnObjectArray[COLUMN_RACE] = d.Race.Name;
            returnObjectArray[COLUMN_NAME] = d.Name;

            return returnObjectArray;
        }


        private Domain.Demon GetDemonFromDataGridViewRowDemonColumn(DataGridViewRow dgvr)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            Domain.Demon returnDemon = null;

            object nameValue = dgvr.Cells[COLUMN_NAME].Value;

            _logger.Info("Cell contains name '" + nameValue + "'");

            if (nameValue != null)
            {
                string nameString = nameValue.ToString();
                var currentGameRaces = _dbSession.Get<Domain.Game>(GlobalObjects.CurrentGame.Id).Races;
                returnDemon = currentGameRaces.SelectMany(x => x.Demons).Where(y => y.Name == nameString).FirstOrDefault();
                _logger.Info(returnDemon == null ? "Could not find a demon." : "Found demon '" + returnDemon.ToString() + "'");
            }
            return returnDemon;
        }


        #region event handlers
        private void dgvDemons_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _addingRow = true;
        }


        private void dgvDemons_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvDemons.Rows[e.RowIndex];

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


        private void dgvDemons_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvDemons.Rows[e.RowIndex];

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
                        if (e.ColumnIndex == COLUMN_IN_PARTY )
                        {
                            rowDemon.InParty = Math.Abs(1 - rowDemon.InParty);
                            //_dbSession.SaveOrUpdate(rowDemon);
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

                if (_addingRow)
                {
                    _addingRow = false;
                    GlobalObjects.MainForm.UpdateFusions();
                }
                _logger.CloseSection(location);
            }
        }


        private void dgvDemons_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var demonToRemoveFromParty = GetDemonFromDataGridViewRowDemonColumn(e.Row);
            if (demonToRemoveFromParty != null)
            {
                using (var transaction = _dbSession.BeginTransaction())
                {
                    _logger.Info("Removing a demon from party: " + demonToRemoveFromParty.ToString());
                    demonToRemoveFromParty.InParty = 0;
                    demonToRemoveFromParty.UseInFusionCalculator = 0;
                    _dbSession.Update(demonToRemoveFromParty);
                    _logger.Info("Deleted.");
                    transaction.Commit();
                }
            }
        }


        private void PartyDemonsListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        #endregion

    }
}
