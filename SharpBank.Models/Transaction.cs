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
        public string RecepientIFSC { get; set; }
        public string SenderIFSC { get; set; }
        public string RecepientAccount { get; set; }
        public string SenderAccount { get; set; }
        public decimal Amount { get; set; }
         
        
        public Transaction(string transactionID, string senderIFSC, string senderAccount,  string recepientIFSC, string recepientAccount, decimal amount)
        {
            TransactionID = transactionID;
            RecepientIFSC = recepientIFSC;
            SenderIFSC = senderIFSC;
            RecepientAccount = recepientAccount;
            SenderAccount = senderAccount;
            Amount = amount;
        }
    }
}
