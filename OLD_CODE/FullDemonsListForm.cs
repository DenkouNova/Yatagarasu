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
    public partial class FullDemonsListForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;
        bool _cellChanged = false;

        DataGridViewCheckBoxColumn colUseInFusions, colInParty;
        DataGridViewTextBoxColumn colObject, colId, colLevel, colRace, colName;

        // Columns declared in the order the appear
        enum MyDataGridColumns
        {
            columnObject,
            columnId,
            columnUseInFusions,
            columnInParty,
            columnLevel,
            columnRace,
            columnName
        }

        public FullDemonsListForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            InitializeColumnsAndStuff();

            _logger.CloseSection(location);
        }


        private void InitializeColumnsAndStuff()
        {
            List<DataGridViewColumn> listOfColumns = new List<DataGridViewColumn>();

            this.colInParty = new DataGridViewCheckBoxColumn {
                HeaderText = "Party",
                Name = "colInParty",
                Width = 40,
                Tag = MyDataGridColumns.columnInParty
            };
            listOfColumns.Add(colInParty);

            this.colUseInFusions = new DataGridViewCheckBoxColumn {
                HeaderText = "Fuse",
                Name = "colUseInFusions",
                Width = 40,
                Tag = MyDataGridColumns.columnUseInFusions
            };
            listOfColumns.Add(colUseInFusions);

            this.colObject = new DataGridViewTextBoxColumn() {
                HeaderText = "Object",
                Name = "colObject",
                Visible = false,
                Tag = MyDataGridColumns.columnObject
            };
            listOfColumns.Add(colObject);

            this.colId = new DataGridViewTextBoxColumn() {
                HeaderText = "Id",
                Name = "colId",
                Width = 45,
                ReadOnly = true,
                Tag = MyDataGridColumns.columnId
            };
            listOfColumns.Add(colId);

            this.colLevel = new DataGridViewTextBoxColumn() {
                HeaderText = "Level",
                Name = "colLevel",
                Width = 55,
                Tag = MyDataGridColumns.columnLevel
            };
            listOfColumns.Add(colLevel);

            this.colRace = new DataGridViewTextBoxColumn() {
                HeaderText = "Race",
                Name = "colRace",
                Width = 85,
                Tag = MyDataGridColumns.columnRace
            };
            listOfColumns.Add(colRace);

            this.colName = new DataGridViewTextBoxColumn() {
                HeaderText = "Name",
                Name = "colName",
                Width = 250,
                Tag = MyDataGridColumns.columnName
            };
            listOfColumns.Add(colName);

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

            foreach (MyDataGridColumns oneColumnType in Enum.GetValues(typeof(MyDataGridColumns)))
                foreach(DataGridViewColumn oneColumnToAdd in listOfColumns)
                    if (((MyDataGridColumns)oneColumnToAdd.Tag) == oneColumnType)
                        this.dgvDemons.Columns.Add(oneColumnToAdd);

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
                _logger.Info("Game '" + game.Name + "' chosen; will load list of demons.");
                var allDemons = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).
                    OrderBy(y => y.Level).ThenBy(z => z.Race.Id).ToList();
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


        private Domain.Demon GetDemonFromDataGridViewRow(DataGridViewRow dgvr, bool canInsertRace)
        {
            Domain.Demon returnDemon = null;

            if (dgvr.Cells[(int)MyDataGridColumns.columnObject].Value != null)
            {
                // Selecting / updating demon
                returnDemon = (Domain.Demon)dgvr.Cells[(int)MyDataGridColumns.columnObject].Value;
            }
            else
            {
                // Inserting new demon
                object idValue = dgvr.Cells[(int)MyDataGridColumns.columnId].Value;
                object levelValue = dgvr.Cells[(int)MyDataGridColumns.columnLevel].Value;
                object nameValue = dgvr.Cells[(int)MyDataGridColumns.columnName].Value;
                object raceValue = dgvr.Cells[(int)MyDataGridColumns.columnRace].Value;

                if (levelValue != null && nameValue != null && raceValue != null)
                {
                    var actualDemonRace = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().
                        Where(x => x.Name == raceValue.ToString()).FirstOrDefault();
                    if (actualDemonRace == null && canInsertRace)
                        actualDemonRace = GlobalObjects.InsertRaceMaybe(raceValue.ToString());

                    if (actualDemonRace != null)
                    {
                        returnDemon = new Domain.Demon();
                        returnDemon.Level = Convert.ToInt32(levelValue.ToString());
                        returnDemon.Name = (string)nameValue;
                        returnDemon.Race = actualDemonRace;
                    }
                }
            }

            return returnDemon;
        }


        private object[] CreateRow(Domain.Demon d)
        {
            var returnObjectArray = new object[Enum.GetNames(typeof(MyDataGridColumns)).Length];
            returnObjectArray[(int)MyDataGridColumns.columnObject] = d;
            returnObjectArray[(int)MyDataGridColumns.columnId] = d.Id;
            returnObjectArray[(int)MyDataGridColumns.columnUseInFusions] = d.UseInFusionCalculatorBoolean;
            returnObjectArray[(int)MyDataGridColumns.columnInParty] = d.InPartyBoolean;
            returnObjectArray[(int)MyDataGridColumns.columnLevel] = d.Level;
            returnObjectArray[(int)MyDataGridColumns.columnRace] = d.Race.Name;
            returnObjectArray[(int)MyDataGridColumns.columnName] = d.Name;

            // TODO maybe:
            // do a select * on all races of the game
            // create a combobox with it
            // assign the combo box to object array 2
            // select the index of the correct race

            return returnObjectArray;
        }


        private void RemoveHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged -=
                new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow -=
                new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
        }


        private void AddHandlers()
        {
            _logger.Info(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            this.dgvDemons.CellValidating +=
                new DataGridViewCellValidatingEventHandler(dgvDemons_CellValidating);
            this.dgvDemons.CellValueChanged +=
                new DataGridViewCellEventHandler(dgvDemons_CellValueChanged);
            this.dgvDemons.UserDeletingRow +=
                new DataGridViewRowCancelEventHandler(this.dgvDemons_UserDeletingRow);
        }


        #region Handlers
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
                                if (e.ColumnIndex == (int)MyDataGridColumns.columnInParty)
                                {
                                    databaseDemon.InParty =
                                        (bool)this.dgvDemons.Rows[e.RowIndex].Cells[(int)MyDataGridColumns.columnInParty].Value ? 1 : 0;
                                }
                                else if (e.ColumnIndex == (int)MyDataGridColumns.columnUseInFusions)
                                {
                                    databaseDemon.UseInFusionCalculator =
                                        (bool)this.dgvDemons.Rows[e.RowIndex].Cells[(int)MyDataGridColumns.columnUseInFusions].Value ? 1 : 0;
                                }
                                _dbSession.SaveOrUpdate(databaseDemon); // update
                            }
                            transaction.Commit();
                            _logger.Info("Demon saved.");
                        }
                    }

                } // using (var transaction = _dbSession.BeginTransaction())

                var numberOfDemonsForFusions = 
                    _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().
                    SelectMany(x => x.Demons).Where(y => y.UseInFusionCalculatorBoolean).ToList();
                if (numberOfDemonsForFusions.Count == 2)
                {
                    GlobalObjects.MainForm.ForceUpdateFusions();
                }

                if (insertedNewDemon)
                {
                    RemoveHandlers();
                    currentRow.Cells[(int)MyDataGridColumns.columnObject].Value = databaseDemon;
                    currentRow.Cells[(int)MyDataGridColumns.columnId].Value = databaseDemon.Id;
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
