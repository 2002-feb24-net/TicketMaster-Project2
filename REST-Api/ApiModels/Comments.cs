using System;
using System.Collections.Generic;

namespace REST_Api.ApiModels
{
    public  class Comments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Comment { get; set; }
        public DateTime? Datetime { get; set; }
        public int? AdminId { get; set; }

    }
}
