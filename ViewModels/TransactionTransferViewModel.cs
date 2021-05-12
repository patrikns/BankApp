using System;
using System.ComponentModel.DataAnnotations;

namespace Uppgift2BankApp.ViewModels
{
    public class TransactionTransferViewModel
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [StringLength(50)]
        public string Bank { get; set; }
        [StringLength(50)]
        public string Account { get; set; }
    }
}
