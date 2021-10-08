using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public static class TransactionServices
    {
        private static string generateTransactionID() 
        {
            int serialNumber = BankManager.Transactions.Count + 1;
            return serialNumber.ToString();
        }
        private static void validateTransaction(string senderIFSC, string sender, string receiverIFSC, string receiver, decimal amount)
        {
            try
            {
                if (amount < 0)
                {
                    throw new FormatException();
                }
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
        private static void validateTransaction(string ifsc, string accountNumber, decimal amount) {
            try
            {
                if ((BankManager.Banks[ifsc].getAccount(accountNumber).Balance + amount) < 0)
                {
                    throw new BalanceException();
                }
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
        private static void AddTransaction(string senderIFSC, string sender, string receiverIFSC, string receiver,decimal amount) 
        {
            validateTransaction(senderIFSC,sender,receiverIFSC,receiver,amount);
            string id = generateTransactionID();
            Transaction txn = new Transaction(id, senderIFSC, sender, receiverIFSC, receiver, amount);
            BankManager.Transactions.Add(txn);
        }
        private static void AddTransaction(string ifsc, string accountNumber, decimal amount)
        {
            if (amount > 0)
            {
                
                string id = generateTransactionID();
                Transaction txn = new Transaction(id, "Deposit", "Deposit", ifsc, accountNumber, amount);
                BankManager.Transactions.Add(txn);
                
            }
            else
            {
                string id = generateTransactionID();
                Transaction txn = new Transaction(id, ifsc, accountNumber, "Withdraw", "Withdraw", amount);
                BankManager.Transactions.Add(txn);
            }
        }

        public static void Transfer(string senderIFSC, string sender, string receiverIFSC, string receiver, decimal amount) 
        {
            validateTransaction(senderIFSC, sender, receiverIFSC, receiver, amount);
            Account senderAcc = BankManager.Banks[senderIFSC].getAccount(sender);
            Account receiverAcc = BankManager.Banks[receiverIFSC].getAccount(receiver);
            senderAcc.Balance -= amount;
            receiverAcc.Balance += amount;
            BankManager.Banks[senderIFSC].setAccount(sender,senderAcc);
            BankManager.Banks[receiverIFSC].setAccount(receiver,receiverAcc);
            AddTransaction(senderIFSC, sender, receiverIFSC, receiver, amount);

        }

        public static void Withdraw(string ifsc,string accountNumber, decimal amount)
        {
            try
            {
                validateTransaction(ifsc, accountNumber, -amount);
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
                AddTransaction(ifsc, accountNumber, -amount);

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
        public static void Deposit(string ifsc,string accountNumber, decimal amount)
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
                AddTransaction(ifsc, accountNumber, amount);

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
