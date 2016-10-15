using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Feathers;

namespace Yatagarasu.Domain
{
    public class Race
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Pronunciation { get; set; }
        // also game

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Name = " + FeatherStrings.TraceString(Name) +
                ", " + "Pronunciation = " + FeatherStrings.TraceString(Pronunciation);
        }
    }
}
