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
        [Key]
        public string TransactionID { get; set; }
        [Required]
        public string RecepientIFSC { get; set; }
        [Required]
        public string SenderIFSC { get; set; }
        [Required]
        public string RecepientAccount { get; set; }
        [Required]
        public string SenderAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }


        public Transaction()
        {
           
        }
    }
}
