using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public class BankServices
    {
        public long GenerateId()
        {
            Random rand = new Random();
            long Id = rand.Next(); ;
            
            while (Database.Banks.FirstOrDefault(b => b.Id == Id)!=null)
            {
                Id = rand.Next();
            }
            return Id;
        }

        public long AddBank(string name)
        {
            Bank bank = new Bank
            {
                Id = GenerateId(),
                Name = name,
                CreatedBy = "Snake Babu",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Snake Babu",
                UpdatedOn = DateTime.UnixEpoch,
                Accounts = new List<Account>()
            };
            Database.Banks.Add(bank);
            return bank.Id;
;        }

        

    }
}
