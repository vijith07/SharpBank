using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    class BankServices
    {
        //Returns the account number generated
        public string GenerateAccountNumber(string ifsc)
        {
            return (BankManager.Banks[ifsc].Count + 1).ToString();
        }
        
        public string AddAccount(string name, string bankName, string ifsc, string password)
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

        public void RemoveAccount(string accountNumber, string ifsc)
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
