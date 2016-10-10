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
    public partial class FusionsForm : Form
    {
        FeatherLogger _logger;
        ISession _databaseSession;

        public FusionsForm(FeatherLogger logger, ISession databaseSession)
        {
            _logger = logger;
            _databaseSession = databaseSession;

            InitializeComponent();
        }

        private void FusionsForm_Load(object sender, EventArgs e)
        {
            _logger.OpenSection(this.GetType().FullName);
        }

        private void FusionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.CloseSection(this.GetType().FullName);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _logger.Info("TODO");
            MessageBox.Show("TODO");
            _logger.CloseSection(location);
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _logger.Info("TODO");
            MessageBox.Show("TODO");
            _logger.CloseSection(location);
        }
    }
}
