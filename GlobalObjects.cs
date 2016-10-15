using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
    }
}
