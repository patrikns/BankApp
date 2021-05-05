﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2BankApp.ViewModels
{
    public class AdminCreateCustomerViewModel
    {
        [Required]
        [StringLength(6)]
        public string Gender { get; set; }
        [Required]
        [StringLength(100)]
        public string Givenname { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(15)]
        public string Zipcode { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; }
        [StringLength(10)]
        public string Telephonecountrycode { get; set; }
        [StringLength(25)]
        public string Telephonenumber { get; set; }
        [StringLength(100)]
        public string Emailaddress { get; set; }
    }
}
