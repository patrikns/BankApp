using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepos _dbContext;

        public TransactionService(ITransactionRepos dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.GetAccounts().First(a => a.AccountId == id);
        }

        public void AddCredit(TransactionCreditViewModel viewModel)
        {
            var transaction = new SharedLibrary.Models.Transaction
            {
                AccountId = viewModel.AccountId,
                Amount = viewModel.Amount,
                Date = viewModel.Date,
                Type = "Credit",
                Operation = viewModel.SelectedOperation == 0 ? "Credit" : "Credit in cash"
            };
            _dbContext.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _dbContext.Save();
        }

        public void Withdrawal(TransactionDebitViewModel viewModel)
        {
            var transaction = new SharedLibrary.Models.Transaction
            {
                AccountId = viewModel.AccountId,
                Amount = -viewModel.Amount,
                Date = viewModel.Date,
                Type = "Debit",
                Operation = viewModel.SelectedOperation == 0 ? "Credit card withdrawal" : "Withdrawal in cash"
            };
            _dbContext.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _dbContext.Save();
        }

        public void Transfer(TransactionTransferViewModel viewModel)
        {
            var transaction = new SharedLibrary.Models.Transaction
            {
                AccountId = viewModel.AccountId,
                Amount = -viewModel.Amount,
                Date = viewModel.Date,
                Type = "Debit",
                Operation = "Remittance to another bank",
                Bank = viewModel.Bank,
                Account = viewModel.Account
            };
            _dbContext.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _dbContext.Save();
        }
    }
}
