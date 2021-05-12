using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerController(BankAppDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(string q, int page = 1)
        {
            
            var viewModel = new CustomerIndexViewModel();
            var query =
                _dbContext.Customers.Where(c =>q == null ||
                    c.City.Contains(q) || c.Givenname.Contains(q) || c.Surname.Contains(q));

            var totalRowCount = query.Count();
            int pageSize = 50;
            int howManyRecordsToSkip = (page - 1) * pageSize;
            var pageCount = (double) totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            query = query.Skip(howManyRecordsToSkip).Take(pageSize);

            viewModel.Items = query.Select(c => new CustomerIndexViewModel.Item
            {
                CustomerId = c.CustomerId,
                NationalId = c.NationalId,
                Name = c.Givenname + " " + c.Surname,
                Streetaddress = c.Streetaddress,
                City = c.City
            }).ToList();
            viewModel.Q = q;
            viewModel.Page = page;
            return View(viewModel);
        }

        public IActionResult Info(int id)
        {
            var model = _dbContext.Customers.Include(c=>c.Dispositions).FirstOrDefault(c => c.CustomerId == id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = _mapper.Map<CustomerInfoViewModel>(model);
            viewModel.Name = model.Givenname + " " + model.Surname;

            var accounts = new List<Account>();
            foreach (var disposition in model.Dispositions)
            {
                var account = _dbContext.Accounts.First(a => a.AccountId == disposition.AccountId);
                accounts.Add(account);
                viewModel.TotalBalance += account.Balance;
            }

            viewModel.Accounts = accounts.Select(a => new CustomerInfoViewModel.Item
            {
                AccountId = a.AccountId,
                Balance = a.Balance
            }).ToList();


            return View(viewModel);
        }
    }
}