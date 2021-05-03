using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2BankApp.ViewModels
{
    public class CustomerInfoViewModel
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public List<Item> Accounts { get; set; }
        public decimal TotalBalance { get; set; }
        public class Item
        {
            public int AccountId { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
