using System.Collections.Generic;

namespace Uppgift2BankApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<CountryStatisticViewModel> Items { get; set; }
        public class CountryStatisticViewModel
        {
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public int CustomerCount { get; set; }
            public int AccountCount { get; set; }
            public decimal TotalBalance { get; set; }
        }
    }
}
