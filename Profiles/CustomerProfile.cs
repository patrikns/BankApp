using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerInfoViewModel>();
            CreateMap<AdminCreateCustomerViewModel, Customer>();
            CreateMap<Customer, AdminEditCustomerViewModel>();
            CreateMap<AdminEditCustomerViewModel, Customer>();
        }
    }
}
