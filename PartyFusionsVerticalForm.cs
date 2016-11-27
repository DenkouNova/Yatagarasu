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

        const int COLUMN_LEVEL_1 = 1;
        const int COLUMN_RACE_1 = 2;
        const int COLUMN_NAME_1 = 3;

        const int COLUMN_LEVEL_2 = 4;
        const int COLUMN_RACE_2 = 5;
        const int COLUMN_NAME_2 = 6;

        const int COLUMN_LEVEL_3 = 7;
        const int COLUMN_RACE_3 = 8;
        const int COLUMN_NAME_3 = 9;

        bool _cellChanged = false;

        DataGridViewTextBoxColumn 
            colLevel1, colRace1, colName1,
            colLevel2, colRace2, colName2,
            colLevel3, colRace3, colName3;

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
            this.colLevel1 = new DataGridViewTextBoxColumn()
                { HeaderText = "Level1", Name = "colLevel1", Width = 45, ReadOnly = true };
            this.colLevel2 = new DataGridViewTextBoxColumn()
                { HeaderText = "Level2", Name = "colLevel2", Width = 45, ReadOnly = true };
            this.colLevel3 = new DataGridViewTextBoxColumn()
                { HeaderText = "Level3", Name = "colLevel3", Width = 45, ReadOnly = false };

            this.colRace1 = new DataGridViewTextBoxColumn()
                { HeaderText = "Race1", Name = "colRace1", Width = 90, ReadOnly = true };
            this.colRace2 = new DataGridViewTextBoxColumn()
                { HeaderText = "Race2", Name = "colRace2", Width = 90, ReadOnly = true };
            this.colRace3 = new DataGridViewTextBoxColumn()
                { HeaderText = "Race3", Name = "colRace3", Width = 90, ReadOnly = false };

            this.colName1 = new DataGridViewTextBoxColumn()
                { HeaderText = "Name1", Name = "colName1", Width = 170, ReadOnly = true };
            this.colName2 = new DataGridViewTextBoxColumn()
                { HeaderText = "Name2", Name = "colName2", Width = 170, ReadOnly = true };
            this.colName3 = new DataGridViewTextBoxColumn()
                { HeaderText = "Name3", Name = "colName3", Width = 170, ReadOnly = false };

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

            this.dgvPartyFusions.Columns.AddRange(new DataGridViewColumn[] {
				this.colLevel1, this.colRace1, this.colName1,
                this.colLevel2, this.colRace2, this.colName2,
                this.colLevel3, this.colRace3, this.colName3});
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
                            this.dgvPartyFusions.Rows.Add(oneFusionObject.ToDataRow());

                            this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[0].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[1].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[2].Style.BackColor =
                                oneFusionObject._demon1.InParty == 1 ?
                                GlobalObjects.InPartyCell :
                                GlobalObjects.DefaultCell;

                            this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[3].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[4].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[5].Style.BackColor =
                                oneFusionObject._demon2.InParty == 1 ?
                                GlobalObjects.InPartyCell :
                                GlobalObjects.DefaultCell;

                            this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[6].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count - 1].Cells[7].Style.BackColor =
                                this.dgvPartyFusions.Rows[this.dgvPartyFusions.Rows.Count-1].Cells[8].Style.BackColor =
                                oneFusionObject._demon3 != null && oneFusionObject._demon3.InParty == 1 ?
                                GlobalObjects.InPartyCell :
                                GlobalObjects.DefaultCell;
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



        public void ReorderTable()
        {
            if (this.dgvPartyFusions.Rows.Count > 0)
            {
                //this.dgvPartyFusions.Sort(colLevel1, ListSortDirection.Ascending);
                //this.dgvPartyFusions.Sort(colLevel2, ListSortDirection.Ascending);
                this.dgvPartyFusions.Sort(colName3, ListSortDirection.Ascending);
                this.dgvPartyFusions.Sort(colLevel3, ListSortDirection.Descending);
            }
        }

        private void AddHandlers()
        {
            this.dgvPartyFusions.SelectionChanged += 
                new EventHandler(this.dgvPartyFusions_SelectionChanged);
            this.dgvPartyFusions.CellValidating += new DataGridViewCellValidatingEventHandler(dgvPartyFusions_CellValidating);
            this.dgvPartyFusions.CellValueChanged += new DataGridViewCellEventHandler(dgvPartyFusions_CellValueChanged);
            
        }

        private void RemoveHandlers()
        {
            this.dgvPartyFusions.SelectionChanged -=
                new EventHandler(this.dgvPartyFusions_SelectionChanged);
            this.dgvPartyFusions.CellValidating -= new DataGridViewCellValidatingEventHandler(dgvPartyFusions_CellValidating);
            this.dgvPartyFusions.CellValueChanged -= new DataGridViewCellEventHandler(dgvPartyFusions_CellValueChanged);
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
                int i = 3;
                i++;
            }
        }

        private void dgvPartyFusions_SelectionChanged(object sender, EventArgs e)
        {
            var blah = this.dgvPartyFusions.Rows.Count;

            if (this.dgvPartyFusions.SelectedRows.Count == this.dgvPartyFusions.Rows.Count)
            {
                LoadData();
            }

        }

    }
}
