using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
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

        public IActionResult Search(string q)
        {
            var viewModel = new CustomerSearchViewModel();
            return View(viewModel);
        }

        public IActionResult SearchByCustomerId()
        {
            var viewModel = new CustomerSearchByCustomerIdViewModel();
            return View(viewModel);
        }

        public IActionResult SearchByCustomerId(CustomerSearchByCustomerIdViewModel viewModel)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == viewModel.CustomerId);

            return View(viewModel);
        }
    }
}