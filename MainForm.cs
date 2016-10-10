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
using Yatagarasu.Domain;

namespace Yatagarasu
{
    public partial class MainForm : Form
    {
        FeatherLogger _logger;
        ISession _databaseSession;

        public MainForm()
        {
            InitializeComponent();

            // Initializing logger
            _logger = new FeatherLogger(
                FeatherLogger.TRACE_LEVEL_INFO, 
                @"D:\Logger\Yatagarasu",
                "Yatagarasu",
                true,
                ".xml");

            InitDb();
            UpdateGamesComboBox();
        }


        private void InitDb()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            try
            {
                _databaseSession = NHibernateHelper.GetCurrentSession();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

            _logger.CloseSection(location);
        }


        private void UpdateGamesComboBox()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var games = _databaseSession.CreateCriteria<Game>().List<Game>();
            foreach (Game oneGame in games)
            {
                this.cbGame.Items.Add(new FeatherItem(oneGame.Name, oneGame.Id));
            }
            if (games.Count > 0) this.cbGame.SelectedIndex = 0;

            _logger.CloseSection(location);
        }

        private void btnFusions_Click(object sender, EventArgs e)
        {
            new FusionsForm(_logger, _databaseSession).ShowDialog();
        }






    }
}
