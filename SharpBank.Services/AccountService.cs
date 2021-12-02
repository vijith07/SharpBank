using SharpBank.Models;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class AccountService : IAccountService
    {
        public bool Authenticate(Account account, string password)
        {
            throw new NotImplementedException();
        }

        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Account UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
