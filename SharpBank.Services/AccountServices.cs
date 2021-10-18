
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Enums;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public class AccountServices
    {
        private readonly BankService bankService;

        public AccountServices(BankService bankService)
        {
            this.bankService = bankService;
            Account acc = new Account
            {
                Name = "Cash",
                Gender = Gender.Other,
                Id = 0,
                Balance = 0m,
                Status = Status.Active,
                Transactions = new List<Transaction>()
            };
            bankService.GetBank(0).Accounts.Add(acc);
        }
        public Account GetAccount(long bankId, long accountId)
        {
            return bankService.GetBank(bankId).Accounts.SingleOrDefault(a => a.Id == accountId);
        }
        public decimal GetBalance(long BankId, long AccountId)
        {
            Account acc = GetAccount(BankId, AccountId);
            return acc.Balance;
        }
        public long AddAccount(String name,long BankId,Gender gender)
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
            bankService.GetBank(BankId).Accounts.Add(account);
            return account.Id;
        }
        public long GenerateId(long bankId)
        {
            Random rand = new Random(69);
            Bank bank = bankService.GetBank(bankId);
            long Id;
            do
            {
                Id = rand.Next();
            }

            while (bank.Accounts.SingleOrDefault(a => a.Id == Id) != null);
            return Id;
        }

        public void UpdateBalance(long BankId,long AccountId,decimal Balance)
        {
            Account acc = GetAccount(BankId, AccountId);
            acc.Balance = Balance;

        }
        public void RemoveAccount(long bankId, long accountId)
        {
            bankService.GetBank(bankId).Accounts.Remove(GetAccount(bankId, accountId));
        }

    }
}
