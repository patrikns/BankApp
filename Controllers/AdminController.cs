using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;

        public AdminController(BankAppDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModel = new AdminIndexViewModel {Accounts = _dbContext.Accounts.ToList()};
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            var viewModel = new AdminCreateUserViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser()
        {
            var viewModel = new AdminEditUserViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Cashier")]
        public IActionResult CreateCustomer()
        {
            var viewModel = new AdminCreateCustomerViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Cashier")]
        public IActionResult EditCustomer()
        {
            var viewModel = new AdminEditCustomerViewModel();
            return View(viewModel);
        }
    }
}
