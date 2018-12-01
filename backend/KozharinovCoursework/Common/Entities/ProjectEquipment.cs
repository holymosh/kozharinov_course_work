using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class ProjectEquipment : Entity
    {
        public string State { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public int? InvestProjectId { get; set; }
        public virtual InvestProject InvestProject { get; set; }
    }
}
