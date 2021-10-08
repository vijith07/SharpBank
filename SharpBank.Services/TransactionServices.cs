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
        private void validateTransaction(string senderIFSC, string sender, string receiverIFSC, string receiver, decimal amount)
        {
            try
            {
                if (BankManager.Banks[senderIFSC].getAccount(sender).Balance - amount < 0)
                {
                    throw new BalanceException();
                }
                BankManager.Banks[receiverIFSC].getAccount(receiver);
            }
            catch (ArgumentException e)
            {
                throw new AccountNumberException();
            }
            catch (KeyNotFoundException e) {
                throw new IFSCException();
            }
        
        }
        public void AddTransaction(string senderIFSC, string sender, string receiverIFSC, string receiver,decimal amount) 
        {
            validateTransaction(senderIFSC,sender,receiverIFSC,receiver,amount);
            string id = generateTransactionID();
            Transaction txn = new Transaction(id, senderIFSC, sender, receiverIFSC, receiver, amount);
            BankManager.Transactions.Add(id,txn);
        }
        public void Withdraw(string accountNumber, string ifsc, decimal amount)
        {
            try
            {
                Account acc = BankManager.Banks[ifsc].getAccount(accountNumber);

                if (acc.Balance - amount < 0)
                {
                    throw new BalanceException();
                }
                if (amount < 0)
                {
                    throw new FormatException();
                }

                acc.Balance -= amount;

                BankManager.Banks[ifsc].setAccount(accountNumber, acc);

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
        public void Deposit(string accountNumber, string ifsc, decimal amount)
        {
            try
            {
                Account acc = BankManager.Banks[ifsc].getAccount(accountNumber);

                if (amount < 0)
                {
                    throw new FormatException();
                }

                acc.Balance += amount;

                BankManager.Banks[ifsc].setAccount(accountNumber, acc);

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
