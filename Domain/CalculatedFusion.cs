using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Feathers;

namespace Yatagarasu.Domain
{
    class CalculatedFusion
    {
        public CalculatedFusion() { }

        public CalculatedFusion(Demon d1, Demon d2, Demon d3)
        {
            IdDemon1 = d1.Id > d2.Id ? d1.Id.Value : d2.Id.Value;
            IdDemon2 = d1.Id > d2.Id ? d2.Id.Value : d1.Id.Value;
            IdDemon3 = d3.Id;
        }

        public virtual int Id { get; set; }

        public virtual int IdDemon1 { get; set; }
        public virtual int IdDemon2 { get; set; }
        public virtual int? IdDemon3 { get; set; }
        
        public override string ToString()
        {
            return
                "IdDemon1 = " + FeatherStrings.TraceString(IdDemon1) +
                "IdDemon2 = " + FeatherStrings.TraceString(IdDemon2) +
                "IdDemon3 = " + FeatherStrings.TraceString(IdDemon3);
        }
    }
}
