﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Uppgift2BankApp.Areas.Identity.Pages.Account.Manage
{
    public class ApiKeysModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ApiKeysModel> _logger;

        public ApiKeysModel (
            UserManager<IdentityUser> userManager,
            ILogger<ApiKeysModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        private string GenerateJSONWebToken(string email)
        {

        }
    }
}