using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2BankApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
