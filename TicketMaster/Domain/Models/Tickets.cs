using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Tickets
    {
        private string _title;
        private string _category;
        private DateTime _datetime;
        private string _details;
        private int _userId;
        private int? _storeId;
        private string _completed;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Title must not be empty.", nameof(value));
                }
                _title = value;
            }
        }
        public string Category
        {
            get => _category;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Category must not be empty.", nameof(value));
                }
                _category = value;
            }
        }
        public DateTime DatetimeOpened
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
        public DateTime? DatetimeModified { get; set; }
        public DateTime? DatetimeClosed { get; set; }
        public DateTime? Deadline { get; set; }
        public int Priority { get; set; }
        public string Details
        {
            get => _details;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Details must not be empty.", nameof(value));
                }
                _details = value;
            }
        }
        public int UserId
        {
            get => _userId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("You must enter a valid UserId", nameof(value));
                }
                _userId = value;
            }
        }
        public int? AdminId { get; set; }
        public int? StoreId
        {
            get => _storeId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("You must enter a valid StoreId", nameof(value));
                }
                _storeId = value;
            }
        }
        public string Completed
        {
            get => _completed;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You must indicate whether the ticket " +
                        "has been completed or not.", nameof(value));
                }
                _completed = value;
            }
        }


        //public List<Comments>? Comments { get; set; } = new List<Comments>();
    }
}
