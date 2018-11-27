using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class Holding : Entity
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public virtual Holding Parent { get; set; }
    }
}
