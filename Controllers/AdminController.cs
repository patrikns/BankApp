using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uppgift2BankApp.Data;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _userDbContext;

        public AdminController(BankAppDataContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager, ApplicationDbContext userDbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
            _userDbContext = userDbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new AdminIndexViewModel();
            return View(viewModel);
        }

        public IActionResult UserIndex()
        {
            var viewModel = new AdminUserIndexViewModel();
            viewModel.Items = _userDbContext.Users.Select(u => new AdminUserIndexViewModel.Item
            {
                Id = u.Id,
                UserName = u.UserName,
                Role = _userDbContext.Roles.First(r=>r.Id==(_userDbContext.UserRoles.First(r=>r.UserId==u.Id).RoleId)).Name
            }).ToList();
            return View(viewModel);

        }

        public IActionResult CustomerIndex(string q, int page = 1)
        {
            var viewModel = new AdminCustomerIndexViewModel();

            var query =
                _dbContext.Customers.Where(c =>q == null ||
                                               c.City.Contains(q) || c.Givenname.Contains(q) || c.Surname.Contains(q));

            var totalRowCount = query.Count();
            int pageSize = 50;
            int howManyRecordsToSkip = (page - 1) * pageSize;
            var pageCount = (double) totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            query = query.Skip(howManyRecordsToSkip).Take(pageSize);

            viewModel.Items = query.Select(c => new AdminCustomerIndexViewModel.Item
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

        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            var viewModel = new AdminCreateUserViewModel();
            viewModel.Roles = GetRoles();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult CreateUser(AdminCreateUserViewModel viewModel)
        {
            if (_userManager.FindByEmailAsync(viewModel.UserName).Result != null)
            {
                ModelState.AddModelError("UserName", "Användaren finns redan");
            }

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<IdentityUser>(viewModel);
                user.EmailConfirmed = true;
                string[] roles = viewModel.SelectedRoleId == 0 ? new string[] {"Admin"} : new string[] {"Cashier"};
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                var unused = _userManager.CreateAsync(user, viewModel.Password).Result;
                var dummy = _userManager.AddToRolesAsync(user, roles).Result;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                return RedirectToAction("Index");
            }
            viewModel.Roles = GetRoles();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(string id)
        {
            var model = _userDbContext.Users.First(u => u.Id == id);
            var viewModel = _mapper.Map<AdminEditUserViewModel>(model);
            viewModel.Roles = GetRoles();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult EditUser(AdminEditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<IdentityUser>(viewModel);
                user.EmailConfirmed = true;
                string[] roles = viewModel.SelectedRoleId == 0 ? new string[] {"Admin"} : new string[] {"Cashier"};
                _userManager.UpdateAsync(user);
                _userManager.AddToRolesAsync(user, roles);
                return RedirectToAction("Index");
            }
            viewModel.Roles = GetRoles();
            return View(viewModel);
        }

        
        private List<SelectListItem> GetRoles()
        {
            var l = new List<SelectListItem>
            {
                new("Admin", "0"),
                new("Cashier", "1"),
            };
            return l;
        }

        [Authorize(Roles = "Cashier")]
        public IActionResult CreateCustomer()
        {
            var viewModel = new AdminCreateCustomerViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Cashier"), HttpPost]
        public IActionResult CreateCustomer(AdminCreateCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(viewModel);
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var model = _dbContext.Customers.First(c => c.CustomerId == id);
            var viewModel = _mapper.Map<AdminEditCustomerViewModel>(model);
            return View(viewModel);
        }

        [Authorize(Roles = "Cashier"), HttpPost]
        public IActionResult EditCustomer(AdminEditCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(viewModel);
                _dbContext.Customers.Update(customer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
