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
        public static string GenerateAccountNumber(string ifsc)
        {
            return (BankManager.Banks[ifsc].Count + 1).ToString();
        }
        
        public static string AddAccount(string ifsc, string name, string password)
        {
            try
            {
                string number = GenerateAccountNumber(ifsc);
                Account acc = new Account(number, name, password);
                BankManager.Banks[ifsc].addAccount(acc);
                return number;
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
        }

        public static void RemoveAccount(string ifsc, string accountNumber )
        {
            try
            {
                BankManager.Banks[ifsc].RemoveAccount(accountNumber);
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
