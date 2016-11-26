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
    public partial class FusionsChartForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;
        bool _cellChanged;

        private const int FONT_SIZE = 12;

        public FusionsChartForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            this.dgvFusions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dgvFusions.AllowUserToResizeRows = false;
            this.dgvFusions.RowTemplate.Height = 35;
            this.dgvFusions.RowTemplate.MinimumHeight = 35;
            this.dgvFusions.ColumnHeadersVisible = false;

            LoadData();

            _logger.CloseSection(location);
        }


        public void LoadData()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            RemoveHandlers();

            var game = GlobalObjects.CurrentGame;
            this.dgvFusions.Enabled = (game != null);
            if (game == null)
            {
                _logger.Info("No data to load; no game is chosen.");
                this.dgvFusions.Rows.Clear();
            }
            else
            {
                _logger.Info("Game '" + game.Name + "' chosen; will load list of fusions.");

                var currentGameRaces = game.Races.ToList();
                var currentGameRaceIds = currentGameRaces.Select(x => x.Id).ToList();
                var allFusions =
                    _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                    .Where(x => currentGameRaceIds.Contains(x.IdRace1))
                    .OrderBy(y => y.IdRace1)
                    .ThenBy(z => z.IdRace2).ToList();

                AddOneColumn(null);
                foreach (var oneRace in currentGameRaces) AddOneColumn(oneRace);

                // first row
                object[] oneRow = new object[currentGameRaces.Count + 1];
                oneRow[0] = " ";
                for (int i = 0; i < currentGameRaces.Count; i++)
                    oneRow[i + 1] = currentGameRaces[i].Name;
                    
                // race tag on every cell of the first row except the first one
                this.dgvFusions.Rows.Add(oneRow);
                for (int i = 0; i < currentGameRaces.Count; i++)
                    this.dgvFusions.Rows[0].Cells[i+1].Tag = currentGameRaces[i];

                // following rows
                DataGridViewRow oneDgvr;
                foreach(var oneRace in currentGameRaces)
                {
                    oneDgvr = new DataGridViewRow();
                    oneDgvr.Cells.Add(new DataGridViewTextBoxCell()
                        {
                            Value = oneRace.Name,
                            Tag = oneRace
                        });
                    foreach (var anotherRace in currentGameRaces)
                    {
                        int? idRaceResult =
                            allFusions.Where(x =>
                                x.IdRace1 == oneRace.Id &&
                                x.IdRace2 == anotherRace.Id)
                                .Select(y => y.IdRace3).FirstOrDefault();
                        if (idRaceResult == null)
                        {
                            idRaceResult =
                            allFusions.Where(x =>
                                x.IdRace2 == oneRace.Id &&
                                x.IdRace1 == anotherRace.Id)
                                .Select(y => y.IdRace3).FirstOrDefault();
                        }
                        Domain.Race raceResult =
                            (idRaceResult == null ? null :
                            _dbSession.Get<Domain.Race>(idRaceResult.GetValueOrDefault()));

                        oneDgvr.Cells.Add(new DataGridViewTextBoxCell()
                        {
                            Value = (raceResult == null ? null : raceResult.Name),
                            Tag = raceResult
                        });
                    }
                    this.dgvFusions.Rows.Add(oneDgvr);
                }

                foreach (DataGridViewCell oneCell in this.dgvFusions.Rows[0].Cells)
                {
                    oneCell.ReadOnly = true;
                    oneCell.Style = GlobalObjects.GetDefaultDgvcStyle(FONT_SIZE, false);
                }

                foreach(DataGridViewRow aRow in this.dgvFusions.Rows)
                {
                    aRow.Cells[0].ReadOnly = true;
                    aRow.Cells[0].Style = GlobalObjects.GetDefaultDgvcStyle(FONT_SIZE, false);
                }

                AddHandlers();
                _logger.Info("Adding complete.");

                SetFormats();
            }
            _logger.CloseSection(location);
        }

        public void SetFormats()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var defaultStyle = new System.Windows.Forms.DataGridViewCellStyle();
            defaultStyle.Alignment = 
                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            defaultStyle.Font = 
                new System.Drawing.Font("MS PGothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            var notAvailableStyle = new System.Windows.Forms.DataGridViewCellStyle();
            notAvailableStyle.Alignment = 
                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            notAvailableStyle.BackColor = 
                System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));

            
            


            foreach(DataGridViewTextBoxColumn oneColumn in this.dgvFusions.Columns)
                oneColumn.DefaultCellStyle = defaultStyle;

            foreach (DataGridViewRow oneRow in this.dgvFusions.Rows)
                foreach (DataGridViewCell oneCell in oneRow.Cells)
                    if (oneCell.Value != null && oneCell.Value.ToString() == GlobalObjects.ImpossibleToFuseRace.Name)
                        oneCell.Style = notAvailableStyle;

            _logger.CloseSection(location);
        }

        public void RemoveHandlers()
        {
            this.dgvFusions.CellValidating -= new DataGridViewCellValidatingEventHandler(dgvFusions_CellValidating);
            this.dgvFusions.CellValueChanged -= new DataGridViewCellEventHandler(dgvFusions_CellValueChanged);
        }


        public void AddHandlers()
        {
            this.dgvFusions.CellValidating += new DataGridViewCellValidatingEventHandler(dgvFusions_CellValidating);
            this.dgvFusions.CellValueChanged += new DataGridViewCellEventHandler(dgvFusions_CellValueChanged);
        }


        private void AddOneColumn(Domain.Race oneRace)
        {
            string raceName = (oneRace == null ? "" : oneRace.Name);
            var oneColumn = new DataGridViewTextBoxColumn() {
                HeaderText = raceName,
                Name = "col" + raceName,
                Width = 60,
                ReadOnly = (oneRace == null),
                DefaultCellStyle = GlobalObjects.GetDefaultDgvcStyle(FONT_SIZE)
            };
            this.dgvFusions.Columns.Add(oneColumn);
        }


        #region event handlers
        private void dgvFusions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
            var currentRow = this.dgvFusions.Rows[e.RowIndex];

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


        private void dgvFusions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            List<Domain.Fusion> allFusions;
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                Domain.Race race1 = this.dgvFusions.Rows[0].Cells[e.ColumnIndex].Tag as Domain.Race;
                Domain.Race race2 = this.dgvFusions.Rows[e.RowIndex].Cells[0].Tag as Domain.Race;
                var resultCell = this.dgvFusions.Rows[e.RowIndex].Cells[e.ColumnIndex];

                Domain.Race race3 = null;

                if (resultCell.Value != null)
                {
                    race3 = GlobalObjects.CurrentGame.Races.
                        Where(x => x.Name == resultCell.Value.ToString()).FirstOrDefault();
                    
                    if (race3 == null && resultCell.ToString() == GlobalObjects.ImpossibleToFuseRace.Name)
                    {
                        race3 = GlobalObjects.ImpossibleToFuseRace;
                    }

                    if (race3 == null)
                    {
                        race3 = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>()
                            .Where(x => x.Name == resultCell.Value.ToString()).FirstOrDefault();
                        if (race3 == null) race3 = GlobalObjects.InsertRaceMaybe(resultCell.ToString());
                    }
                }

                if (race3 == null && resultCell.Value != null)
                {
                    _logger.Info("Insertion cancelled.");
                }
                else
                {
                    using (var transaction = _dbSession.BeginTransaction())
                    {

                        var currentFusion =
                            _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                            .Where(x => x.IdRace1 == race2.Id && x.IdRace2 == race1.Id)
                            .FirstOrDefault();
                        if (currentFusion == null)
                        {
                            currentFusion = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                            .Where(x => x.IdRace1 == race1.Id && x.IdRace2 == race2.Id)
                            .FirstOrDefault();
                        }

                        if (currentFusion != null)
                        {
                            _logger.Info("Got fusion " + currentFusion.ToString());
                            if (race3 != null)
                            {
                                _logger.Info("Result of fusion will now be " + race3.ToString());
                                currentFusion.IdRace3 = race3.Id;
                                _logger.Info("Saving fusion " + currentFusion.ToString());
                                _dbSession.Update(currentFusion);
                                _logger.Info("Saved.");
                            }
                            else
                            {
                                _logger.Info("Fusion will be deleted from database.");
                                _dbSession.Delete(currentFusion);
                                _logger.Info("Deleted.");
                            }
                            
                        }
                        else
                        {
                            currentFusion = new Domain.Fusion(race1, race2, race3);
                            _logger.Info("Creating new fusion " + currentFusion.ToString());
                            _dbSession.Save(currentFusion);
                            _logger.Info("Created.");
                        }
                        
                        transaction.Commit();
                        _logger.Info("Fusion saved.");
                    }
                    
                }

                _logger.CloseSection(location);
            }
        }


        private void FusionsForm_FormClosing(object sender, FormClosingEventArgs e)
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
