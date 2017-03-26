using System;
using System.Web;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;

namespace Yatagarasu
{
    // code partly from http://nhibernate.info/doc/nh/en/index.html#quickstart-playingwithcats

    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;
        private static ISession currentSession = null;

        static NHibernateHelper()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();

            // Add all hbm.xml files in the assembly
            cfg.AddAssembly(typeof(Yatagarasu.NHibernateHelper).Assembly);

            sessionFactory = cfg.BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
            if (currentSession == null || !currentSession.IsOpen)
            {
                currentSession = sessionFactory.OpenSession();
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            if (currentSession == null)
            {
                return;
            }

            currentSession.Close();
            currentSession = null;
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}