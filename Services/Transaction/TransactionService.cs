using System.Linq;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepos _transactionRepos;

        public TransactionService(ITransactionRepos transactionRepos)
        {
            _transactionRepos = transactionRepos;
        }

        public Account GetAccountById(int id)
        {
            return _transactionRepos.GetAccounts().First(a => a.AccountId == id);
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
            _transactionRepos.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _transactionRepos.Save();
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
            _transactionRepos.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _transactionRepos.Save();
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
            _transactionRepos.GetTransactions().Add(transaction);

            var account = GetAccountById(viewModel.AccountId);
            account.Balance += transaction.Amount;
            _transactionRepos.Save();
        }

        public bool ExceedsBalance(int viewModelAccountId, decimal viewModelAmount)
        {
            return viewModelAmount > GetAccountById(viewModelAccountId).Balance;
        }

        public bool AmountIsNegative(decimal viewModelAmount)
        {
            return viewModelAmount < 0;
        }
    }
}
