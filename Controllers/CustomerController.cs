using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Azure.Search.Documents;
using Microsoft.AspNetCore.Mvc;
using SearchApp;
using SharedLibrary.Models;
using Uppgift2BankApp.Services.AccountService;
using Uppgift2BankApp.Services.CustomerService;
using Uppgift2BankApp.Services.SearchService;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IAzureSearch _searcher;

        public CustomerController(ICustomerService customerService, IMapper mapper, IAzureSearch searcher)
        {
            _customerService = customerService;
            _mapper = mapper;
            _searcher = searcher;
        }

        public IActionResult Index(string q, string sortField, string sortOrder, int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();
            var searchClient = _searcher.GetSearchClient();

            int pageSize = 50;
            int skip = (page - 1) * pageSize;

            if (string.IsNullOrEmpty(sortField)) sortField = "Name";
            if (string.IsNullOrEmpty(sortOrder)) sortOrder = "asc";

            var searchOptions = new SearchOptions
            {
                OrderBy = {sortField + " " + sortOrder},
                Skip = skip,
                Size = pageSize,
                IncludeTotalCount = true
            };
     
            var searchResult = searchClient.Search<CustomerInAzure>(q, searchOptions);
            var ids = searchResult.Value.GetResults().Select(result => result.Document.Id);

            var query =
                _customerService.GetAll().Where(c => ids.Contains(c.CustomerId.ToString()));

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
            viewModel.SortField = sortField;
            viewModel.SortOrder = sortOrder;
            viewModel.OppositeSortOrder = sortOrder == "asc" ? "desc" : "asc";
            viewModel.Page = page;
            return View(viewModel);
        }

        public IActionResult Info(int id)
        {
            var model = _customerService.GetCustomerById(id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = _mapper.Map<CustomerInfoViewModel>(model);  
            viewModel.Name = model.Givenname + " " + model.Surname;
            viewModel.Accounts = model.Dispositions.Where(d=>d.Type=="OWNER").Select(d => new CustomerInfoViewModel.Item
            {
                AccountId = d.Account.AccountId,
                Balance = d.Account.Balance
            }).ToList();
            viewModel.TotalBalance = model.Dispositions.Where(d => d.Type == "OWNER").Sum(d => d.Account.Balance);

            return View(viewModel);
        }
    }
}