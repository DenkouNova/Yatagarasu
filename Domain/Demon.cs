using System;
using System.Text;
using System.Collections.Generic;

using Feathers;

namespace Yatagarasu.Domain
{

    public class Demon
    {
        public Demon() { Id = null; }

        public virtual int? Id { get; set; }

        public virtual int Level { get; set; }

        public virtual int InParty { get; set; }

        public virtual int UseInFusionCalculator { get; set; }

        public virtual bool UseInFusionCalculatorBoolean { get { return UseInFusionCalculator > 0; } }

        public virtual string Name { get; set; }

        public virtual Race Race { get; set; }

        public override bool Equals(object obj)
        {
            Domain.Demon other = obj as Domain.Demon;
            if (other != null)
            {
                if (this.Level == other.Level &&
                    this.Name == other.Name)
                {
                    if (this.Race == null && other.Race == null)
                        return true;

                    if (this.Race != null && other.Race != null && this.Race.Id == other.Race.Id)
                        return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Level = " + FeatherStrings.TraceString(Level) +
                ", " + "Name = " + FeatherStrings.TraceString(Name) + 
                ", " + "Race = " + FeatherStrings.TraceString(Race.Name) + 
                ", " + "InParty = " + FeatherStrings.TraceString(InParty);
        }
    }
}
