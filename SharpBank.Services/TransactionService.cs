using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Enums;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
    public class TransactionService
    {
        private readonly AccountService accountService;
        private readonly BankService bankService;
        public TransactionService(AccountService accountService, BankService bankService)
        {
            this.accountService = accountService;
            this.bankService = bankService;
        }
        public string GenerateId(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId)
        {
            Account sourceAccount = accountService.GetAccount(sourceBankId, sourceAccountId);
            Account destinationAccount = accountService.GetAccount(destinationBankId, destinationAccountId);

            string Id;
            do
            {
                string timestamp = DateTime.UtcNow.ToString("yyMMddhmsf",
                                        System.Globalization.CultureInfo.InvariantCulture);
                Id ="TXN" + timestamp;
            }

            while ((sourceAccount.Transactions.SingleOrDefault(t => t.Id == Id) != null) ||
                    (destinationAccount.Transactions.SingleOrDefault(t => t.Id == Id) != null));
            return Id;
        }
        public string AddTransaction(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId,decimal totalAmount ,decimal amount, Mode mode, TransactionType type)
        {
            
            decimal charges = totalAmount-amount;

            if (sourceAccountId != "CASH")
            {
                accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - totalAmount);
            } 
            accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + amount);
            
            Transaction transaction = new Transaction
            {
                Id = GenerateId(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId),
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                SourceBankId = sourceBankId,
                DestinationBankId = destinationBankId,
                Mode=mode,
                Amount = totalAmount,
                Type=TransactionType.Debit,
                TransactionCharges=charges,
                NetAmount=amount,
                On = DateTime.Now
            };

            accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
            Transaction t2 = new Transaction {
                Id=transaction.Id,
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                SourceBankId = sourceBankId,
                DestinationBankId = destinationBankId,
                Mode = mode,
                Amount = totalAmount,
                Type = TransactionType.Credit,
                TransactionCharges = charges,
                NetAmount = amount,
                On = DateTime.Now
            };

            accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(t2);

            return transaction.Id;
        }

        public Transaction GetTransaction(string bankId, string accountId, string TransactionId)
        {
            Account account = accountService.GetAccount(bankId, accountId);
            var transaction = account.Transactions.SingleOrDefault(t => t.Id == TransactionId);
            return transaction;
        }


        public decimal ConvertToINR(string bankId,decimal amount,string code)
        {
            Currency curr = bankService.GetAcceptedCurrencies(bankId).SingleOrDefault(c => c.Code == code);
            if (curr == null)
            {
                throw new InvalidCurrencyException();
            }
            return amount * curr.ExchangeRate;
        }
        public decimal GetTransactionRate(string sourceBankId,string destinationBankId,Mode mode)
        {
            if (sourceBankId == destinationBankId)
            {
                if (mode == Mode.IMPS) {
                    return bankService.GetBank(sourceBankId).IMPSToSame; 
                }
                else if (mode == Mode.RTGS)
                {
                    return bankService.GetBank(sourceBankId).RTGSToSame;
                }
                return 0m;
            }
            else
            {
                if (mode == Mode.IMPS)
                {
                    return bankService.GetBank(sourceBankId).IMPSToOther;
                }
                else if (mode == Mode.RTGS)
                {
                    return bankService.GetBank(sourceBankId).RTGSToOther;
                }
                return 0m;
            }
        }
    }
}