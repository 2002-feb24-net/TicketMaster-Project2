using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Stores
    {
        private long _phoneNumber;

        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value > 9999999999 || value < 999999999)
                {
                    throw new ArgumentException("You must enter a valid phone number.", nameof(value));
                }
                _phoneNumber = value;
            }
        }

    }
}
