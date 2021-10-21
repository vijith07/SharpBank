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
                if (amount < 0||amount<accountService.GetBalance(bankId,accountId))
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(bankId, accountId, "0000000000", "0000000000", amount,Models.Enums.Mode.Other);
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
        public string Deposit(string bankId, string accountId, decimal amount)
        {

            string id = "";
            try
            {
                if (amount < 0)
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction( "0000000000", "0000000000", bankId, accountId, amount, Models.Enums.Mode.Other);
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
                if (amount < 0 || amount < accountService.GetBalance(sourceBankId, sourceAccountId))
                {
                    throw new BalanceException();
                }
                Mode mode = inputs.GetTransactionMode();
                id = transactionService.AddTransaction(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId, amount,mode);
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
    }
}