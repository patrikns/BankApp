using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uppgift2BankApp.Services.Transaction;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
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
            if (_transactionService.AmountIsNegative(viewModel.Amount))
            {
                ModelState.AddModelError("Amount", "Summan måste vara minst 0 SEK");
            }

            if (ModelState.IsValid)
            {
                _transactionService.AddCredit(viewModel);
                return RedirectToAction("Index", "Home");
            }

            viewModel.OperationItems = GetCreditOperations();
            return View(viewModel);
        }

        public List<SelectListItem> GetCreditOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit", "0"),
                new("Credit in cash", "1"),
            };
            return l;
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
            if (_transactionService.ExceedsBalance(viewModel.AccountId, viewModel.Amount))
            {
                ModelState.AddModelError("Amount", "Summan får inte överskrida kontots saldo");
            }

            if (_transactionService.AmountIsNegative(viewModel.Amount))
            {
                ModelState.AddModelError("Amount", "Summan måste vara minst 0 SEK");
            }

            if (ModelState.IsValid)
            {
                _transactionService.Withdrawal(viewModel);
                return RedirectToAction("Index", "Home");
            }

            viewModel.OperationItems = GetDebitOperations();
            return View(viewModel);
        }

        public List<SelectListItem> GetDebitOperations()
        {
            var l = new List<SelectListItem>
            {
                new("Credit card withdrawal", "0"),
                new ("Withdrawal in cash", "1"),
            };
            return l;
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
            if (_transactionService.ExceedsBalance(viewModel.AccountId, viewModel.Amount))
            {
                ModelState.AddModelError("Amount", "Summan får inte överskrida kontots saldo");
            }

            if (_transactionService.AmountIsNegative(viewModel.Amount))
            {
                ModelState.AddModelError("Amount", "Summan måste vara minst 0 SEK");
            }

            if (ModelState.IsValid)
            {
               _transactionService.Transfer(viewModel);
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }
    }
}
