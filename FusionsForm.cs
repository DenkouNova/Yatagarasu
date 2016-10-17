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
    public partial class FusionsForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;
        bool _cellChanged;

        private const int FONT_SIZE = 12;

        public FusionsForm()
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
                _logger.Info("No data to load; no game game is chosen.");
                this.dgvFusions.Rows.Clear();
            }
            else
            {
                _logger.Info("Game '" + game.Name + "' chosen; will load list of fusions.");

                var currentGameRaces = game.Races.ToList();
                var currentGameRaceIds = currentGameRaces.Select(x => x.Id);
                var allFusions = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                    .Where(x => currentGameRaceIds.Contains(x.FusionIdentifier.IdRace1))
                    .OrderBy(y => y.FusionIdentifier.IdRace1)
                    .ThenBy(z => z.FusionIdentifier.IdRace2).ToList();

                AddOneColumn(null);
                foreach (var oneRace in currentGameRaces) AddOneColumn(oneRace);

                // first row
                object[] oneRow = new object[currentGameRaces.Count + 1];
                oneRow[0] = " ";
                for (int i = 0; i < currentGameRaces.Count; i++)
                    oneRow[i + 1] = currentGameRaces[i].Name;
                this.dgvFusions.Rows.Add(oneRow);

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
                        Domain.Race raceResult =
                            allFusions.Where(x =>
                                x.FusionIdentifier.IdRace1 == oneRace.Id &&
                                x.FusionIdentifier.IdRace2 == anotherRace.Id)
                                .Select(y => y.Race3).FirstOrDefault();
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
            }
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
            var oneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn() {
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
            if (_cellChanged)
            {
                string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                _logger.OpenSection(location);

                _logger.Info("Called with row index " + e.RowIndex + ", column index = " + e.ColumnIndex);
                Domain.Race race1 = this.dgvFusions.Rows[0].Cells[e.ColumnIndex].Tag as Domain.Race;
                Domain.Race race2 = this.dgvFusions.Rows[e.RowIndex].Cells[0].Tag as Domain.Race;
                var resultCell = this.dgvFusions.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Domain.Race race3 = resultCell.Tag as Domain.Race;

                if (race3 == null)
                {
                    race3 = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>()
                        .Where(x => x.Name == resultCell.Value.ToString()).FirstOrDefault();
                    if (race3 == null) race3 = GlobalObjects.InsertRaceMaybe(resultCell.ToString());
                }

                if (race3 == null)
                {
                    _logger.Info("Insertion cancelled.");
                }
                else
                {
                    // TODO FIX THIS
                    var currentFusion = _dbSession.CreateCriteria<Domain.Fusion>().List<Domain.Fusion>()
                    .Where(x => x.FusionIdentifier.IdRace1 == race1.Id &&
                        x.FusionIdentifier.IdRace2 == race2.Id).FirstOrDefault();

                    _logger.Info("Got fusion " + currentFusion.ToString());
                    _logger.Info("Result of fusion will now be " + race3.ToString());

                    currentFusion.IdRace3 = race3.Id;
                    currentFusion.Race3 = race3;

                    _dbSession.SaveOrUpdate(currentFusion);
                    _logger.Info("Fusion saved.");
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
