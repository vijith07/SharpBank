using SharpBank.Data;
using SharpBank.Models;
using SharpBank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class BankService : IBankService
    {
        private readonly AppDbContext appDbContext;

        public BankService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Bank CreateBank(Bank bank)
        {
            appDbContext.Banks.Add(bank);
            appDbContext.SaveChanges();
            return bank;
        }

        public Bank DeleteBank(Guid Id)
        {
            var bank = appDbContext.Banks.FirstOrDefault(bank => bank.Id == Id);
            appDbContext.Banks.Remove(bank);
            appDbContext.SaveChanges();
            return bank;

        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return appDbContext.Banks
                .Include(b => b.Currencies)
             .Include(b => b.DefaultCurrency)
                .ToList();    
            //.Include(b => b.Currencies)
            //    .Include(b => b.DefaultCurrency)
            //    .Include(b => b.Accounts)
            //    .ThenInclude(a => new { a.DebitTransactions, a.CreditTransactions })
        }

        public Bank GetBank(Guid bankId)
        {
            return appDbContext.Banks.FirstOrDefault(b => b.Id == bankId);  
        }

        public Bank UpdateBank(Bank bank)
        {
            appDbContext.Banks.Attach(bank);
            appDbContext.SaveChanges();
            return bank;
        }
    }
}
