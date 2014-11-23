using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IAccountRepository
    {
        void CreateAccount(Account account);

        IEnumerable<Account> GetAccounts();

        Account GetAccount(string username);

        Account GetAccountByUserId(int userId, UserRole role);

        Account VerifyAccount(string username, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);

        void DisableAccount(int userId, UserRole role);

        void EnableAccount(int userId, UserRole role);
    }
}