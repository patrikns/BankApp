using System;
using System.Collections.Generic;

namespace Uppgift2BankApp.ViewModels
{
    public class TransactionRowViewModel
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}