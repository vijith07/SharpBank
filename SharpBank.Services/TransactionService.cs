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
        public Transaction CreateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction DeleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransaction(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
