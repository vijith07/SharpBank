using SharpBank.Models;
using SharpBank.Models.Exceptions;
using SharpBank.Models.Enums;
using SharpBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.CLI.Controllers
{
    class TransactionsController
    {
        private readonly TransactionService transactionService;
        private readonly AccountService accountService;
        private readonly Inputs inputs;


        public TransactionsController(TransactionService transactionService, AccountService accountService, Inputs inputs)
        {
            this.transactionService = transactionService;
            this.inputs = inputs;
            this.accountService = accountService;
        }
    public string Withdraw(string bankId, string accountId, decimal amount)
        {
            
            string id = "";
            try
            {
                if (amount < 0 || amount > accountService.GetBalance(bankId,accountId))
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(bankId, accountId, bankId, "CASH", amount,amount,Models.Enums.Mode.Other, TransactionType.Debit);
                Console.WriteLine("Transaction Successful");
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance or Inavlid Amount entered");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public string Deposit(string bankId, string accountId, decimal amount,string code)
        {
            
            string id = "";
            try
            {
                amount = transactionService.ConvertToINR(bankId, amount, code);
                if (amount < 0)
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(bankId, "CASH", bankId, accountId, amount,amount, Models.Enums.Mode.Other, TransactionType.Credit);
                Console.WriteLine("Transaction Successful");
            }
            catch (InvalidCurrencyException)
            {
                Console.WriteLine(code+" Currency is not accpted here");
            }
        
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance or Inavlid Amount entered");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public string Transfer(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, decimal amount)
        {
            string id = "";
            try
            {
                Mode mode = inputs.GetTransactionMode();
                decimal rate = transactionService.GetTransactionRate(sourceBankId, destinationBankId, mode);
                decimal totalAmount = amount / (1m - rate);
                
                if (totalAmount < 0 || totalAmount > accountService.GetBalance(sourceBankId, sourceAccountId))
                {
                    throw new BalanceException();
                }
                if(GenerateBill(amount, totalAmount,destinationBankId,destinationAccountId,mode))
                {
                    id = transactionService.AddTransaction(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId, totalAmount, amount, mode, TransactionType.Debit);
                    Console.WriteLine("Transaction Successful : " + id);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance or Inavlid Amount entered");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");
            }
            return id;
        }
        public bool GenerateBill(decimal amount ,decimal totalAmount,string bankId,string accountId,Mode mode)
        {
            Console.WriteLine("Transaction Summary");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("TO: Bank ID: "+bankId+" Account ID: "+accountId+" Mode: "+mode.ToString());
            Console.WriteLine("Amount             :  " + amount.ToString() + "/-");
            Console.WriteLine("Transaction Charges: +" + (totalAmount-amount).ToString() + "/-");
            Console.WriteLine("Total Amount       : =" + (totalAmount).ToString() + "/-");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            Console.WriteLine("Would you like to proceed?((Yes/1/y)/No)");
            string s = Console.ReadLine();
            if (s == "1" || s == "Yes" || s == "y" || s == "Y"|| s == "yes")
            {
                return true;
            }
            return false;
        }
    }
}