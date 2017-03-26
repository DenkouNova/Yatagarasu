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

        protected Game() { }

        public Game (string gameName)
        {
            this.Name = gameName;
        }

        /*
        private ISet<Race> races = new HashSet<Race>();
        public virtual ISet<Race> Races
        {
            get { return races; }
            set { races = value; }
        }*/

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Name = " + FeatherStrings.TraceString(Name);
        }



    }
}
