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

        public static Account AddAccount(Account acc)
        {
            Database.Accounts.Add(acc);
            return acc;
        }
        public static string GenerateAccountNumber(string ifsc)
        {
            return "00" + (Database.Accounts.Count + 1).ToString();
        }

        public static List<Account> GetAccounts()
        {
            return Database.Accounts;
        }

        public static Account GetAccount(string ifsc, string accountNumber)
        {
            foreach(Account a in AccountServices.GetAccounts())
            {
                if (a.AccountNumber == accountNumber && a.IFSC == ifsc)
                {
                    return a;
                }
            }
            return null; 
        }
        public static void UpdateAccount(Account acc)
        {
            foreach (Account a in Database.Accounts)
            {
                if (a.AccountNumber == acc.AccountNumber && a.IFSC == acc.IFSC)
                {
                    a.Balance = acc.Balance;
                    a.UserName = acc.UserName;
         
                }
            }
            
        }
        public static void RemoveAccount(string ifsc, string accountNumber)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
            catch (ArgumentException e)
            {
                throw new AccountNumberException();
            }
        }

    }
}
