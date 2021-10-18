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
    class TransactionsController
    {
        private readonly TransactionService transactionService;
        private readonly AccountServices accountService;

        public TransactionsController(TransactionService transactionService, AccountServices accountService)
        {
            this.transactionService = transactionService;
            
            this.accountService = accountService;
        }
    public long Withdraw(long bankId, long accountId, decimal amount)
        {
            
            long id = 0;
            try
            {
                if (amount < 0||amount<accountService.GetBalance(bankId,accountId))
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(bankId, accountId, 0, 0, amount);
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
        public long Deposit(long bankId, long accountId, decimal amount)
        {

            long id = 0;
            try
            {
                if (amount < 0)
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(0, 0, bankId, accountId, amount);
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
        public long Transfer(long sourceBankId, long sourceAccountId, long destinationBankId, long destinationAccountId, decimal amount)
        {
            long id = 0;
            try
            {
                if (amount < 0 || amount < accountService.GetBalance(sourceBankId, sourceAccountId))
                {
                    throw new BalanceException();
                }
                id = transactionService.AddTransaction(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId, amount);
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