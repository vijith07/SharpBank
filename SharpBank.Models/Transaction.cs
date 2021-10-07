using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string RecepientBank { get; set; }
        public string SenderBank { get; set; }
        public string RecepientAccount { get; set; }
        public string SenderAccount { get; set; }
        public decimal Amount { get; set; }
         
        
        public Transaction(string transactionID, string recepientBank, string senderBank, string recepientAccount, string senderAccount, decimal amount)
        {
            TransactionID = transactionID;
            RecepientBank = recepientBank;
            SenderBank = senderBank;
            RecepientAccount = recepientAccount;
            SenderAccount = senderAccount;
            Amount = amount;
        }
    }
}
