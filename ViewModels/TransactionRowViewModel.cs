using System;
using System.ComponentModel.DataAnnotations;

namespace Uppgift2BankApp.ViewModels
{
    public class TransactionRowViewModel
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
    }
}