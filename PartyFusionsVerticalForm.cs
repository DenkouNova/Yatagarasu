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
    public partial class PartyFusionsVerticalForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;

        // Columns declared in the order the appear
        enum MyDataGridColumns
        {
            colFusionObject,

            colId1,
            colLevel1,
            colRaceObject1,
            colRace1,
            colName1,

            colId2,
            colLevel2,
            colRaceObject2,
            colRace2,
            colName2,

            colId3,
            colLevel3,
            colRace3,
            colName3
        }

        bool _cellChanged = false;

        DataGridViewTextBoxColumn colFusionObject,
            colId1, colLevel1, colRaceObject1, colRace1, colName1,
            colId2, colLevel2, colRaceObject2, colRace2, colName2,
            colId3, colLevel3, colRace3, colName3;

        public PartyFusionsVerticalForm()
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
            List<DataGridViewColumn> listOfColumns = new List<DataGridViewColumn>();

            this.colFusionObject = new DataGridViewTextBoxColumn()
            {
                HeaderText = "FusionObject",
                Name = "colFusionObject",
                Visible = false,
                Tag = MyDataGridColumns.colFusionObject
            };
            listOfColumns.Add(colFusionObject);

            this.colId1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id1",
                Name = "colId1",
                Visible = false,
                Tag = MyDataGridColumns.colId1
            };
            this.colLevel1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Level1",
                Name = "colLevel1",
                Width = 45,
                ReadOnly = true,
                Tag = MyDataGridColumns.colLevel1
            };
            this.colRaceObject1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "RaceObject1",
                Name = "colRaceObject1",
                Visible = false,
                Tag = MyDataGridColumns.colRaceObject1
            };
            this.colRace1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Race1",
                Name = "colRace1",
                Width = 90,
                ReadOnly = true,
                Tag = MyDataGridColumns.colRace1
            };
            this.colName1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Name1",
                Name = "colName1",
                Width = 180,
                ReadOnly = true,
                Tag = MyDataGridColumns.colName1
            };
            listOfColumns.AddRange(new DataGridViewColumn[] {
                colId1, colLevel1, colRaceObject1, colRace1, colName1 });


            this.colId2 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id2",
                Name = "colId2",
                Visible = false,
                Tag = MyDataGridColumns.colId2
            };
            this.colLevel2 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Level2",
                Name = "colLevel2",
                Width = 45,
                ReadOnly = true,
                Tag = MyDataGridColumns.colLevel2
            };
            this.colRaceObject2 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "RaceObject2",
                Name = "colRaceObject2",
                Visible = false,
                Tag = MyDataGridColumns.colRaceObject2
            };
            this.colRace2 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Race2",
                Name = "colRace2",
                Width = 90,
                ReadOnly = true,
                Tag = MyDataGridColumns.colRace2
            };
            this.colName2 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Name2",
                Name = "colName2",
                Width = 180,
                ReadOnly = true,
                Tag = MyDataGridColumns.colName2
            };
            listOfColumns.AddRange(new DataGridViewColumn[] {
                colId2, colLevel2, colRaceObject2, colRace2, colName2 });


            this.colId3 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id3",
                Name = "colId3",
                Visible = false,
                Tag = MyDataGridColumns.colId3
            };
            this.colLevel3 = new DataGridViewTextBoxColumn() {
                HeaderText = "Level3",
                Name = "colLevel3",
                Width = 45,
                ReadOnly = false,
                Tag = MyDataGridColumns.colLevel3
            };
            this.colRace3 = new DataGridViewTextBoxColumn() {
                HeaderText = "Race3",
                Name = "colRace3",
                Width = 90,
                ReadOnly = false,
                Tag = MyDataGridColumns.colRace3
            };
            this.colName3 = new DataGridViewTextBoxColumn() {
                HeaderText = "Name3",
                Name = "colName3",
                Width = 180,
                ReadOnly = false,
                Tag = MyDataGridColumns.colName3
            };
            listOfColumns.AddRange(new DataGridViewColumn[] { colId3, colLevel3, colRace3, colName3 });

            this.dgvPartyFusions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dgvPartyFusions.AllowUserToResizeRows = false;
            this.dgvPartyFusions.RowTemplate.Height = 40;
            this.dgvPartyFusions.RowTemplate.MinimumHeight = 40;
            this.dgvPartyFusions.AllowUserToAddRows = false;

            this.colLevel1.DefaultCellStyle = this.colLevel2.DefaultCellStyle =
                this.colLevel3.DefaultCellStyle = this.colRace1.DefaultCellStyle =
                this.colRace2.DefaultCellStyle = this.colRace3.DefaultCellStyle =
                this.colName1.DefaultCellStyle = this.colName2.DefaultCellStyle =
                this.colName3.DefaultCellStyle = GlobalObjects.GetDefaultDgvcStyle(16);

            foreach (MyDataGridColumns oneColumnType in Enum.GetValues(typeof(MyDataGridColumns)))
                foreach (DataGridViewColumn oneColumnToAdd in listOfColumns)
                    if (((MyDataGridColumns)oneColumnToAdd.Tag) == oneColumnType)
                        this.dgvPartyFusions.Columns.Add(oneColumnToAdd);
        }


        public void LoadData()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            RemoveHandlers();

            var game = GlobalObjects.CurrentGame;
            this.dgvPartyFusions.Enabled = (game != null);
            if (game == null)
            {
                _logger.Info("No data to load; no game is chosen.");
                this.dgvPartyFusions.Rows.Clear();
            }
            else
            {
                _logger.Info("Game '" + game.Name + "' chosen; will load fusions for demons.");
                this.dgvPartyFusions.Rows.Clear();
                var demonsForFusions = GlobalObjects.CurrentGame.Races.SelectMany(x => x.Demons).
                    Where(y => y.UseInFusionCalculatorBoolean).ToList();
                for (int i = 0; i < demonsForFusions.Count; i++)
                {
                    for (int j = i+1; j < demonsForFusions.Count; j++)
                    {
                        if (j < demonsForFusions.Count)
                        {
                            _logger.Info("First demon for fusion: " + demonsForFusions[i].ToString());
                            _logger.Info("Second demon for fusion: " + demonsForFusions[j].ToString());
                            var oneFusion = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                                .Where(x =>
                                    (x.IdRace1 == demonsForFusions[i].Race.Id &&
                                        x.IdRace2 == demonsForFusions[j].Race.Id) ||
                                    (x.IdRace2 == demonsForFusions[i].Race.Id &&
                                        x.IdRace1 == demonsForFusions[j].Race.Id))
                                    .SingleOrDefault();
                            var oneFusionObject = new FusionObject
                                (demonsForFusions[i], demonsForFusions[j], oneFusion);
                            this.dgvPartyFusions.Rows.Add(this.CreateRow(oneFusionObject));

                            var formattedRow = this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1];

                            ChangeCellColorsBasedOnParty(oneFusionObject.Demon1,
                                new DataGridViewCell[] {
                                    formattedRow.Cells[(int)MyDataGridColumns.colLevel1],
                                     formattedRow.Cells[(int)MyDataGridColumns.colRace1],
                                      formattedRow.Cells[(int)MyDataGridColumns.colName1]});

                            ChangeCellColorsBasedOnParty(oneFusionObject.Demon2,
                                new DataGridViewCell[] {
                                    formattedRow.Cells[(int)MyDataGridColumns.colLevel2],
                                     formattedRow.Cells[(int)MyDataGridColumns.colRace2],
                                      formattedRow.Cells[(int)MyDataGridColumns.colName2]});

                            ChangeCellColorsBasedOnParty(oneFusionObject.Demon3,
                                new DataGridViewCell[] {
                                    formattedRow.Cells[(int)MyDataGridColumns.colLevel3],
                                     formattedRow.Cells[(int)MyDataGridColumns.colRace3],
                                      formattedRow.Cells[(int)MyDataGridColumns.colName3]});
                        }
                        
                    }
                }
                ReorderTable();
            }
            this.dgvPartyFusions.ClearSelection();
            _logger.Info("Adding complete.");
            AddHandlers();
            _logger.CloseSection(location);
        }


        private void ChangeCellColorsBasedOnParty(Domain.Demon demon, DataGridViewCell[] cells)
        {
            if (demon != null)
                foreach (DataGridViewCell oneCell in cells)
                    oneCell.Style.BackColor =
                        demon.InPartyBoolean ? GlobalObjects.InPartyCell : GlobalObjects.DefaultCell;
        }

        private object[] CreateRow(FusionObject fo)
        {
            var returnObjectArray = new object[Enum.GetNames(typeof(MyDataGridColumns)).Length];
            returnObjectArray[(int)MyDataGridColumns.colFusionObject] = fo;

            returnObjectArray[(int)MyDataGridColumns.colId1] = fo.Demon1.Id;
            returnObjectArray[(int)MyDataGridColumns.colLevel1] = fo.Demon1.Level;
            returnObjectArray[(int)MyDataGridColumns.colRaceObject1] = fo.Demon1.Race;
            returnObjectArray[(int)MyDataGridColumns.colRace1] = fo.Demon1.Race.Name;
            returnObjectArray[(int)MyDataGridColumns.colName1] = fo.Demon1.Name;

            returnObjectArray[(int)MyDataGridColumns.colId2] = fo.Demon2.Id;
            returnObjectArray[(int)MyDataGridColumns.colLevel2] = fo.Demon2.Level;
            returnObjectArray[(int)MyDataGridColumns.colRaceObject2] = fo.Demon2.Race;
            returnObjectArray[(int)MyDataGridColumns.colRace2] = fo.Demon2.Race.Name;
            returnObjectArray[(int)MyDataGridColumns.colName2] = fo.Demon2.Name;

            returnObjectArray[(int)MyDataGridColumns.colId3] = 
                fo.Demon3 == null ? "" : fo.Demon3.Id.ToString();
            returnObjectArray[(int)MyDataGridColumns.colLevel3] =
                fo.FusionIsImpossible ? "-" : 
                fo.Demon3 == null ? "" : fo.Demon3.Level.ToString();
            returnObjectArray[(int)MyDataGridColumns.colRace3] =
                fo.FusionIsImpossible ? "-" : 
                fo.Demon3 == null ? "" : fo.Demon3.Race.Name;
            returnObjectArray[(int)MyDataGridColumns.colName3] =
                fo.FusionIsImpossible ? "-" : 
                fo.Demon3 == null ? "?" : fo.Demon3.Name;

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

            if (!String.IsNullOrEmpty(dgvr.Cells[(int)MyDataGridColumns.colId3].Value.ToString()))
            {
                // Selecting / updating demon
                MessageBox.Show("UPDATING fusions is not implemented. Please refresh window to reload data.");
                returnDemon = null;
            }
            else if (((FusionObject)dgvr.Cells[(int)MyDataGridColumns.colFusionObject].Value).FusionIsImpossible)
            {
                MessageBox.Show("UPDATING fusions from impossible to possible is not implemented. " +
                    "Please refresh window to reload data.");
                returnDemon = null;
            }
            else
            {
                // Inserting new demon
                object nameValue = dgvr.Cells[(int)MyDataGridColumns.colName3].Value;

                if (nameValue != null)
                {
                    returnDemon = GetDemonFromDataGridViewRowDemonColumn(dgvr);
                }
                else
                {
                    object idValue = dgvr.Cells[(int)MyDataGridColumns.colId3].Value;
                    object levelValue = dgvr.Cells[(int)MyDataGridColumns.colLevel3].Value;
                    object raceValue = dgvr.Cells[(int)MyDataGridColumns.colRace3].Value;

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
            }

            return returnDemon;
        }


        private Domain.Demon GetDemonFromDataGridViewRowDemonColumn(DataGridViewRow dgvr)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            Domain.Demon returnDemon = null;

            object nameValue = dgvr.Cells[(int)MyDataGridColumns.colName3].Value;

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


        public void ReorderTable()
        {
            if (this.dgvPartyFusions.Rows.Count > 0)
            {
                this.dgvPartyFusions.Sort(colName3, ListSortDirection.Ascending);
                this.dgvPartyFusions.Sort(colLevel3, ListSortDirection.Descending);
            }
        }


        private void AddHandlers()
        {
            this.dgvPartyFusions.SelectionChanged += 
                new EventHandler(this.dgvPartyFusions_SelectionChanged);
            this.dgvPartyFusions.CellValidating += 
                new DataGridViewCellValidatingEventHandler(dgvPartyFusions_CellValidating);
            this.dgvPartyFusions.CellValueChanged += 
                new DataGridViewCellEventHandler(dgvPartyFusions_CellValueChanged);
            
        }


        private void RemoveHandlers()
        {
            this.dgvPartyFusions.SelectionChanged -=
                new EventHandler(this.dgvPartyFusions_SelectionChanged);
            this.dgvPartyFusions.CellValidating -=
                new DataGridViewCellValidatingEventHandler(dgvPartyFusions_CellValidating);
            this.dgvPartyFusions.CellValueChanged -= 
                new DataGridViewCellEventHandler(dgvPartyFusions_CellValueChanged);
        }


        private void dgvPartyFusions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvPartyFusions.Rows[e.RowIndex];

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

        private void dgvPartyFusions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);
                
                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.dgvPartyFusions.Rows[e.RowIndex];
                
                Domain.Demon rowDemon = null;
                Domain.Demon databaseDemon = null;
                bool insertedNewDemon = false;

                // THIS CODE IS COPIED FROM THE DEMONS LIST FORM
                // FIXME: REFACTOR / DO SOMETHING BETTER
                using (var transaction = _dbSession.BeginTransaction())
                {
                    rowDemon = GetDemonFromDataGridViewRow(currentRow, true);
                    if (rowDemon != null)
                    {
                        databaseDemon = rowDemon;
                        bool insertDemon = true;
                        bool insertFusion = true;
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
                                }
                            }
                            else
                            {
                                insertDemon = true;
                                databaseDemon = rowDemon;
                            }
                        }


                        if (insertDemon)
                        {
                            _logger.Info("Inserting new demon...");

                            // TODO: UPDATING
                            _logger.Info("Row demon is " + rowDemon.ToString() + ". " +
                                (databaseDemon.Id == null ? "Inserting..." : "Updating..."));

                            if (databaseDemon.Id == null)
                            {
                                _dbSession.Save(databaseDemon); // insert
                                insertedNewDemon = true;
                            }

                            _logger.Info("Demon saved (for now; need to commit).");
                        }

                        if (insertFusion)
                        {
                            _logger.Info("Inserting new fusion...");
                            Domain.Fusion f = new Domain.Fusion(
                                (Domain.Race)currentRow.Cells[(int)MyDataGridColumns.colRaceObject1].Value,
                                (Domain.Race)currentRow.Cells[(int)MyDataGridColumns.colRaceObject2].Value,
                                databaseDemon.Race);
                            _dbSession.Save(f);

                            _logger.Info("Fusion saved (for now; need to commit).");
                        }

                        if (insertDemon || insertFusion)
                        {
                            try
                            {
                                _logger.Info("Committing...");
                                transaction.Commit();
                                _logger.Info("Transaction complete.");
                                LoadData();
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex);
                                MessageBox.Show("ERROR: " + ex.Message +
                                    ex.InnerException == null ? "" : "\r\n" + ex.InnerException.Message);
                            }
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
                    currentRow.Cells[(int)MyDataGridColumns.colId3].Value = databaseDemon.Id;
                    AddHandlers();
                }
                // END OF COPIED CODE
                // FIXME: REFACTOR / DO SOMETHING BETTER

                _logger.CloseSection(location);
            }
        }

        private void dgvPartyFusions_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvPartyFusions.SelectedRows.Count == this.dgvPartyFusions.Rows.Count)
            {
                LoadData();
            }

        }

    }
}
