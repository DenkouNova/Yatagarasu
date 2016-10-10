using System;
using System.Text;
using System.Collections.Generic;

using Feathers;

namespace Yatagarasu.Domain
{

    public class Game
    {

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Name = " + FeatherStrings.TraceString(Name);
        }



    }
}
