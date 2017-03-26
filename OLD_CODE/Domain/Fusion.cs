using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Feathers;

namespace Yatagarasu.Domain
{
    class Fusion
    {
        //ISession _dbSession = GlobalObjects.DbSession;

        public Fusion() { }

        public Fusion(Race r1, Race r2, Race r3)
        {
            IdRace1 = r1.Id > r2.Id ? r1.Id : r2.Id;
            IdRace2 = r1.Id > r2.Id ? r2.Id : r1.Id;
            IdRace3 = r3.Id;
        }

        public virtual int Id { get; set; }

        public virtual int IdRace1 { get; set; }
        public virtual int IdRace2 { get; set; }
        public virtual int? IdRace3 { get; set; }

        /*
        public virtual Race Race1 {
            get
            {
                if (race1 == null)
                    race1 = _dbSession.Get<Domain.Race>(IdRace1);
                return race1;
            }
            set {
                race1 = value;
                IdRace1 = race1.Id;
            }
        }

        public virtual Race Race2
        {
            get
            {
                if (race2 == null)
                    race2 = _dbSession.Get<Domain.Race>(IdRace2);
                return race2;
            }
            set { 
                race2 = value;
                IdRace2 = race2.Id;
            }
        }

        public virtual Race Race3
        {
            get
            {
                if (race3 == null && IdRace3 != null)
                    race3 = _dbSession.Get<Domain.Race>(IdRace3);
                return race3;
            }
            set { race3 = value; }
        }
        */

        public override string ToString()
        {
            return 
                "IdRace1 = " + FeatherStrings.TraceString(IdRace1) + 
                "IdRace2 = " + FeatherStrings.TraceString(IdRace2) + 
                "IdRace3 = " + FeatherStrings.TraceString(IdRace3);
        }
    }
}
