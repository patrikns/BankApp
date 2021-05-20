using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.AccountService
{
    public interface IAccountService
    {
        Account GetAccountById(int id);
    }
}
