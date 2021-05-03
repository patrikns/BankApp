using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uppgift2BankApp.Models;
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

        public IActionResult Index(string q, int id)
        {
            var viewModel = new CustomerIndexViewModel();
            var query =
                _dbContext.Customers.Where(c =>q == null ||
                    c.City.Contains(q) || c.Givenname.Contains(q) || c.Surname.Contains(q));
            viewModel.Items = query.Select(c => new CustomerIndexViewModel.Item
            {
                CustomerId = c.CustomerId,
                Name = c.Givenname + " " + c.Surname,
                City = c.City,
                Country = c.Country
            }).ToList();
            viewModel.Q = q;
            return View(viewModel);
        }

        public IActionResult Info(int id)
        {
            var model = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var viewModel = _mapper.Map<CustomerInfoViewModel>(model);
            viewModel.Name = model.Givenname + " " + model.Surname;
            return View(viewModel);
        }
    }
}