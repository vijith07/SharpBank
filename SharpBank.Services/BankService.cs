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
            
        }
        public string GenerateId(string name)
        { 
            string Id;
            do
            {
                string timestamp = DateTime.UtcNow.ToString("yyMMddhmsf",
                                        System.Globalization.CultureInfo.InvariantCulture);
                while (name.Length < 3)
                {
                    name = name + '0';
                }
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
                Password = "ADMIN".GetHashCode().ToString(),
                Balance = 0m,
                Gender = Models.Enums.Gender.Other,
                Status = Models.Enums.Status.Active,
                Type = Models.Enums.AccountType.Staff,
                Transactions = new List<Transaction>()
            };
            Account Cash = new Account
            {
                Id = "CASH",
                Name = "CASH",
                Password = "CASH".GetHashCode().ToString(),
                Balance = 10000000000000000000000000000m,
                Gender = Models.Enums.Gender.Other,
                Status = Models.Enums.Status.Active,
                Type = Models.Enums.AccountType.Customer,
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
                DefaultCurrency = datastore.Currencies.SingleOrDefault(c => c.Code == "INR"),
                Currencies = new List<Currency>(),
                Accounts = new List<Account>(),
                

            };
            bank.Accounts.Add(Admin);
            bank.Accounts.Add(Cash);
            bank.Currencies.Add(datastore.Currencies.SingleOrDefault(c => c.Code == "INR"));
            datastore.Banks.Add(bank);

            return bank.Id;
        }

        public Bank GetBank(string bankId)
        {

            Bank b= datastore.Banks.SingleOrDefault(b => b.Id == bankId);
            if (b == null)
            {
                throw new BankIdException();
            }
            return b;
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
        public void AddCurrency(string bankId,string code)
        {
            Bank bank = GetBank(bankId);
            Currency curr  = datastore.Currencies.SingleOrDefault(c => c.Code == code);

            if ( curr== null) {
                throw new InvalidCurrencyException();
            }
           
            if (bank.Currencies.SingleOrDefault(c => c.Code == code)!=null)
            {
                throw new CurrencyExistsException();
            }
            bank.Currencies.Add(curr);
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
        public List<Currency> GetAcceptedCurrencies(string bankId)
        {
            Bank bank = GetBank(bankId);
            return bank.Currencies;
        }
        public Currency GetDefaultCurrency(string bankId)
        {
            Bank bank = GetBank(bankId);
            return bank.DefaultCurrency;
        }
    }
}
    