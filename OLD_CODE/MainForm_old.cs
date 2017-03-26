﻿using System;
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
    public partial class MainForm_old : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;

        ChooseGameForm _chooseGameForm;
        FullDemonsListForm _fullDemonsListForm;
        DemonsForm _partyDemonsListForm;
        FusionsChartForm _fusionsForm;
        PartyFusionsVerticalForm _partyFusionsVerticalForm;

        private const string FORM_NAME = "Yatagarasu";

        public MainForm_old()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            GlobalObjects.MainForm = this;
            GlobalObjects.CurrentGame = null;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // ShowPartyDemonsForm();
            ShowFusionsChartForm();
            ShowFullDemonsListForm();
            ShowPartyFusionsForm();

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

                if (_fullDemonsListForm != null) _fullDemonsListForm.LoadData();
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


        public void ForceUpdateFusions()
        {
            if (_partyFusionsVerticalForm != null)
            {
                _partyFusionsVerticalForm.Visible = true;
                _partyFusionsVerticalForm.LoadData();
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


        private void ShowFullDemonsListForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_fullDemonsListForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _fullDemonsListForm = new FullDemonsListForm();
                _fullDemonsListForm.MdiParent = this;
            }
            _logger.Info(_fullDemonsListForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_fullDemonsListForm.Visible) _fullDemonsListForm.Show();
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


        private void ShowFusionsChartForm()
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


        private void ShowPartyFusionsForm()
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
            ShowFullDemonsListForm();
        }

        private void partyDemonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPartyDemonsForm();
        }
        #endregion




    }
}
