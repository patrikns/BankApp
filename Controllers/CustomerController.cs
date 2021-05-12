using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Azure.Search.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchApp;
using SharedLibrary.Models;
using Uppgift2BankApp.Services.SearchService;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAzureSearch _searcher;

        public CustomerController(BankAppDataContext dbContext, IMapper mapper, IAzureSearch searcher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _searcher = searcher;
        }

        public IActionResult Index(string q, int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();
            var searchClient = _searcher.GetSearchClient();

            int pageSize = 50;
            int skip = (page - 1) * pageSize;

            var searchOptions = new SearchOptions
            {
                Skip = skip,
                Size = pageSize,
                IncludeTotalCount = true
            };
            var searchResult = searchClient.Search<CustomerInAzure>(q, searchOptions);
            var ids = searchResult.Value.GetResults().Select(result => result.Document.Id);

            var query =
                _dbContext.Customers.Where(c => ids.Contains(c.CustomerId.ToString()));

            var totalRowCount = searchResult.Value.TotalCount;
            var pageCount = (double) totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

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