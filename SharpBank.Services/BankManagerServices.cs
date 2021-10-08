using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
   public static class BankManagerServices
    {
        private static string GenerateIFSC()
        {
            string code = (BankManager.Banks.Count + 1).ToString();
            return code;
        }
        public static string AddBank(string name)
        {
            Bank bank = new Bank(name, GenerateIFSC());
            BankManager.Banks.Add(bank.IFSC,bank);
            return bank.IFSC;
        }
        public static Dictionary<string,Bank> GetBanks() {
            return BankManager.Banks;
        }
    }
}
