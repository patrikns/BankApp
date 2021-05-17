using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.CustomerService
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetAll();
        Customer GetCustomerById(int id);
        List<Account> GetAccountList(int customerId);
    }
}
