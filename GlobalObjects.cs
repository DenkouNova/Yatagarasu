﻿using System;
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
        private static bool _exiting = false;

        //public static Color InPartyCell = Color.FromArgb(209, 248, 171);
        //public static Color CannotEditCell = SystemColors.ControlLight;
        //public static Color DefaultCell = SystemColors.Window;

        private static FeatherLogger _logger = null;
        private static ISession _dbSession = null;
        private static Domain.Game _game = null;

        //public static bool AUTOMATIC_UPDATE_OF_PARTY_FUSIONS = false;

        public static MainForm MainForm { get; set; }

        public static FeatherLogger Logger 
            { get { return _logger ?? (_logger = CreateFeatherLogger()); } }

        public static ISession DbSession
            { get { return _dbSession ?? (_dbSession = CreateDbSession()); } }

        public static Domain.Game CurrentGame { 
            get { return _game; }
            set { _game = value; }
        }

        public static bool Exiting
        {
            get { return _exiting; }
            set { _exiting = value; }
        }

        public static Domain.Race ImpossibleToFuseRace { get; set; }

        private static FeatherLogger CreateFeatherLogger()
        {
            try
            {
                string location = new StackFrame().GetMethod().DeclaringType.ToString();
                var returnLogger = new FeatherLogger(
                        //FeatherLogger.TRACE_LEVEL_INFO,
                        FeatherLogger.TRACE_LEVEL_ERROR,
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

        public static DataGridViewCellStyle GetDefaultDgvcStyle(float fontSize, bool enabled = true, bool inParty = false)
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
            if (inParty) returnStyle.BackColor = Color.LightGreen;

            return returnStyle;
        }

    }
}
