using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Users
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string? _phoneNumber;

        public int Id { get; set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                _lastName = value;
            }
        }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                //if (value.Length != 10)//> 9999999999 || value < 999999999)
                //{
                //    throw new ArgumentException("You must enter a valid phone number.", nameof(value));
                //}
                _phoneNumber = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Email must not be empty.", nameof(value));
                }
                _email = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Password must not be empty.", nameof(value));
                }
                _password = value;
            }
        }
    }
}
