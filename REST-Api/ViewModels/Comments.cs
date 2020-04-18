using System;
using System.Collections.Generic;

namespace REST_Api.ViewModels
{
    public partial class Comments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Comment { get; set; }
        public DateTime? Datetime { get; set; }
        public int? AdminId { get; set; }

        public virtual Admins Admin { get; set; }
        public virtual Tickets Ticket { get; set; }
    }
}
