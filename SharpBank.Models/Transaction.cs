using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SharpBank.Models
{
    public class Transaction
    {
        public long ID { get; set; }
      
        public long SourceAccountID { get; set; }
        public long SourceBankId { get; set; }
        public long DestinationAccountID { get; set; }
        public long DestinationBankId { get; set; }
        public decimal Amount { get; set; }
        public Enums.Type Type { get; set; }
        public string Description { get; set; }
        public DateTime On { get; set; }

    }
}
