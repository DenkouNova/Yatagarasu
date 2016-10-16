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
    class FusionsDataGridView : DataGridView
    {
        const int COLUMN_RACE1 = 0;
        const int COLUMN_RACE2 = 1;
        const int COLUMN_RACE3 = 2;

        FeatherLogger _logger;
        ISession _dbSession;
        Domain.Game _game;
        Domain.Race _impossibleFusionRace;
        bool _cellChanged = false;

        DataGridViewTextBoxColumn colRace1, colRace2, colRace3;

        public FusionsDataGridView()
            : base()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _dbSession = GlobalObjects.DbSession;
            _game = GlobalObjects.CurrentGame;

            InitializeColumnsAndStuff();

            // load data
            var currentGameRaceIds = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>().Select(x => x.Id);
            var allFusions = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                .Where(x => currentGameRaceIds.Contains(x.FusionIdentifier.IdRace1))
                .OrderBy(y => y.FusionIdentifier.IdRace1)
                .ThenBy(z => z.FusionIdentifier.IdRace2).ToList();

            _impossibleFusionRace = _dbSession.Get<Domain.Race>(0);

            foreach (Fusion f in allFusions)
            {
                _logger.Info("Loaded this fusion: " + f.ToString());
                this.Rows.Add(CreateRow(f));
                this.Rows.Add(CreateRow(f, true));
            }

            this.CellValidating += new DataGridViewCellValidatingEventHandler(this_CellValidating);
            this.CellValueChanged += new DataGridViewCellEventHandler(this_CellValueChanged);
            
            _logger.CloseSection(location);
        }

        
        private Fusion GetFusionFromDataGridViewRow(DataGridViewRow dgvr)
        {
            Fusion returnFusion = null;

            object race1Value = dgvr.Cells[COLUMN_RACE1].Value;
            object race2Value = dgvr.Cells[COLUMN_RACE2].Value;
            object race3Value = dgvr.Cells[COLUMN_RACE3].Value;

            if (race1Value != null && race2Value != null)
            {
                var race1 = _game.Races.Where(x => x.Name == race1Value.ToString()).FirstOrDefault();
                var race2 = _game.Races.Where(x => x.Name == race2Value.ToString()).FirstOrDefault();

                if (race1 != null && race2 != null && race1.Id < race2.Id)
                {
                    // switch around, first race has gotta be lower
                    race1 = _game.Races.Where(x => x.Name == race2Value.ToString()).FirstOrDefault();
                    race2 = _game.Races.Where(x => x.Name == race1Value.ToString()).FirstOrDefault();
                }

                var race3 = (race3Value == null ? null :
                        _game.Races.Where(x => x.Name == race3Value.ToString()).FirstOrDefault());

                // If race 3 isn't a real race in the game then maybe it's the "gattai fuka" pseudo-race.
                if (race3Value != null && race3 == null)
                    if (race3Value.ToString() == _impossibleFusionRace.Name)
                        race3 = _impossibleFusionRace;

                var actualDemonFusion = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                    .Where(x => x.FusionIdentifier.IdRace1 == race1.Id && x.FusionIdentifier.IdRace2 == race2.Id)
                    .FirstOrDefault();

                if (actualDemonFusion != null)
                {
                    returnFusion = actualDemonFusion;
                }
                else
                {
                    returnFusion = new Fusion()
                    {
                        FusionIdentifier = new FusionIdentifier()
                        {
                            IdRace1 = race1.Id,
                            IdRace2 = race2.Id
                        },
                        Race1 = race1,
                        Race2 = race2
                    };
                }
                returnFusion.IdRace3 = (race3 == null ? null : (int?)race3.Id);
                returnFusion.Race3 = race3;
            }
            return returnFusion;
        }
        

        private object[] CreateRow(Fusion f, bool backwardsFusion = false)
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            Domain.Fusion fusionToDisplay;
            if (!backwardsFusion)
            {
                fusionToDisplay = f;
            }
            else
            {
                fusionToDisplay = new Fusion()
                {
                    FusionIdentifier = new FusionIdentifier()
                    {
                        IdRace1 = f.FusionIdentifier.IdRace2,
                        IdRace2 = f.FusionIdentifier.IdRace1
                    },
                    IdRace3 = f.IdRace3
                };
            }

            var returnObjectArray = new object[3];
            returnObjectArray[COLUMN_RACE1] = fusionToDisplay.Race1.Name;
            returnObjectArray[COLUMN_RACE2] = fusionToDisplay.Race2.Name;
            returnObjectArray[COLUMN_RACE3] = (fusionToDisplay.Race3 == null ? "" : fusionToDisplay.Race3.Name);

            _logger.CloseSection(location);

            // TODO:
            // combobox

            return returnObjectArray;
        }


        private void InitializeColumnsAndStuff()
        {
            this.colRace1 = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Race1", Name = "colRace1", Width = 110 };
            this.colRace2 = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Race2", Name = "colRace2", Width = 110 };
            this.colRace3 = new System.Windows.Forms.DataGridViewTextBoxColumn()
                { HeaderText = "Race3", Name = "colRace3", Width = 110 };

            this.colRace1.DefaultCellStyle =
                this.colRace2.DefaultCellStyle =
                this.colRace3.DefaultCellStyle =
                GlobalObjects.GetDefaultDataGridViewCellStyle();

            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.AllowUserToResizeRows = false;
            this.RowTemplate.Height = 40;
            this.RowTemplate.MinimumHeight = 40;

            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.colRace1,
				this.colRace2,
				this.colRace3});

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


        private void this_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                var currentRow = this.Rows[e.RowIndex];

                Fusion rowFusion;

                try
                {
                    using (var transaction = _dbSession.BeginTransaction())
                    {
                        rowFusion = GetFusionFromDataGridViewRow(currentRow);
                        if (rowFusion != null)
                        {
                            _logger.Info("Row fusion is " + rowFusion.ToString() + ". " //+
                                /*(rowFusion.Id == null ? "Inserting..." : "Updating...")*/
                                );

                            _dbSession.SaveOrUpdate(rowFusion);
                            transaction.Commit();
                            _logger.Info("Fusion saved.");
                        }

                    } // using (var transaction = _dbSession.BeginTransaction())
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    throw;
                }

                _logger.CloseSection(location);
            }
        }
        #endregion



    }
}
