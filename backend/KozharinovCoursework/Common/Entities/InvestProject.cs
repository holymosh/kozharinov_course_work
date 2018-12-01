using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class InvestProject : Entity
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public double TotalCost { get; set; }
        public string Customer { get; set; }
        public string Executor { get; set; }
        public virtual ICollection<EnergeticsObject> EnergeticsObjects { get; set; }
        public virtual ICollection<ProjectCheck> ProjectChecks { get; set; }
        public virtual ICollection<ProjectEquipment> ProjectEquipments { get; set; }
    }
}
