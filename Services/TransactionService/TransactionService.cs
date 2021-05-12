using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.TransactionService
{
    class TransactionService : ITransactionService
    {
        public TransactionCreditViewModel Credit(int id)
        {
            var viewModel = new TransactionCreditViewModel
            {
                AccountId = id, Date = DateTime.Now, OperationItems = GetCreditOperations()
            };

            return viewModel;
        }

        public List<SelectListItem> GetCreditOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit", "0"),
                new("Credit in cash", "1"),
            };
            return l;
        }

        public TransactionDebitViewModel Debit(int id)
        {
            var viewModel = new TransactionDebitViewModel
            {
                AccountId = id, Date = DateTime.Now, OperationItems = GetDebitOperations()
            };
            return viewModel;
        }

        public List<SelectListItem> GetDebitOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit card withdrawal", "0"),
                new ("Withdrawal in cash", "1"),
            };
            return l;
        }

        public TransactionTransferViewModel Transfer(int id)
        {
            var viewModel = new TransactionTransferViewModel
            {
                AccountId = id, Date = DateTime.Now
            };

            return viewModel;
        }
    }
}