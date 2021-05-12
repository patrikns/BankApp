using System.Collections.Generic;

namespace Uppgift2BankApp.ViewModels
{
    public class AccountIndexViewModel
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionRowViewModel> Items { get; set; }

    }
}
