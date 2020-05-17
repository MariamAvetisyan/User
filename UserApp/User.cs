using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp
{
    class User
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public bool IsNumber { get; private set; }
        private int _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber.ToString();
            set
            {
                try
                {
                    _phoneNumber = Convert.ToInt32(value);
                    IsNumber = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide valid data");
                    IsNumber = false;
                }
            }
        }
    }
}
