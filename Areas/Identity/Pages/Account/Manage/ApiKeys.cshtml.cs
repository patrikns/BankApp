using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Uppgift2BankApp.Areas.Identity.Pages.Account.Manage
{
    public class ApiKeysModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ApiKeysModel> _logger;
        private readonly BankAppDataContext _dbContext;
        private string SecretKey = "sdfdsfskjkj234343jk:SDsd";
        private string Issuer = "patriksbankapp";

        [Required, BindProperty]
        public int CustomerId { get; set; }
        public string GeneratedToken { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public ApiKeysModel (
            UserManager<IdentityUser> userManager,
            ILogger<ApiKeysModel> logger, BankAppDataContext dbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (User.IsInRole("Admin"))
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerId", "Kunden finns inte");
                    return Page();
                }
                GeneratedToken = GenerateJSONWebToken(customer.Emailaddress);
            }

            StatusMessage = $"{GeneratedToken}";
            return RedirectToPage();
        }

        private string GenerateJSONWebToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("roles", "Customer")
            };

            var token = new JwtSecurityToken(Issuer,
                Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}