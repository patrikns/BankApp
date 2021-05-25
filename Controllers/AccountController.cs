using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
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
            var model = _dbContext.Dispositions.Include(d=>d.Account).ThenInclude(a=>a.Transactions).First(d=> d.CustomerId == id).Account;
            viewModel.AccountId = model.AccountId;
            viewModel.Balance = model.Balance;
            viewModel.Items = model.Transactions.OrderByDescending(t=>t.Date).Select(t => new TransactionRowViewModel()
                {
                    Date = t.Date.ToShortDateString(),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount
                }).Take(20).ToList();
            viewModel.CustomerId = id;
            return View(viewModel);
        }

        public IActionResult GetTransactionsFrom(int id, int skip)
        {
            var viewModel = new AccountGetTransactionsFromViewModel();
            var model = _dbContext.Dispositions.Include(d=>d.Account).ThenInclude(a=>a.Transactions).First(d => d.CustomerId==id).Account;
            viewModel.Items = model.Transactions.OrderByDescending(t=>t.Date).Select(t => new TransactionRowViewModel()
                {
                    Date = t.Date.ToShortDateString(),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount
                }).Skip(skip).Take(20).ToList();
            return View(viewModel);
        }
    }
}
