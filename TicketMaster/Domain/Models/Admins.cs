using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Admins
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;

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
        public string CurrentTicket { get; set; }
        public int SupportLevel { get; set; }

    }
}
