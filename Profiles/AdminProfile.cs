using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Uppgift2BankApp.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminCreateUserViewModel, IdentityUser>();
            CreateMap<AdminEditUserViewModel, IdentityUser>();
            CreateMap<IdentityUser, AdminEditUserViewModel>();
        }
    }
}
