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

        public MainForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _dbSession = GlobalObjects.DbSession;

            InitializeComponent();
            RefreshButtons();
            UpdateGamesComboBox();

            _logger.CloseSection(location);
        }

        private void RefreshButtons()
        {
            this.rbDisplayDemons.Enabled =
                this.rbDisplayFusions.Enabled =
                this.btnRefresh.Enabled = 
                    (GlobalObjects.CurrentGame != null);
        }


        private void UpdateGamesComboBox()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var games = _dbSession.CreateCriteria<Domain.Game>().List<Domain.Game>();
            foreach (Domain.Game oneGame in games)
            {
                this.cbGame.Items.Add(new FeatherItem(oneGame.Name, oneGame));
            }
            if (games.Count > 0) this.cbGame.SelectedIndex = 0;

            _logger.CloseSection(location);
        }

        private void DisplayDemons()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            foreach (Control c in this.pData.Controls) Controls.Remove(c);
            this.pData.Controls.Add(new DemonsDataGridView());
            
            _logger.CloseSection(location);
        }

        private void DisplayFusions()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var allRaces = _dbSession.CreateCriteria<Domain.Race>().List<Domain.Race>();
            allRaces.OrderBy(x => x.Pronunciation).ToList().
                ForEach(x => this.tbLog.Text += x.Name + " (" + x.Pronunciation + ")\r\n");

            var allDemons = _dbSession.CreateCriteria<Domain.Demon>().List<Domain.Demon>();
            allDemons.OrderBy(x => x.Level).ToList().
                ForEach(x => this.tbLog.Text += "Lv" + x.Level + " " + x.Race.Name + " " + x.Name + "\r\n");

            

            _logger.CloseSection(location);
        }



        #region event handlers
        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            FeatherItem selectedItem;
            Domain.Game selectedGame = null;

            selectedItem = this.cbGame.SelectedItem as FeatherItem;
            if (selectedItem != null)
                selectedGame = selectedItem.Value as Domain.Game;
            _logger.Info("Currently selected game is " + 
                (selectedGame == null ? "(null)" : "'" + selectedGame.Name + "'"));
            GlobalObjects.CurrentGame = selectedGame;
            RefreshButtons();
            _logger.CloseSection(location);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (rbDisplayDemons.Checked)
            {
                DisplayDemons();
            }
            if (rbDisplayFusions.Checked)
            {
                DisplayFusions();
            }
        }
        #endregion


    }
}
