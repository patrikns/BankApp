using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly BankAppDataContext _dbContext;

        public AdminController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new AdminIndexViewModel();
            viewModel.Accounts = _dbContext.Accounts.ToList();
            return View(viewModel);
        }

        public IActionResult CreateUser()
        {
            var viewModel = new AdminCreateUserViewModel();
            return View(viewModel);
        }

        public IActionResult EditUser()
        {
            var viewModel = new AdminEditUserViewModel();
            return View(viewModel);
        }
    }
}
