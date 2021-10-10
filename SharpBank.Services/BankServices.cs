using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public static class BankServices
    {
        //Returns the account number generated
        private static string GenerateIFSC()
        {
            string code = (Database.Banks.Count + 1).ToString();
            return code;
        }
        
        public static Bank AddBank(Bank bank)
        {
            Database.Banks.Add(bank);
            return bank;
        }

        public static Bank GetBank(string ifsc) {

            foreach (Bank b in Database.Banks) {
                if (b.IFSC == ifsc)
                {
                    return b;
                }
            }
            return null;
        }
        public static List<Bank> GetBanks()
        {
            return Database.Banks;
        }

    }
}
