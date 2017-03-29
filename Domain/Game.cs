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

        private ISet<Race> races = new HashSet<Race>();
        public virtual ISet<Race> Races
        {
            get { return races; }
            set { races = value; }
        }

        private ISet<FusionRace> fusionRaces = new HashSet<FusionRace>();
        public virtual ISet<FusionRace> FusionRaces
        {
            get { return fusionRaces; }
            set { fusionRaces = value; }
        }

        private ISet<FusionDemon> fusionDemons = new HashSet<FusionDemon>();
        public virtual ISet<FusionDemon> FusionDemons
        {
            get { return fusionDemons; }
            set { fusionDemons = value; }
        }

        public virtual Dictionary<int, Demon> demonsById { get; set; }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Name = " + FeatherStrings.TraceString(Name);
        }



    }
}
