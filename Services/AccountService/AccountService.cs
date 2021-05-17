using System.Linq;
using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.AccountService
{
    class AccountService : IAccountService
    {
        private readonly BankAppDataContext _dbContext;

        public AccountService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.First(a => a.AccountId == id);
        }
    }
}