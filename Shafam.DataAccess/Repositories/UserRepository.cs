using System;
using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IShafamDataContext _dataContext;

        public UserRepository(IShafamDataContext shafamDataContext)
        {
            _dataContext = shafamDataContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dataContext.Users.ToList();
        }

        public User GetUser(string username)
        {
            return _dataContext.Users.First(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        public User VerifyUser(string username, string password)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                                                       && u.Password.Equals(password));
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = VerifyUser(username, oldPassword);

            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;
            _dataContext.Save();

            return true;
        }
    }
}
