using System;
using System.Xml.Serialization;

namespace UserApp
{
    public class User
    {
        public User()
        {
        }
        [XmlAttribute("User_ID")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public override string ToString()
        {
            return string.Format($"UserID {UserId} FirstName: {FirstName} Lastname: {Lastname}  Phone Number: {PhoneNumber} EmailAddress: {EmailAddress}"); 
        }
    }
}
