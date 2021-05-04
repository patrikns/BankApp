using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;

        public TransactionController(BankAppDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult New(int id)
        {
            var viewModel = new TransactionNewViewModel
            {
                AccountId = id, Date = DateTime.Now, OperationItems = GetOperations()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New(TransactionNewViewModel viewModel)
        {
            return View(viewModel);
        }

        private List<SelectListItem> GetOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit", "0"),
                new("Credit in cash", "1"),
                new("Credit card withdrawal", "2"),
                new ("Withdrawal in cash", "3"),
                new("Remittance to another bank", "4")
            };
            return l;
        }
    }
}
