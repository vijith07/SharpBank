using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;

namespace SharpBank.Services.Interfaces
{
    public interface IAccountService
    {
        public Account GetAccount(Guid id);
        
        public Account CreateAccount(Account account);

        public Account UpdateAccount(Account account);

        public Account DeleteAccount(Guid id);

        public IEnumerable<Account> GetAllAccounts();

        public bool Authenticate(Account account,string password);
    }
}
