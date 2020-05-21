using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp
{
    public enum UserProperties
    {
       FirstName, LastName, EmailAddress, PhoneNumber
    }


    public enum Commands
    {
        AddUser, RemoveUser, ShowUser , EditUser, ShowAllUsers, RemoveAllUsers, Quite
    }
}

