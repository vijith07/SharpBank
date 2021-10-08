using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    class AccountServices
    {
        //Returns the account number generated
        public string AddAccount(string name,string bankName, string ifsc, string password ) {
            try
            {
                string number = (BankManager.Banks[ifsc].Count + 1).ToString();
                Account acc = new Account(number, name, password);
                BankManager.Banks[ifsc].addAccount(acc);
                return number;
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
        }

        public void RemoveAccount(string accountNumber,string ifsc) {
            try
            {
                BankManager.Banks[ifsc].RemoveAccount(accountNumber);
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
            catch (ArgumentException e) {
                throw new AccountNumberException();
            }
        }

    }
}
