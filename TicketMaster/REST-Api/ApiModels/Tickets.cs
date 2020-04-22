using System;
using System.Collections.Generic;

namespace REST_Api.ApiModels
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Category { get; set; }
        public DateTime DatetimeOpened { get; set; }
        public DateTime? DatetimeModified { get; set; }
        public DateTime? DatetimeClosed { get; set; }
        public string Priority { get; set; }
        public string Details { get; set; }
        public int UserId { get; set; }
        public string? UserRequesterName { get; set; }
        public int? AdminId { get; set; }
        public string? AdminAssignedName { get; set; }
        public int? StoreId { get; set; }
        public string Completed { get; set; }

        //public List<Comments>? Comments { get; set; } = new List<Comments>();
    }
}
