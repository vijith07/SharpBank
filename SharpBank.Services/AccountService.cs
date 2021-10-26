
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
    public class AccountService
    {
        private readonly BankService bankService;

        public AccountService(BankService bankService)
        {
            this.bankService = bankService;
   
        }
        public Account GetAccount(string bankId, string accountId)
        {
            Account acc= bankService.GetBank(bankId).Accounts.SingleOrDefault(a => a.Id == accountId);
            if(acc==null)
            {
                throw new AccountNumberException();
            }
            return acc;
        }
        public decimal GetBalance(string bankId, string accountId)
        {
            Account acc = GetAccount(bankId, accountId);
            return acc.Balance;
        }
        public string AddAccount(String name, string bankId, Gender gender, AccountType Type)
        {
            Account account = new Account
            {
                Id = GenerateId(bankId,name),
                Name = name,
                Password = (name.Substring(0,1)+"@123").GetHashCode().ToString(),
                Balance = 0m,
                Gender = Models.Enums.Gender.Other,
                Status = Models.Enums.Status.Active,
                Type=Type,
                Transactions = new List<Transaction>()
            };
            bankService.GetBank(bankId).Accounts.Add(account);
            return account.Id;
        }
        public string GenerateId(string bankId, String name)
        {
            Bank bank = bankService.GetBank(bankId);
            string Id;
            do
            {
                string timestamp = DateTime.UtcNow.ToString("yyMMddhmsf",
                                        System.Globalization.CultureInfo.InvariantCulture);
                Id = name.Substring(0, 3).ToUpper() + timestamp;

            }
            while (bank.Accounts.SingleOrDefault(b => b.Id == Id) != null);
            return Id;
        }

        public void UpdateBalance(string bankId, string accountId, decimal Balance)
        {
            Account acc = GetAccount(bankId, accountId);
            acc.Balance = Balance;

        }
        public void RemoveAccount(string bankId, string accountId)
        {
            bankService.GetBank(bankId).Accounts.Remove(GetAccount(bankId, accountId));
        }
        public bool ValidateUser(string bankId, string accountId,string password)
        {
            Account acc = GetAccount(bankId, accountId);
            if (acc.Password != password.GetHashCode().ToString()) {
                throw new PasswordIncorrectException();
            }
            return true;
        }
        public bool ValidateStaff(string bankId, string accountId, string password)
        {
            Account acc = GetAccount(bankId, accountId);
            if (acc.Type != AccountType.Staff)
            {
                throw new Models.Exceptions.UnauthorizedAccessException();
            }
            if (acc.Password != password.GetHashCode().ToString())
            {
                throw new PasswordIncorrectException();
            }
            return true;
        }
    }
}
