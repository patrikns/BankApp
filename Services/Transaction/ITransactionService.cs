using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.Transaction
{
    public interface ITransactionService
    {
        Account GetAccountById(int id);
        void AddCredit(TransactionCreditViewModel viewModel);
        void Withdrawal(TransactionDebitViewModel viewModel);
        void Transfer(TransactionTransferViewModel viewModel);
    }
}
