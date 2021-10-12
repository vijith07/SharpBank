using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public class AccountServices
    {

        public long AddAccount(String name,String password,long BankId)
        {
            Account account = new Account
            {
                Id = GenerateId(BankId),
                Name = name,
                Password = name,
                Balance = 0m,
                Gender = Models.Enums.Gender.Other,
                Status = Models.Enums.Status.Active,
                Transactions = new List<Transaction>()
            };
            Bank bank = Database.Banks.FirstOrDefault(b => b.Id == BankId);
            bank.Accounts.Add(account);
            return account.Id;
        }
        public long GenerateId(long BankId)
        {
            Random rand = new Random();
            long Id =rand.Next(); ;
            Bank bank = Database.Banks.FirstOrDefault(b => b.Id == BankId);
            while (bank.Accounts.SingleOrDefault(acc => acc.Id == Id)!=null)
            {
                Id = rand.Next();
            }
            
            return Id;
        }

        public void UpdateBalance(long BankId,long AccountId,decimal Balance)
        {
            Bank bank = Database.Banks.FirstOrDefault(b => b.Id == BankId);
            Account acc=bank.Accounts.SingleOrDefault(acc => acc.Id == AccountId);
            acc.Balance = Balance;

        }

    }
}
