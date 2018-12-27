using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class EnergeticsObject : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual EnergeticsObject Parent { get; set; }
        public int? SubjectId { get; set; }
        public virtual EnergeticsSubject Subject { get; set; }
        public string Address { get; set; }
        public int? EnegeticsTypeId { get; set; }
        public string ObjType { get; set; }
        public virtual EnergeticsType EnergeticsType { get; set; }
        public virtual ICollection<InvestProject> InvestProjects { get; set; }
    }
}
