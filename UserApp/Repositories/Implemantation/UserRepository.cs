using System;
using System.Collections.Generic;
using System.Configuration;
using UserApp.Common;
using System.Linq;

namespace UserApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IXmlWorker _xmlWorker;
        private readonly string _path;
        public List<User> Users { get; set; }

        public UserRepository(IXmlWorker xmlWorker)
        {
            _xmlWorker = xmlWorker;
            _path = ConfigurationManager.AppSettings["xmlPath"];
            Users = _xmlWorker.Deserilize<List<User>>(_xmlWorker.Read());
        }


        public void AddUser(User user)
        {
            if (Users.Count != 0)
                user.UserId = Users[(Users.Count - 1)].UserId + 1;
            else
                user.UserId = 1;
            Users.Add(user);
            string xml = _xmlWorker.Serialize<List<User>>(Users);
            _xmlWorker.Save(xml, _path);
        }

        public void RemoveUser(int id)
        {
            var user = Users.FirstOrDefault(x => x.UserId == id);

            Users.Remove(user);
            string xml = _xmlWorker.Serialize<List<User>>(Users);
            _xmlWorker.Save(xml, _path);
        }

        public void RemoveAllUsers()
        {
            Users = null;
        }

        public User GetUser(int id)
        {
            User user = Users.FirstOrDefault(x => x.UserId == id);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public void Update(User user, UserProperties userProperty, string Value)
        {
            switch (userProperty)
            {
                case UserProperties.FirstName:
                    user.FirstName = Value;
                    break;
                case UserProperties.LastName:
                    user.Lastname = Value;
                    break;
                case UserProperties.EmailAddress:
                    user.EmailAddress = Value;
                    break;
                case UserProperties.PhoneNumber:
                    user.PhoneNumber = Value;
                    break;
            }
            string xml = _xmlWorker.Serialize<List<User>>(Users);
            _xmlWorker.Save(xml, _path);
        }
    }
}