using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetAll()
        {
            return _dbContext.Customers.AsQueryable();
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Include(c=>c.Dispositions).ThenInclude(d=>d.Account).ThenInclude(a=>a.Transactions).FirstOrDefault(c => c.CustomerId == id);
        }

        public List<Account> GetAccountList(int customerId)
        {
            var customer = _dbContext.Customers.Include(a => a.Dispositions).First(c => c.CustomerId == customerId);
           
            var list = new List<Account>();
            foreach (var disposition in customer.Dispositions)
            {
                var account = _dbContext.Accounts.First(a => a.AccountId == disposition.AccountId);
                list.Add(account);
            }

            return list;
        }
    }
}