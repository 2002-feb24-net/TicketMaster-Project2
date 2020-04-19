using System;
using System.Collections.Generic;

namespace REST_Api.ApiModels
{
    public class Admins
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentTicket { get; set; }
        public int SupportLevel { get; set; }

    }
}
