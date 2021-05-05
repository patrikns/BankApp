using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uppgift2BankApp.ViewModels
{
    public class AdminCreateUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public int SelectedRoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
