using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using Uppgift2BankApp.Services.CustomerService;
using Uppgift2BankApp.Services.StatisticsService;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStatisticsService _statisticsService;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, IStatisticsService statisticsService, ICustomerService customerService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _statisticsService = statisticsService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel 
                {Items = _statisticsService.GetListOfCountryStatistics()};

            return View(viewModel);
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

        public IActionResult TopTen(string countryCode)
        {
            var viewModel = new HomeTopTenViewModel();
            var list = _statisticsService.GetTopTenAccountsByCountryCode(countryCode);
            viewModel.Items = list.Select(a=> new HomeTopTenViewModel.Item
            {
                CustomerId = a.Dispositions.First(d=>d.Type=="OWNER").CustomerId,
                Balance = a.Balance,
                Name = _customerService.GetCustomerById(a.Dispositions.First(d=>d.Type=="OWNER").CustomerId).Givenname + " " + _customerService.GetCustomerById(a.Dispositions.First(d=>d.Type=="OWNER").CustomerId).Surname
            }).ToList();
            return View(viewModel);
        }
    }
}
