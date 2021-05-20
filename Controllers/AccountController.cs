using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
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
            var disposition = _dbContext.Dispositions.First(d => d.CustomerId == id);
            var model = _dbContext.Accounts.First(a=>a.AccountId == disposition.AccountId);
            viewModel.AccountId = model.AccountId;
            viewModel.Balance = model.Balance;
            viewModel.Items = _dbContext.Transactions.Where(t => t.AccountId == model.AccountId).OrderByDescending(t=>t.Date).Select(t => new TransactionRowViewModel()
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
            var disposition = _dbContext.Dispositions.First(d => d.CustomerId == id);
            var model = _dbContext.Accounts.First(a => a.AccountId == disposition.AccountId);
            viewModel.Items = _dbContext.Transactions.Where(t => t.AccountId == id).OrderByDescending(t=>t.Date).Select(t => new TransactionRowViewModel()
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
