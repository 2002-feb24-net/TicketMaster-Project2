using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Stores
    {
        public Stores()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
