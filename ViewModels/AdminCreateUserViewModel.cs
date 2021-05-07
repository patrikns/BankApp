using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uppgift2BankApp.ViewModels
{
    public class AdminCreateUserViewModel
    {
        [EmailAddress]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        public int SelectedRoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
