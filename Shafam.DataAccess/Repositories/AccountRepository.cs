using System;
using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IShafamDataContext _dataContext;

        public AccountRepository(IShafamDataContext shafamDataContext)
        {
            _dataContext = shafamDataContext;
        }

        public void CreateAccount(Account account)
        {
            _dataContext.Accounts.Add(account);
            _dataContext.Save();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _dataContext.Accounts.ToList();
        }

        public Account GetAccount(string username)
        {
            return _dataContext.Accounts.FirstOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        public Account GetAccountByUserId(int userId, UserRole role)
        {
            return _dataContext.Accounts.FirstOrDefault(a => a.UserId == userId && a.Role == role);
        }

        public Account VerifyAccount(string username, string password)
        {
            return _dataContext.Accounts.FirstOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                                                             && u.Password.Equals(password) && !u.Disabled);
        }

        public void DisableAccount(int userId, UserRole role)
        {
            Account account = GetAccountByUserId(userId, role);
            account.Disabled = true;
            _dataContext.Save();
        }

        public void EnableAccount(int userId, UserRole role)
        {
            Account account = GetAccountByUserId(userId, role);
            account.Disabled = false;
            _dataContext.Save();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            Account account = VerifyAccount(username, oldPassword);

            if (account == null)
            {
                return false;
            }

            account.Password = newPassword;
            _dataContext.Save();

            return true;
        }
    }
}
