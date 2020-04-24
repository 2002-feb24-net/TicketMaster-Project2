using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Admins
    {
        public Admins()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentTicket { get; set; }
        public int SupportLevel { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
