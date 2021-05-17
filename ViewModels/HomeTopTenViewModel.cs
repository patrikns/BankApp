using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2BankApp.ViewModels
{
    public class HomeTopTenViewModel
    {
        public List<Item> Items { get; set; }
        public class Item
        {
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
