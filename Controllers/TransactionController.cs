using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using SharedLibrary.Models;
using Uppgift2BankApp.Services.TransactionService;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionController(BankAppDataContext dbContext, IMapper mapper, ITransactionService transactionService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Credit(int id)
        {
            var viewModel = _transactionService.Credit(id);
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

            viewModel.OperationItems = _transactionService.GetCreditOperations();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Debit(int id)
        {
            var viewModel = _transactionService.Debit(id);
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

            viewModel.OperationItems = _transactionService.GetDebitOperations();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Transfer(int id)
        {
            var viewModel = _transactionService.Transfer(id);
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
    }
}
