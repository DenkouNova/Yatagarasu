using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Feathers;

namespace Yatagarasu.Domain
{
    public class FusionDemon
    {
        public FusionDemon() { }

        public FusionDemon(int idGame, Demon d1, Demon d2, Demon d3)
        {
            IdGame = idGame;

            Demon1 = d1;
            Demon2 = d2;
            Demon3 = d3;
        }

        public virtual int Id { get; set; }
        public virtual int IdGame { get; set; }

        public virtual Demon Demon1 { get; set; }
        public virtual Demon Demon2 { get; set; }
        public virtual Demon Demon3 { get; set; }

        public virtual Race Race3 { get; set; }
        public virtual int? Level3 { get; set; }

        public override string ToString()
        {
            return
                "Demon1 = " + FeatherStrings.TraceString(Demon1.ToString()) +
                ", Demon2 = " + FeatherStrings.TraceString(Demon2.ToString()) +
                ", Demon3 = " + FeatherStrings.TraceString(Demon3 == null ? "(null)" : Demon3.ToString());
        }

        /*
        public FusionDemon(int idGame, int d1, int d2, int? d3)
        {
            IdGame = idGame;

            IdDemon1 = d1;
            IdDemon2 = d2;
            IdDemon3 = d3;
        }

        public virtual int Id { get; set; }
        public virtual int IdGame { get; set; }

        public virtual int IdDemon1 { get; set; }
        public virtual int IdDemon2 { get; set; }
        public virtual int? IdDemon3 { get; set; }

        public override string ToString()
        {
            return
                "IdDemon1 = " + FeatherStrings.TraceString(IdDemon1) +
                ", IdDemon2 = " + FeatherStrings.TraceString(IdDemon2) +
                ", IdDemon3 = " + FeatherStrings.TraceString(IdDemon3 == null ? "(null)" : IdDemon3.ToString());
        }
        */
    }
}
