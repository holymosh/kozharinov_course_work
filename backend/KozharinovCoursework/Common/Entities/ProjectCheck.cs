using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ProjectCheck : Entity
    {
        public int Year { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public CheckingType CheckingType { get; set; }
        public int? InvestProjectId { get; set; }
        public virtual InvestProject InvestProject { get; set; }
        public int? EnergeticsObjectId { get; set; }
        public EnergeticsObject EnergeticsObject { get; set; }

    }

    public enum CheckingType
    {
        Planned, //Плановая
        NotPlanned // Неплановая
    }
}
