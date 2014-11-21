using System;
using System.Threading;
using Shafam.Common.Infrastructure;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.Authentication
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IAccountRepository _accountRepository;

        public IdentityProvider(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public int GetAuthenticatedUserId()
        {
            if (Thread.CurrentPrincipal.IsAnonymous())
            {
                throw new UnauthorizedAccessException("No user is logged in");
            }

            string username = Thread.CurrentPrincipal.Identity.Name;
            int? userId = _accountRepository.GetAccount(username).UserId;

            if (userId == null)
            {
                throw new Exception("Logged in account is not associated with a user");
            }

            return userId.Value;
        }
    }
}
