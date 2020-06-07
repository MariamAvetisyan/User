using System;
using System.Collections.Generic;

namespace UserApp.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void Update(User user, UserProperties userProperty, string Value);
        void RemoveUser(int id);
        void RemoveAllUsers();
        User GetUser(int id);
        IEnumerable<User> GetAll();
    }
}
