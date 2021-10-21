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
        public string AddTransaction(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId,decimal amount,Mode mode)
        {
            decimal rate=0;
            if(mode == Mode.Other)
            {
                rate = 0;
            }
            else if(sourceBankId== destinationBankId)
            {
                if (mode == Mode.IMPS)
                {
                    rate = bankService.GetBank(sourceBankId).IMPSToSame;
                }
                else if(mode == Mode.RTGS)
                {
                    rate = bankService.GetBank(sourceBankId).RTGSToSame;
                }
            }
            else if(sourceBankId != destinationBankId)
            {
                if (mode == Mode.IMPS)
                {
                    rate = bankService.GetBank(sourceBankId).IMPSToOther;
                }
                else if (mode == Mode.RTGS)
                {
                    rate = bankService.GetBank(sourceBankId).RTGSToOther;
                }
            }
            decimal charges= amount * rate;
            accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - (amount-charges));
            accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + (amount-charges));

            Transaction transaction = new Transaction
            {
                Id = GenerateId(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId),
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                SourceBankId = sourceBankId,
                DestinationBankId = destinationBankId,
                Mode=mode,
                Amount = amount,
                TransactionCharges=charges,
                NetAmount=amount-charges,
                On = DateTime.Now
            };
            accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
            accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(transaction);

            return transaction.Id;
        }

        public Transaction GetTransaction(string bankId, string accountId, string TransactionId)
        {
            Account account = accountService.GetAccount(bankId, accountId);
            var transaction = account.Transactions.SingleOrDefault(t => t.Id == TransactionId);
            return transaction;
        }
    }
}