﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uppgift2BankApp.ViewModels
{
    public class TransactionDebitViewModel
    {
        public int SelectedOperation { get; set; }
        public List<SelectListItem> OperationItems { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}