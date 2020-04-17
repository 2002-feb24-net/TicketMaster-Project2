using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Users
    {
        public Users()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
