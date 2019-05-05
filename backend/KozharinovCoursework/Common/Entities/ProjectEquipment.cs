using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class ProjectEquipment : Entity
    {
        public string State { get; set; }
        public int VendorId { get; set; }
        public double Power { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public int? InvestProjectId { get; set; }
        public virtual InvestProject InvestProject { get; set; }
        public DateTime PredictedDate { get; set; }
    }
}
