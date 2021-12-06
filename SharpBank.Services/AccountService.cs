using SharpBank.Data;
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
        private readonly AppDbContext appDbContext;

        public AccountService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public bool Authenticate(Account account, string password)
        {
            return BCrypt.Net.BCrypt.Verify(account.Password, password);
        }

        

        public Account CreateAccount( Account account)
        {
            appDbContext.Accounts.Add(account);
            appDbContext.SaveChanges();
            var createdAccount=appDbContext.Accounts.FirstOrDefault(a=>a.Id==account.Id);
            return createdAccount;
        }

      

        public Account DeleteAccount(Guid bankId, Guid id)
        {
            var acc = GetAccount(bankId, id);
            appDbContext.Accounts.Remove(acc);
            appDbContext.SaveChanges();
            return acc;
        }

        public Account GetAccount(Guid bankId,Guid acccountId)
        {
            return appDbContext.Accounts.FirstOrDefault(a => (a.Id == acccountId) && (a.BankId==bankId));
            
        }

        

        public IEnumerable<Account> GetAllAccounts(Guid bankId)
        {
            return appDbContext.Accounts.Where(a=>a.BankId==bankId).ToList();
        }

   

        public Account UpdateAccount( Account account)
        {
            appDbContext.Accounts.Attach(account);
            appDbContext.SaveChanges();
            var updatedAccount = appDbContext.Accounts.FirstOrDefault(a => a.Id == account.Id);
            return updatedAccount;
        }
    }
}
