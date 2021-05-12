using AutoMapper;
using SearchApp;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerInfoViewModel>();
            CreateMap<Customer, CustomerInAzure>();
            CreateMap<AdminCreateCustomerViewModel, Customer>();
            CreateMap<Customer, AdminEditCustomerViewModel>();
            CreateMap<AdminEditCustomerViewModel, Customer>();
        }
    }
}
