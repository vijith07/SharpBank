using Microsoft.IdentityModel.Tokens;
using SharpBank.Data;
using SharpBank.Models;
using SharpBank.Models.Enums;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public string Authenticate(Guid accountId ,string password)
        {
            Account account = appDbContext.Accounts.SingleOrDefault(a => a.Id == accountId);
            if (account == null)
                return null;
            bool res=BCrypt.Net.BCrypt.Verify(password,account.Password);
            if(!res)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey=Encoding.ASCII.GetBytes("Kurzgesagt – In a Nutshell");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name,account.Id.ToString()) ,
                        new Claim(ClaimTypes.Role,account.Type.ToString())
                    }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
