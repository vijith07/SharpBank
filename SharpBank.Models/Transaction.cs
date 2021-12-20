using SharpBank.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Transaction
    {
        [Key]

        public Guid Id { get; set; }
    
        public Guid SourceAccountId { get; set; }
      
        public Guid DestinationAccountId { get; set; }
        public Account SourceAccount { get; set; }
        public Account DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionCharges { get; set; }
        public decimal  NetAmount { get; set; }
        public DateTime On { get; set; }
        public TransactionType Type { get; set; }
        public Mode Mode { get; set; }

        
    }
}