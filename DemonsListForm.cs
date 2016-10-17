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
    public partial class DemonsListForm : Form
    {
        const int COLUMN_ID = 0;
        const int COLUMN_LEVEL = 1;
        const int COLUMN_RACE = 2;
        const int COLUMN_NAME = 3;

        public enum DemonsListMode
        {
            AllDemons,
            PartyDemons
        }

        DemonsListMode _demonsListMode;
        FeatherLogger _logger;
        ISession _dbSession;
        int _maxDemonId;
        bool _cellChanged = false;

        DataGridViewTextBoxColumn colId, colLevel, colRace, colName;

        public DemonsListForm(DemonsListMode mode)
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name
                + "(" + _demonsListMode.ToString() + ")";
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            InitializeColumnsAndStuff();

            _demonsListMode = mode;
            if (_demonsListMode == DemonsListMode.AllDemons)
            {
                LoadData();
            }
            else
            {
                this.Text = "Party demons";
                this.Size = new Size(this.Size.Width, this.Size.Height / 3);
                AddHandlers();
            }  

            _logger.CloseSection(location);
        }


        private void InitializeColumnsAndStuff()
        {
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Id", Name = "colId", Width = 70, ReadOnly = true };
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Level", Name = "colLevel", Width = 80 };
            this.colRace = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Race", Name = "colRace", Width = 120 };
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Name", Name = "colName", Width = 240 };

            this.dgvDemons.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dgvDemons.AllowUserToResizeRows = false;
            this.dgvDemons.RowTemplate.Height = 40;
            this.dgvDemons.RowTemplate.MinimumHeight = 40;

            this.colLevel.DefaultCellStyle =
                this.colRace.DefaultCellStyle =
                this.colName.DefaultCellStyle =
                GlobalObjects.GetDefaultDgvcStyle(16);

            this.colId.DefaultCellStyle =
                GlobalObjects.GetDefaultDgvcStyle(16, false);

            this.dgvDemons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.colId,
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
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name
                + "(" + _demonsListMode.ToString() + ")";
            _logger.OpenSection(location);

            RemoveHandlers();

            var game = GlobalObjects.CurrentGame;
            this.dgvDemons.Enabled = (game != null);
            if (game == null)
            {
                _logger.Info("No data to load; no game game is chosen.");
                this.dgvDemons.Rows.Clear();
            }
            else
            {
                _logger.Info("Game '" + game.Name + "' chosen; will load list of demons.");
                var allDemons = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).
                OrderBy(y => y.Level).ThenBy(z => z.Race.Id);
                _maxDemonId = allDemons.Select(x => x.Id).Max().GetValueOrDefault();
                foreach (Domain.Demon d in allDemons)
                {
                    _logger.Info("Loaded this demon: " + d.ToString());
                    this.dgvDemons.Rows.Add(CreateRow(d));
                }
                _logger.Info("Data load complete. Adding event handlers...");
                AddHandlers();
                _logger.Info("Adding complete.");
            }
            _logger.CloseSection(location);
        }


        private void RemoveHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating -= new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged -= new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
        }


        private void AddHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating += new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged += new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
        }


        private object[] CreateRow(Domain.Demon d)
        {
            var returnObjectArray = new object[4];
            returnObjectArray[COLUMN_ID] = d.Id;
            returnObjectArray[COLUMN_LEVEL] = d.Level;
            returnObjectArray[COLUMN_RACE] = d.Race.Name;
            returnObjectArray[COLUMN_NAME] = d.Name;

            // TODO maybe:
            // do a select * on all races of the game
            // create a combobox with it
            // assign the combo box to object array 2
            // select the index of the correct race

            return returnObjectArray;
        }


        private Domain.Demon GetDemonFromDataGridViewRow(DataGridViewRow dgvr, bool canInsertRace)
        {
            Domain.Demon returnDemon = null;

            object idValue = dgvr.Cells[COLUMN_ID].Value;
            object levelValue = dgvr.Cells[COLUMN_LEVEL].Value;
            object nameValue = dgvr.Cells[COLUMN_NAME].Value;
            object raceValue = dgvr.Cells[COLUMN_RACE].Value;

            if (levelValue != null && nameValue != null && raceValue != null)
            {
                var actualDemonRace = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().
                    Where(x => x.Name == raceValue.ToString()).FirstOrDefault();
                if (actualDemonRace == null && canInsertRace)
                    actualDemonRace = GlobalObjects.InsertRaceMaybe(raceValue.ToString());

                if (actualDemonRace != null)
                {
                    if (idValue != null)
                    {
                        returnDemon = _dbSession.Get<Domain.Demon>((int)idValue);
                    }
                    else
                    {
                        returnDemon = new Domain.Demon();
                    }
                    returnDemon.Level = Convert.ToInt32(levelValue.ToString());
                    returnDemon.Name = (string)nameValue;
                    returnDemon.Race = actualDemonRace;
                }
            }
            return returnDemon;
        }




        #region event handlers
        private void dgvDemons_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name
                + "(" + _demonsListMode.ToString() + ")";
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
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name
                    + "(" + _demonsListMode.ToString() + ")";
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvDemons.Rows[e.RowIndex];

                Domain.Demon rowDemon = null;
                Domain.Demon databaseDemon = null;
                bool insertedNewDemon = false;

                using (var transaction = _dbSession.BeginTransaction())
                {
                    rowDemon = GetDemonFromDataGridViewRow(currentRow, true);
                    if (rowDemon != null)
                    {
                        databaseDemon = rowDemon;
                        bool proceedWithDatabaseOperation = true;
                        if (rowDemon.Id == null)
                        {
                            // ID is null but maybe the demon is already in the DB. Look in the database
                            _logger.Info("Looking in database if this demon already exists...");
                            databaseDemon = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().
                                SelectMany(x => x.Demons).Where(y => y.Name == rowDemon.Name).FirstOrDefault();
                            if (databaseDemon != null)
                            {
                                _logger.Info("Found demon ID " + databaseDemon.Id + ".");
                                if (databaseDemon.Equals(rowDemon))
                                {
                                    _logger.Info("Demons are exactly the same; no need to update DB.");
                                    proceedWithDatabaseOperation = false;
                                }
                            }
                            else
                            {
                                // if null just revert to the rowDemon again, we'll add that.
                                databaseDemon = rowDemon;
                            }
                        }

                        if (proceedWithDatabaseOperation)
                        {
                            _logger.Info("Row demon is " + rowDemon.ToString() + ". " +
                            (databaseDemon.Id == null ? "Inserting..." : "Updating..."));

                            if (databaseDemon.Id == null)
                            {
                                _dbSession.Save(databaseDemon); // insert
                                insertedNewDemon = true;
                            }
                            else
                            {
                                _dbSession.SaveOrUpdate(databaseDemon); // update
                            }
                            transaction.Commit();
                            _logger.Info("Demon saved.");
                        }
                    }

                } // using (var transaction = _dbSession.BeginTransaction())

                if (insertedNewDemon ||
                    (databaseDemon != null &&
                        _demonsListMode == DemonsListMode.PartyDemons &&
                        currentRow.Cells[COLUMN_ID].Value == null))
                {
                    RemoveHandlers();
                    currentRow.Cells[COLUMN_ID].Value = databaseDemon.Id;
                    AddHandlers();
                }

                _logger.CloseSection(location);
            }
        }


        private void dgvDemons_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var deletedDemon = GetDemonFromDataGridViewRow(e.Row, false);
            if (deletedDemon != null)
            {
                using (var transaction = _dbSession.BeginTransaction())
                {
                    _logger.Info("Deleting demon: " + deletedDemon.ToString());
                    _dbSession.Delete(deletedDemon);
                    _logger.Info("Deleted.");
                    transaction.Commit();
                }
            }
        }


        private void DemonsListForm_FormClosing(object sender, FormClosingEventArgs e)
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
