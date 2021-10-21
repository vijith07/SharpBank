using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public class BankService
    {
        private readonly Datastore datastore;

        public BankService(Datastore datastore)
        {
            this.datastore = datastore;
            datastore.Banks.Add(new Bank { Id = "RBI0000000000", Name = "Reserve Bank", Accounts = new List<Account> { } });
        }
        public string GenerateId(string name)
        { 
            string Id;
            do
            {
                string timestamp = DateTime.UtcNow.ToString("yyMMddhmsf",
                                        System.Globalization.CultureInfo.InvariantCulture);
                Id = name.Substring(0, 3) + timestamp;

            }
            while (datastore.Banks.SingleOrDefault(b => b.Id == Id) != null);
            return Id;
        }
        
        public string AddBank(string name)
        {
            Account Admin = new Account
            {
                Id = "ADMIN",
                Name = "ADMIN",
                Password = "ADMIN",
                Balance = 0m,
                Gender = Models.Enums.Gender.Other,
                Status = Models.Enums.Status.Active,
                Type = Models.Enums.AccountType.Staff,
                Transactions = new List<Transaction>()
            };
            Bank bank = new Bank
            {
                Id = GenerateId(name),
                Name = name,
                CreatedOn = DateTime.Now,
                CreatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                IMPSToOther = 0.06m,
                RTGSToOther = 0.02m,
                IMPSToSame = 0.05m,
                RTGSToSame = 0m,
                DefaultCurrency = new Currency
                {
                    Name = "Indian Rupee",
                    Code = "INR",
                    ExchangeRate = 1m
                },
                Currencies = new List<Currency>(),
                Accounts = new List<Account>(),
                

            };
            bank.Accounts.Add(Admin);
            datastore.Banks.Add(bank);

            return bank.Id;
        }

        public Bank GetBank(string bankId)
        {

            return datastore.Banks.SingleOrDefault(b => b.Id == bankId);
        }
        public List<string> GetBanks()
        {
            List<string> banks = new List<string>();
            foreach (Bank bank in datastore.Banks)
            {
                string s = bank.Id + " | " + bank.Name;
                banks.Add(s);
            }
            return banks;
        }
        public void AddCurrency(string bankId,string name,String code,decimal exchangeRate)
        {
            Bank bank = GetBank(bankId);
            Currency currency = new Currency
            {
                Name = name,
                Code = code,
                ExchangeRate = exchangeRate,
            };
            if (bank.Currencies.SingleOrDefault(c => c.Code == code)!=null)
            {
                throw new CurrencyExistsException();
            }
            bank.Currencies.Add(currency);
        }
        public void UpdateSameRTGS(string bankId,decimal rate)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToSame = rate;
        }
        public void UpdateSameIMPS(string bankId, decimal rate)
        {
            Bank bank = GetBank(bankId);
            bank.IMPSToSame = rate;
        }
        public void UpdateOtherIMPS(string bankId, decimal rate)
        {
            Bank bank = GetBank(bankId);
            bank.IMPSToOther = rate;
        }
        public void UpdateOtherRTGS(string bankId, decimal rate)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToOther = rate;
        }
    }
}
    