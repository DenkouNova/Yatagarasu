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
        private Domain.Demon _demon1;
        private Domain.Demon _demon2;
        private Domain.Demon _demon3;
        private Domain.Fusion _fusion;
        private bool _fusionIsImpossible = false;

        FeatherLogger _logger;
        ISession _dbSession;

        public FusionObject(Domain.Demon d1, Domain.Demon d2, Domain.Fusion f)
        {
            _logger = GlobalObjects.Logger;
            string location = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            _dbSession = GlobalObjects.DbSession;

            _demon1 = (d1.Level <= d2.Level ? d1 : d2);
            _demon2 = (d1.Level <= d2.Level ? d2 : d1);
            _fusion = f;
            _demon3 = FindDemonFromFusion(d1, d2, f);

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
                    _fusionIsImpossible = true;
                }
                else
                {
                    returnDemon = _dbSession.CreateCriteria<Domain.Demon>().List<Domain.Demon>()
                    .Where(x =>
                        x.Race.Id == f.IdRace3 &&
                        x.Level >= (d1.Level + d2.Level) / 2)
                    .OrderBy(x => x.Level)
                    .FirstOrDefault();
                }
                
            }
            return returnDemon;
        }


        public object[] ToDataRow()
        {
            var returnObjectList = new List<string>();

            returnObjectList.Add(_demon1.Level.ToString().PadLeft(2, '0'));
            returnObjectList.Add(_demon1.Race.Name);
            returnObjectList.Add(_demon1.Name);

            returnObjectList.Add(_demon2.Level.ToString().PadLeft(2, '0'));
            returnObjectList.Add(_demon2.Race.Name);
            returnObjectList.Add(_demon2.Name);

            if (_demon3 != null)
            {
                returnObjectList.Add(_demon3.Level.ToString().PadLeft(2, '0'));
                returnObjectList.Add(_demon3.Race.Name);
                returnObjectList.Add(_demon3.Name);
            }
            else
            {
                if (_fusionIsImpossible)
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


    }
}
