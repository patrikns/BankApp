using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.TransactionService
{
    public interface ITransactionService
    {
        TransactionCreditViewModel Credit(int id);
        TransactionDebitViewModel Debit(int id);
        TransactionTransferViewModel Transfer(int id);
        List<SelectListItem> GetDebitOperations();
        List<SelectListItem> GetCreditOperations();
    }
}