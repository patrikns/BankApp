using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;

        public AccountController(BankAppDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            var viewModel = new AccountIndexViewModel();
            var disposition = _dbContext.Dispositions.First(d => d.CustomerId == id);
            var model = _dbContext.Accounts.First(a=>a.AccountId == disposition.AccountId);
            viewModel.AccountId = model.AccountId;
            viewModel.Balance = model.Balance;
            viewModel.Items = _dbContext.Transactions.Where(t => t.AccountId == model.AccountId).Take(20)
                .Select(t => new TransactionRowViewModel()
                {
                    Date = t.Date,
                    Type = t.Type,
                    Amount = t.Amount
                }).OrderByDescending(o=>o.Date).ToList();
            viewModel.CustomerId = disposition.CustomerId;
            return View(viewModel);
        }

        public IActionResult GetTransactionsFrom(int id, int skip)
        {
            var viewModel = new AccountGetTransactionsFromViewModel();
            var d = _dbContext.Dispositions.First(d => d.CustomerId == id);
            var model = _dbContext.Accounts.First(a => a.AccountId == d.AccountId);
            viewModel.Items = _dbContext.Transactions.Where(t => t.AccountId == id).Skip(skip).Take(20)
                .Select(t => new TransactionRowViewModel()
                {
                    Date = t.Date,
                    Type = t.Type,
                    Amount = t.Amount
                }).OrderByDescending(o=>o.Date).ToList();
            return View(viewModel);
        }
    }
}
