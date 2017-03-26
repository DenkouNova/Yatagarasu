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

namespace Yatagarasu.UserControls
{
    public partial class ChooseGame : Form
    {
        private FeatherLogger _logger;
        private ISession _dbSession;

        public Domain.Game chosenGame { get; set; }
        public bool exitWasChosen { get; set; }

        public ChooseGame()
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;
            _logger.Info("Initializing component...");
            InitializeComponent();

            exitWasChosen = false;

            PopulateGamesComboBox();
        }

        private void PopulateGamesComboBox()
        {
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var games = _dbSession.CreateCriteria<Domain.Game>().List<Domain.Game>();
            if (games.Count == 0)
            {
                this.cbGame.Items.Add(new FeatherItem("(No games in database)", null));
            }
            else
            {
                games.ToList().ForEach(x => this.cbGame.Items.Add(new FeatherItem(x.Name, x)));
            }
            this.cbGame.SelectedIndex = 0;

            _logger.CloseSection(location);
        }

        #region Event handlers
        private void btnChooseGame_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbGameName.Text))
            {
                MessageBox.Show("Please enter a game name.");
            }
            else
            {
                var gameName = this.tbGameName.Text;
                var gameAlreadyExists = _dbSession.CreateCriteria<Domain.Game>().List<Domain.Game>()
                    .Where(x => x.Name == gameName).ToList().Count > 0;

                if (gameAlreadyExists)
                {
                    MessageBox.Show("This game already exists.");
                }
                else
                {
                    try
                    {
                        using (var transaction = _dbSession.BeginTransaction())
                        {
                            chosenGame = new Domain.Game(gameName);
                            _dbSession.Save(chosenGame);
                            transaction.Commit();
                        }

                        MessageBox.Show("Game '" + chosenGame.Name + "' created."); ;
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        throw;
                    }

                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.exitWasChosen = true;
            this.Hide();
        }

        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = this.cbGame.SelectedItem as FeatherItem;
            this.chosenGame = (selectedItem == null ? null : selectedItem.Value as Domain.Game);

            this.btnChooseGame.Enabled = (this.chosenGame != null);
        }
        #endregion

    }
}
