using Microsoft.EntityFrameworkCore;
using SharpBank.Data;
using SharpBank.Models;
using SharpBank.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext appDbContext;

        public TransactionService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Transaction CreateTransaction(Transaction transaction)
        {
            appDbContext.Transactions.Add(transaction);
            appDbContext.SaveChanges();
            return appDbContext.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
        }

        public Transaction DeleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions(Guid bankId,Guid accountId) 
        {
            return appDbContext.Transactions.Include(t=>t.SourceAccount).Include(t=>t.DestinationAccount).Where(t => ((t.SourceAccount.BankId == bankId) && (t.SourceAccountId == accountId)) || ((t.DestinationAccount.BankId == bankId) && (t.DestinationAccountId == accountId))).ToList();
        }

       

        public Transaction GetTransaction(Guid Id)
        {
            return appDbContext.Transactions.Include(t => t.SourceAccount).Include(t => t.DestinationAccount).SingleOrDefault(t=>t.Id==Id);
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
