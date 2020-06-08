using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UserApp
{
    class Validation
    {
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[0-9]{1,10}$").Success;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
