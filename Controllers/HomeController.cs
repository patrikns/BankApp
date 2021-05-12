using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BankAppDataContext _dbContext;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, BankAppDataContext dbContext)
        {
            _logger = logger;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.CustomerCount = _dbContext.Customers.Count();
            viewModel.AccountCount = _dbContext.Accounts.Count();
            viewModel.TotalBalance = CalculateTotalBalance();
            return View(viewModel);
        }

        private decimal CalculateTotalBalance()
        {
            decimal sum = 0;
            foreach (var a in _dbContext.Accounts)
            {
                sum += a.Balance;
            }

            return sum;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index");
        }
    }
}
