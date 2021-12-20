using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;

namespace SharpBank.Services.Interfaces
{
    public  interface ITransactionService
    {
        public Transaction CreateTransaction(Transaction transaction);
        public Transaction UpdateTransaction(Transaction transaction);
        public Transaction DeleteTransaction(Transaction transaction);
        public Transaction GetTransaction(Guid Id);
        public IEnumerable<Transaction> GetAllTransactions(Guid bankId,Guid accountId);
        
      
    }
}
