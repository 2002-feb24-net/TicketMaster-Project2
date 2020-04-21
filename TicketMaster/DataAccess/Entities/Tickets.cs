using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Tickets
    {
        public Tickets()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime DatetimeOpened { get; set; }
        public DateTime? DatetimeModified { get; set; }
        public DateTime? DatetimeClosed { get; set; }
        public DateTime? Deadline { get; set; }
        public int Priority { get; set; }
        public string Details { get; set; }
        public int UserId { get; set; }
        public int? AdminId { get; set; }
        public int? StoreId { get; set; }
        public string Completed { get; set; }

        public virtual Admins Admin { get; set; }
        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
