using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedLibrary.Models;
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

        [HttpGet]
        public IActionResult Credit(int id)
        {
            var viewModel = new TransactionCreditViewModel
            {
                AccountId = id, Date = DateTime.Now, OperationItems = GetCreditOperations()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Credit(TransactionCreditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    AccountId = viewModel.AccountId,
                    Amount = viewModel.Amount,
                    Date = viewModel.Date,
                    Type = "Credit",
                    Operation = viewModel.SelectedOperation == 0 ? "Credit" : "Credit in cash"
                };
                _dbContext.Transactions.Add(transaction);

                var account = _dbContext.Accounts.First(a => a.AccountId == transaction.AccountId);
                account.Balance += transaction.Amount;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            viewModel.OperationItems = GetCreditOperations();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Debit(int id)
        {
            var viewModel = new TransactionDebitViewModel
            {
                AccountId = id, Date = DateTime.Now, OperationItems = GetDebitOperations()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Debit(TransactionDebitViewModel viewModel)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == viewModel.AccountId);

            if (viewModel.Amount > account.Balance)
            {
                ModelState.AddModelError("Amount", "Summan får inte överskrida kontots saldo");
            }

            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    AccountId = viewModel.AccountId,
                    Amount = -viewModel.Amount,
                    Date = viewModel.Date,
                    Type = "Debit",
                    Operation = viewModel.SelectedOperation == 0 ? "Credit card withdrawal" : "Withdrawal in cash"
                };
                _dbContext.Transactions.Add(transaction);

                account.Balance += transaction.Amount;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            viewModel.OperationItems = GetDebitOperations();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Transfer(int id)
        {
            var viewModel = new TransactionTransferViewModel
            {
                AccountId = id, Date = DateTime.Now
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Transfer(TransactionTransferViewModel viewModel)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == viewModel.AccountId);
            if (viewModel.Amount > account.Balance)
            {
                ModelState.AddModelError("Amount", "Summan får inte överskrida kontots saldo");
            }

            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    AccountId = viewModel.AccountId,
                    Amount = -viewModel.Amount,
                    Date = viewModel.Date,
                    Type = "Debit",
                    Operation = "Remittance to another bank",
                    Bank = viewModel.Bank,
                    Account = viewModel.Account
                };
                _dbContext.Transactions.Add(transaction);

                account.Balance += transaction.Amount;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        private static List<SelectListItem> GetCreditOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit", "0"),
                new("Credit in cash", "1"),
            };
            return l;
        }

        private static List<SelectListItem> GetDebitOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit card withdrawal", "0"),
                new ("Withdrawal in cash", "1"),
            };
            return l;
        }
    }
}
