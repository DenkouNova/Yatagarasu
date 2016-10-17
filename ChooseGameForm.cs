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
    public partial class ChooseGameForm : Form
    {
        FeatherLogger _logger;
        ISession _dbSession;

        public ChooseGameForm()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            PopulateGamesComboBox();

            _logger.CloseSection(location);
        }


        private void PopulateGamesComboBox()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            this.cbGame.Items.Add(new FeatherItem("(No game selected)", null));
            this.cbGame.SelectedIndex = 0;
            var games = _dbSession.CreateCriteria<Domain.Game>().List<Domain.Game>();
            games.ToList().ForEach(x => this.cbGame.Items.Add(new FeatherItem(x.Name, x)));

            _logger.CloseSection(location);
        }


        #region Event handlers
        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = this.cbGame.SelectedItem as FeatherItem;
            var selectedGame = (selectedItem == null ? null : selectedItem.Value as Domain.Game);
            GlobalObjects.MainForm.ChooseGame(selectedGame);
        }


        private void ChooseGameForm_FormClosing(object sender, FormClosingEventArgs e)
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
