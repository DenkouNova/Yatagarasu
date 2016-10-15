using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

using Feathers;
using Yatagarasu.Domain;

using NHibernate;

namespace Yatagarasu
{
    class DemonsDataGridView : DataGridView
    {
        const int COLUMN_ID = 0;
        const int COLUMN_LEVEL = 1;
        const int COLUMN_RACE = 2;
        const int COLUMN_NAME = 3;


        FeatherLogger _logger;
        ISession _dbSession;
        Domain.Game _game;
        int _maxDemonId;
        bool _cellChanged = false;

        DataGridViewTextBoxColumn colId, colLevel, colRace, colName;

        public DemonsDataGridView() : base()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _dbSession = GlobalObjects.DbSession;
            _game = GlobalObjects.CurrentGame;

            InitializeColumnsAndStuff();

            // load data
            var allDemons = _game.Races.SelectMany(x => x.Demons).
                OrderBy(y => y.Level).ThenBy(z => z.Race.Pronunciation);
            _maxDemonId = allDemons.Select(x => x.Id).Max().GetValueOrDefault();
            foreach(Demon d in allDemons)
            {
                _logger.Info(d.Name);
                this.Rows.Add(CreateRow(d));
            }
            this.CellValidating += new DataGridViewCellValidatingEventHandler(this_CellValidating);
            this.CellValueChanged += new DataGridViewCellEventHandler(this_CellValueChanged);
            _logger.CloseSection(location);
        }


        private Demon GetDemonFromDataGridViewRow(DataGridViewRow dgvr)
        {
            Demon returnDemon = null;

            object idValue = dgvr.Cells[COLUMN_ID].Value;
            object levelValue = dgvr.Cells[COLUMN_LEVEL].Value;
            object nameValue = dgvr.Cells[COLUMN_NAME].Value;
            object raceValue = dgvr.Cells[COLUMN_RACE].Value;

            if (levelValue != null && nameValue != null && raceValue != null)
            {
                var actualDemonRace = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().
                    Where(x => x.Name == raceValue.ToString()).FirstOrDefault();
                if (actualDemonRace != null)
                {
                    if (idValue != null)
                    {
                        returnDemon = _dbSession.Get<Domain.Demon>((int)idValue);
                    }
                    else
                    {
                        returnDemon = new Demon();
                    }
                    returnDemon.Level = Convert.ToInt32(levelValue.ToString());
                    returnDemon.Name = (string)nameValue;
                    returnDemon.Race = actualDemonRace;
                }
            }
            return returnDemon;
        }

        private object[] CreateRow(Demon d)
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var returnObjectArray = new object[4];
            returnObjectArray[COLUMN_ID] = d.Id;
            returnObjectArray[COLUMN_LEVEL] = d.Level;
            returnObjectArray[COLUMN_RACE] = d.Race.Name;
            returnObjectArray[COLUMN_NAME] = d.Name;

            _logger.CloseSection(location);

            // TODO:
            // do a select * on all races of the game
            // create a combobox with it
            // assign the combo box to object array 2
            // select the index of the correct race

            return returnObjectArray;
        }

        private object[] CreateEmptyRow()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.OpenSection(location);

            _maxDemonId++;
            var returnObjectArray = new object[4];
            returnObjectArray[COLUMN_ID] = _maxDemonId;
            returnObjectArray[COLUMN_LEVEL] = null;
            returnObjectArray[COLUMN_RACE] = null;
            returnObjectArray[COLUMN_NAME] = null;

            _logger.CloseSection(location);

            // TODO:
            // do a select * on all races of the game
            // create a combobox with it
            // assign the combo box to object array 2
            // select the index of the correct race

            return returnObjectArray;
        }


        
        private void InitializeColumnsAndStuff()
        {
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn() 
                { HeaderText = "Id", Name = "colId", Width = 35, ReadOnly = true };
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Level", Name = "colLevel", Width = 40 };
            this.colRace = new System.Windows.Forms.DataGridViewTextBoxColumn() 
                { HeaderText = "Race", Name = "colRace", Width = 40 };
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Name", Name = "colName", Width = 90 };

            this.colId.DefaultCellStyle =
                new DataGridViewCellStyle() { BackColor = SystemColors.ControlLight };

            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.colId,
				this.colLevel,
				this.colRace,
				this.colName});

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllowUserToAddRows = true;

            // for the eventual combobox
            // var racesList = new List<FeatherItem>();
            // _game.Races.ToList().ForEach(x => racesList.Add(new FeatherItem(x.Name, x)));
            // this.colRace.DataSource = racesList;
        }




        #region event handlers
        private void this_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.Rows[e.RowIndex];

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

        /*
        private void this_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            var prevRow = this.Rows[0];

            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                var row = this.Rows[index];
                var previousRow = this.Rows[1];
                var previousCell = this.Rows[index - 1].Cells[COLUMN_ID];

                _maxId += 1;
                row.Cells[COLUMN_ID].Value = _maxId;
                
                // DataGridViewRow row = DataGridView1.Rows[index];

                // Do something with the added row here
                // Raise a custom RowAdded event if you want that passes individual rows.
            }
        }
        */



        private void this_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.Rows[e.RowIndex];

                Demon rowDemon;
                bool insertedNewDemon = false;

                using (var transaction = _dbSession.BeginTransaction())
                {
                    rowDemon = GetDemonFromDataGridViewRow(currentRow);
                    if (rowDemon != null)
                    {
                        _logger.Info("Row demon is " + rowDemon.ToString() + ". " +
                            (rowDemon.Id == null ? "Inserting..." : "Updating..."));

                        if (rowDemon.Id == null)
                        {
                            _dbSession.Save(rowDemon); // insert
                            insertedNewDemon = true;
                        }
                        else
                        {
                            _dbSession.SaveOrUpdate(rowDemon); // update
                        }
                        transaction.Commit();
                        _logger.Info("Demon saved.");
                    }
                    
                } // using (var transaction = _dbSession.BeginTransaction())
                if (insertedNewDemon)
                    currentRow.Cells[COLUMN_ID].Value = rowDemon.Id;

                _logger.CloseSection(location);
            }
        }
        #endregion




    }
}
