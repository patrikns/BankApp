using System.Collections.Generic;

namespace Uppgift2BankApp.ViewModels
{
    public class AdminUserIndexViewModel
    {
        public List<Item> Items { get; set; }

        public class Item
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}
