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
    public partial class MainForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;

        ChooseGameForm _chooseGameForm;
        DemonsListForm _demonsListForm, _partyDemonsForm;
        FusionsForm _fusionsForm;

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
                if (_fusionsForm != null) _fusionsForm.LoadData();
            }

            _logger.CloseSection(location);
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
                _demonsListForm = new DemonsListForm(DemonsListForm.DemonsListMode.AllDemons);
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
            if (_partyDemonsForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _partyDemonsForm = new DemonsListForm(DemonsListForm.DemonsListMode.PartyDemons);
                _partyDemonsForm.MdiParent = this;
            }
            _logger.Info(_partyDemonsForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_partyDemonsForm.Visible) _partyDemonsForm.Show();
            _logger.CloseSection(location);
        }


        private void ShowFusionsForm()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            if (_fusionsForm == null)
            {
                _logger.Info("Form is null. Creating...");
                _fusionsForm = new FusionsForm();
                _fusionsForm.MdiParent = this;
            }
            _logger.Info(_fusionsForm.Visible ? "Form is already visible." : "Showing form...");
            if (!_fusionsForm.Visible) _fusionsForm.Show();
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
