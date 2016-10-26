using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Feathers;

namespace Yatagarasu.Domain
{
    [Serializable]
    public class FusionIdentifier
    {
        public virtual int IdRace1 { get; set; }
        public virtual int IdRace2 { get; set; }

        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as FusionIdentifier;
            if (t == null) return false;
            if (IdRace1 == t.IdRace1
             && IdRace2 == t.IdRace2)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ IdRace1.GetHashCode();
            hash = (hash * 397) ^ IdRace2.GetHashCode();

            return hash;
        }
        #endregion
    }

    class Fusion
    {
        ISession _dbSession = GlobalObjects.DbSession;

        public Fusion() { }

        public Fusion(Race r1, Race r2, Race r3)
        {
            Race insertedRace1, insertedRace2, insertedRace3;

            insertedRace1 = (r1.Id < r2.Id) ? r2 : r1;
            insertedRace2 = (r1.Id < r2.Id) ? r1 : r2;
            insertedRace3 = r3;            

            FusionIdentifier = new FusionIdentifier()
            {
                IdRace1 = insertedRace1.Id,
                IdRace2 = insertedRace2.Id
            };
            IdRace3 = r3.Id;
            race1 = insertedRace1;
            race2 = insertedRace2;
            race3 = insertedRace3;
        }

        public virtual FusionIdentifier FusionIdentifier { get; set; }

        public virtual int? IdRace3 { get; set; }

        // some backwards-ass bullshit
        private Race race1 = null;
        private Race race2 = null;
        private Race race3 = null;

        public virtual Race Race1 {
            get
            {
                if (race1 == null)
                    race1 = _dbSession.Get<Domain.Race>(FusionIdentifier.IdRace1);
                return race1;
            }
            set {
                race1 = value;
                FusionIdentifier.IdRace1 = race1.Id;
            }
        }

        public virtual Race Race2
        {
            get
            {
                if (race2 == null)
                    race2 = _dbSession.Get<Domain.Race>(FusionIdentifier.IdRace2);
                return race2;
            }
            set { 
                race2 = value;
                FusionIdentifier.IdRace2 = race2.Id;
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

        public override string ToString()
        {
            return
                "IdRace1 = " + FeatherStrings.TraceString(FusionIdentifier.IdRace1) + 
                    " (" + Race1.Name + ")" +
                "IdRace2 = " + FeatherStrings.TraceString(FusionIdentifier.IdRace2) + 
                    " (" + Race2.Name + ")" +
                "IdRace3 = " + FeatherStrings.TraceString(IdRace3) +
                    (Race3 == null ? "" : " (" + Race3.Name + ")");
        }
    }
}
