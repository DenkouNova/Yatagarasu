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

        public virtual Game Game { get; set; }

        private ISet<Demon> demons = new HashSet<Demon>();
        public virtual ISet<Demon> Demons
        {
            get { return demons; }
            set { demons = value; }
        }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Name = " + FeatherStrings.TraceString(Name);
        }
    }
}
