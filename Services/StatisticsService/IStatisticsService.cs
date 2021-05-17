using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Search.Documents;
using Microsoft.EntityFrameworkCore.Query;
using SharedLibrary.Models;
using Uppgift2BankApp.ViewModels;

namespace Uppgift2BankApp.Services.StatisticsService
{
    public interface IStatisticsService
    {
        IQueryable<Customer> GetCustomersByCountryCode(string code);
        List<Account> GetCustomerAccountsByCountryCode (string code);
        decimal GetTotalBalanceByCountryCode(string code);
        IEnumerable<Account> GetTopTenAccountsByCountryCode(string code);
        List<HomeIndexViewModel.CountryStatisticViewModel> GetListOfCountryStatistics();
    }
}
