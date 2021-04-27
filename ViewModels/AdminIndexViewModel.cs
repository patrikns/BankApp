using System.Collections.Generic;
using Uppgift2BankApp.Models;

namespace Uppgift2BankApp.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<Account> Accounts { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public decimal Saldo { get; set; }
        }
    }
}
