using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Feathers;

namespace Yatagarasu.Domain
{
    public class FusionRace
    {
        public FusionRace() { }

        public FusionRace(int idGame, int r1, int r2, int? r3)
        {
            IdGame = idGame;
            
            IdRace1 = r1;
            IdRace2 = r2;
            IdRace3 = r3;
        }

        public virtual int Id { get; set; }
        public virtual int IdGame { get; set; }

        public virtual int IdRace1 { get; set; }
        public virtual int IdRace2 { get; set; }
        public virtual int? IdRace3 { get; set; }

        public override string ToString()
        {
            return
                "IdRace1 = " + FeatherStrings.TraceString(IdRace1) +
                ", IdRace2 = " + FeatherStrings.TraceString(IdRace2) +
                ", IdRace3 = " + FeatherStrings.TraceString(IdRace3 == null ? "(null)" : IdRace3.ToString());
        }
    }
}
