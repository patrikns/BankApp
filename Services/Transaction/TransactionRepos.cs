using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.Transaction
{
    class TransactionRepos : ITransactionRepos
    {
        private readonly BankAppDataContext _dbContext;

        public TransactionRepos(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public DbSet<Account> GetAccounts()
        {
            return _dbContext.Accounts;
        }

        public DbSet<SharedLibrary.Models.Transaction> GetTransactions()
        {
            return _dbContext.Transactions;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}