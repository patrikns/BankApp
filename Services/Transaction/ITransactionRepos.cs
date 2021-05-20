using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.Transaction
{
    public interface ITransactionRepos
    {
        DbSet<Account> GetAccounts();
        DbSet<SharedLibrary.Models.Transaction> GetTransactions();
        void Save();
    }
}
