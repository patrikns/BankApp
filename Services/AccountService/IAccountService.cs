using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace Uppgift2BankApp.Services.AccountService
{
    public interface IAccountService
    {
        Account GetAccountById(int id);
    }
}
