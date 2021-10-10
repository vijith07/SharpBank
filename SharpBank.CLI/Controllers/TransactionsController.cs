using SharpBank.Models;
using SharpBank.Models.Exceptions;
using SharpBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.CLI.Controllers
{
    static class TransactionsController
    {
        public static Transaction Withdraw( Account acc, decimal amount)
        {
            try
            {
                if (acc.Balance < amount)
                {
                    throw new BalanceException();
                }
                acc.Balance -= amount;
                Transaction t = new Transaction
                {
                    TransactionID = "00" + (TransactionServices.GetTransactions().Count + 1).ToString(),
                    SenderAccount = acc.AccountNumber,
                    SenderIFSC = acc.IFSC,
                    RecepientAccount = "Withdraw",
                    RecepientIFSC = "Withdraw",
                    Amount = amount

                };
                TransactionServices.AddTransaction(t);
                return t;
            }
            catch (BalanceException) 
            {
                Console.WriteLine("Insufficient Balance");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
        }
    }
}
