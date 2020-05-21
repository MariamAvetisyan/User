using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp
{
    public class User
    {
        public User()
        {
        }

        public int UserId { get; set; }
        public int PhoneNumber;
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
    }
}
