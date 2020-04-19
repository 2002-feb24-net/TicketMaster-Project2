using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Comments
    {
        private int _ticketId;
        private string _comment;
        private DateTime? _datetime;
        private int? _adminId;

        public int Id { get; set; }
        public int TicketId
        {
            get => _ticketId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("You must enter a valid TicketId", nameof(value));
                }
                _ticketId = value;
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Comment must not be empty.", nameof(value));
                }
                _comment = value;
            }
        }
        public DateTime? Datetime
        {
            get => _datetime;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Datetime must not be empty.", nameof(value));
                }
                _datetime = value;
            }
        }
        public int? AdminId
        {
            get => _adminId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("You must enter a valid AdminId", nameof(value));
                }
                _adminId = value;
            }
        }
    }
}
