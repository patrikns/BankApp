using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.StatisticsService
{
    class StatisticsService : IStatisticsService
    {
        private readonly BankAppDataContext _dbContext;

        public StatisticsService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetCustomersByCountryCode(string code)
        {
            return _dbContext.Customers.Where(c => c.CountryCode == code);
        }

        public List<Account> GetCustomerAccountsByCountryCode(string code)
        {
            var list = new List<Account>();
            foreach (var c in GetCustomersByCountryCode(code).Include(c=>c.Dispositions).ThenInclude(d=>d.Account))
            {
                var a = c.Dispositions.Where(d=>d.Type=="OWNER").Select(d=>d.Account);
                list.AddRange(a);
            }

            return list;
        }

        public decimal GetTotalBalanceByCountryCode(string code)
        {
            return GetCustomerAccountsByCountryCode(code).Sum(a => a.Balance);
        }

        public IEnumerable<Account> GetTopTenAccountsByCountryCode(string code)
        { 
            var accounts = GetCustomerAccountsByCountryCode(code).OrderByDescending(a => a.Balance).Take(10);
            return accounts;
        }

        public List<HomeIndexViewModel.CountryStatisticViewModel> GetListOfCountryStatistics()
        {
            var arr = new[] {"SE", "NO", "FI", "DK"};

            return arr.Select(countryCode => new HomeIndexViewModel.CountryStatisticViewModel
                {
                    Country = _dbContext.Customers.First(c => c.CountryCode == countryCode).Country,
                    CountryCode = countryCode,
                    CustomerCount = GetCustomersByCountryCode(countryCode).Count(),
                    AccountCount = GetCustomerAccountsByCountryCode(countryCode).Count(),
                    TotalBalance = GetTotalBalanceByCountryCode(countryCode)
                })
                .ToList();
        }
    }
}