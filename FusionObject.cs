using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using NHibernate;
using Feathers;

namespace Yatagarasu
{
    class FusionObject
    {
        public Domain.Demon Demon1 { get; set; }
        public Domain.Demon Demon2 { get; set; }
        public Domain.Demon Demon3 { get; set; }
        public Domain.Fusion Fusion { get; set; }
        public bool FusionIsImpossible { get; set; }

        FeatherLogger _logger;
        ISession _dbSession;

        public FusionObject(Domain.Demon d1, Domain.Demon d2, Domain.Fusion f)
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;

            Demon1 = (d1.Level <= d2.Level ? d1 : d2);
            Demon2 = (d1.Level <= d2.Level ? d2 : d1);
            Fusion = f;
            Demon3 = FindDemonFromFusion(d1, d2, f);

            _logger.CloseSection(location);
        }

        private Domain.Demon FindDemonFromFusion(
            Domain.Demon d1, Domain.Demon d2, Domain.Fusion f)
        {
            Domain.Demon returnDemon = null;

            if (f != null)
            {
                if (f.IdRace3 == GlobalObjects.ImpossibleToFuseRace.Id)
                {
                    FusionIsImpossible = true;
                }
                else
                {
                    returnDemon = _dbSession.CreateCriteria<Domain.Demon>().List<Domain.Demon>()
                    .Where(x =>
                        x.Race.Id == f.IdRace3 &&
                        x.Level > (d1.Level + d2.Level) / 2)
                    .OrderBy(x => x.Level)
                    .FirstOrDefault();
                }
                
            }
            return returnDemon;
        }

        /*
        public object[] ToDataRow()
        {
            var returnObjectList = new List<string>();

            returnObjectList.Add(Demon1.Level.ToString().PadLeft(2, '0'));
            returnObjectList.Add(Demon1.Race.Name);
            returnObjectList.Add(Demon1.Name);

            returnObjectList.Add(Demon2.Level.ToString().PadLeft(2, '0'));
            returnObjectList.Add(Demon2.Race.Name);
            returnObjectList.Add(Demon2.Name);

            if (Demon3 != null)
            {
                returnObjectList.Add(Demon3.Level.ToString().PadLeft(2, '0'));
                returnObjectList.Add(Demon3.Race.Name);
                returnObjectList.Add(Demon3.Name);
            }
            else
            {
                if (FusionIsImpossible)
                {
                    returnObjectList.Add("-");
                    returnObjectList.Add("-");
                    returnObjectList.Add(GlobalObjects.ImpossibleToFuseRace.Name);
                }
                else
                {
                    returnObjectList.Add(" ?");
                    returnObjectList.Add("?");
                    returnObjectList.Add("?");
                }
            }

            return returnObjectList.ToArray<object>();
        }
        */

    }
}
