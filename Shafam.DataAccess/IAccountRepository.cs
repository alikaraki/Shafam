using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();

        Account GetAccount(string username);

        Account VerifyAccount(string username, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}