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

        ChooseGameForm _chooseGameForm;
        FullDemonsListForm _demonsListForm;
        DemonsForm _partyDemonsListForm;
        FusionsChartForm _fusionsForm;
        PartyFusionsVerticalForm _partyFusionsVerticalForm;

        private const string FORM_NAME = "Yatagarasu";

        public MainForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            GlobalObjects.MainForm = this;
            GlobalObjects.CurrentGame = null;

            
            ShowDemonsListForm();
            ShowPartyDemonsForm();
            ShowFusionsForm();
            ShowPartyFusionsVerticalForm();

            ShowChooseGameForm();
            
            _logger.CloseSection(location);
        }


        public void ChooseGame(Domain.Game game)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _logger.Info("Called with game = " + (game == null ? "(null)" : game.Name));

            if (game == GlobalObjects.CurrentGame)
            {
                _logger.Info("Game is same as current game, nothing to do.");
            }
            else
            {
                GlobalObjects.CurrentGame = game;

                string text = FORM_NAME + (game == null ? "" : " - " + game.Name);
                _logger.Info("Setting form text to '" + text + "'");
                this.Text = text;

                if (_demonsListForm != null) _demonsListForm.LoadData();
                if (_partyDemonsListForm != null) _partyDemonsListForm.LoadData();
                if (_fusionsForm != null) _fusionsForm.LoadData();
                if (_partyFusionsVerticalForm != null) _partyFusionsVerticalForm.LoadData(); 
            }

            _logger.CloseSection(location);
        }


        public void UpdateFusions()
        {
            if (GlobalObjects.AUTOMATIC_UPDATE_OF_PARTY_FUSIONS && _partyFusionsVerticalForm != null)
            {
                _partyFusionsVerticalForm.Visible = true;
                _partyFusionsVerticalForm.Refresh();
            }
        }


        private void ShowChooseGameForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_chooseGameForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _chooseGameForm = new ChooseGameForm();
                _chooseGameForm.MdiParent = this;
            }
            _logger.Info(_chooseGameForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_chooseGameForm.Visible) _chooseGameForm.Show();
            _logger.CloseSection(location);
        }


        private void ShowDemonsListForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_demonsListForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _demonsListForm = new FullDemonsListForm();
                _demonsListForm.MdiParent = this;
            }
            _logger.Info(_demonsListForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_demonsListForm.Visible) _demonsListForm.Show();
            _logger.CloseSection(location);
        }


        private void ShowPartyDemonsForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_partyDemonsListForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _partyDemonsListForm = new DemonsForm();
                _partyDemonsListForm.MdiParent = this;
            }
            _logger.Info(_partyDemonsListForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_partyDemonsListForm.Visible) _partyDemonsListForm.Show();
            _logger.CloseSection(location);
        }


        private void ShowFusionsForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_fusionsForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _fusionsForm = new FusionsChartForm();
                _fusionsForm.MdiParent = this;
            }
            _logger.Info(_fusionsForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_fusionsForm.Visible) _fusionsForm.Show();
            _logger.CloseSection(location);
        }


        private void ShowPartyFusionsVerticalForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_partyFusionsVerticalForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _partyFusionsVerticalForm = new PartyFusionsVerticalForm();
                _partyFusionsVerticalForm.MdiParent = this;
            }
            _logger.Info(_partyFusionsVerticalForm.Visible ? 
                "Form is already visible." : "Showing form...");
            if (!_partyFusionsVerticalForm.Visible) _partyFusionsVerticalForm.Show();
            _logger.CloseSection(location);
        }


        #region Event Handlers
        private void chooseGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChooseGameForm();
        }

        private void demonsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDemonsListForm();
        }

        private void partyDemonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPartyDemonsForm();
        }
        #endregion




    }
}
