using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.Transaction
{
    public interface ITransactionRepos
    {
        DbSet<Account> GetAccounts();
        DbSet<SharedLibrary.Models.Transaction> GetTransactions();
        void Save();
    }
}
