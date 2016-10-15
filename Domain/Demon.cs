using System;
using System.Text;
using System.Collections.Generic;

using Feathers;

namespace Yatagarasu.Domain
{

    public class Demon
    {
        public virtual int Id { get; set; }

        public virtual int Level { get; set; }

        public virtual string Name { get; set; }

        public virtual Race Race { get; set; }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Level = " + FeatherStrings.TraceString(Level) +
                ", " + "Name = " + FeatherStrings.TraceString(Name) + 
                ", " + "Race = " + FeatherStrings.TraceString(Race.Name);
        }
    }
}
