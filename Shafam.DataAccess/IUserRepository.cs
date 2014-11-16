using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUser(string username);

        User VerifyUser(string username, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}