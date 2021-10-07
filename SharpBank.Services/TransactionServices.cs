using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    class TransactionServices
    {
        private string generateTransactionID() 
        {
            int serialNumber = BankManager.Transactions.Count + 1;
            return serialNumber.ToString();
        }
        public void AddTransaction(string senderIFSC, string sender, string receiverIFSC, string receiver,decimal amount) 
        {
            string id = generateTransactionID();
            Transaction txn = new Transaction(id, senderIFSC, sender, receiverIFSC, receiver, amount);
            BankManager.Transactions.Add(id,txn);
        }
    }
}
