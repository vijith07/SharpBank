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
        public static List<Transaction> GetTransactionHistory(string ifsc, string accountNumber) {
            try
            {
                BankManager.Banks[ifsc].getAccount(accountNumber);
                List<Transaction> res = new List<Transaction>();
                foreach (Transaction t in BankManager.Transactions)
                {
                    if (((t.SenderIFSC == ifsc) && (t.SenderAccount == accountNumber)) || ((t.RecepientIFSC == ifsc) && (t.RecepientAccount == accountNumber)))
                    {
                        res.Add(t);
                    }
                }
                return res;
            }
            catch (ArgumentException e)
            {
                throw new AccountNumberException();
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
        }
        public static decimal GetBalance(string ifsc, string accountNumber)
        {
            try
            {
                return BankManager.Banks[ifsc].getAccount(accountNumber).Balance;
            }
            catch (ArgumentException e)
            {
                throw new AccountNumberException();
            }
            catch (KeyNotFoundException e)
            {
                throw new IFSCException();
            }
        }
    }
}
