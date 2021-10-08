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
        public List<Transaction> GetTransactionHistory(string accountNumber, string ifsc) {
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
        public decimal GetBalance(string accountNumber, string ifsc)
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
