using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2BankApp.ViewModels
{
    public class AccountGetTransactionsFromViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionRowViewModel> Items { get; set; }

    }
}
