using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

using Feathers;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace Yatagarasu
{
    public class GlobalObjects
    {
        private static FeatherLogger _logger = null;
        private static ISession _dbSession = null;
        private static Domain.Game _game = null;

        public static MainForm MainForm { get; set; }

        public static FeatherLogger Logger 
            { get { return _logger ?? (_logger = CreateFeatherLogger()); } }

        public static ISession DbSession
            { get { return _dbSession ?? (_dbSession = CreateDbSession()); } }

        public static Domain.Game CurrentGame { 
            get { return _game; }
            set { _game = value; }
        }

        private static FeatherLogger CreateFeatherLogger()
        {
            try
            {
                string location = new StackFrame().GetMethod().DeclaringType.ToString();
                var returnLogger = new FeatherLogger(
                        FeatherLogger.TRACE_LEVEL_INFO,
                        @"D:\Logger\Yatagarasu",
                        "Yatagarasu",
                        true,
                        "xml");
                return returnLogger;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }


        private static ISession CreateDbSession()
        {
            try
            {
                string location = new StackFrame().GetMethod().DeclaringType.ToString();
                _logger.OpenSection(location);
                var returnDb = NHibernateHelper.GetCurrentSession();
                _logger.CloseSection(location);
                return returnDb;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }


        public static DataGridViewCellStyle GetDefaultDgvcStyle(float fontSize, bool enabled = true)
        {
            var returnStyle = new DataGridViewCellStyle()
            {
                Font = new System.Drawing.Font
                    ("MS PMincho",
                    fontSize,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point,
                    ((byte)(0)))
            };
            
            if (!enabled) returnStyle.BackColor = SystemColors.ControlLight;
            return returnStyle;
        }


        public static Domain.Race InsertRaceMaybe(string raceName)
        {
            string location = new StackFrame().GetMethod().DeclaringType.ToString(); 
            _logger.OpenSection(location);

            string message = "Inserting new family '" + raceName + "'";
            _logger.Info("Asking user: '" + message + "'");

            Domain.Race returnRace = null;

            DialogResult dr = MessageBox.Show(message, "New family",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                _logger.Info("User cancelled; race will not be inserted.");
            }
            else
            {
                var insertedRace = new Domain.Race()
                {
                    Name = raceName,
                    Game = GlobalObjects.CurrentGame
                };
                _logger.Info("Inserting race...");
                _dbSession.Save(insertedRace);
                _logger.Info("Inserted race: " + insertedRace.ToString());
                returnRace = insertedRace;
            }

            _logger.CloseSection(location);
            return returnRace;
        }

    }
}
