using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Common.Entities
{
    public class EnergeticsSubject : Entity
    {
        public string Name { get; set; }
        public string Okved { get; set; }
        public string Inn { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PostAddress { get; set; }
        public string Kpp { get; set; }
        public int? HoldingId { get; set; }
        public virtual  Holding Holding { get; set; }
    }
}
