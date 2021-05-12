using System.Collections.Generic;

namespace Uppgift2BankApp.ViewModels
{
    public class CustomerIndexViewModel
    {
        public string Q { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<Item> Items { get; set; }

        public class Item
        {
            public int CustomerId { get; set; }
            public string NationalId { get; set; }
            public string Name { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
        }
    }
}
