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

        //public override string ToString() {
        //    string res = "";
        //    res+="Transaction ID : " + this.TransactionID + Environment.NewLine;
        //    res+="-------------------"+Environment.NewLine;
        //    res+="Recepient IFSC : " + this.RecepientAccount+ Environment.NewLine + "Recepient Acc Number : " + this.RecepientAccount +Environment.NewLine;
        //    res += "Sender IFSC : " + this.SenderAccount + Environment.NewLine+"Sender Acc Number : " + this.SenderAccount + Environment.NewLine;
        //    res += "Transacted Amount : " + this.Amount +"bucks"+ Environment.NewLine;
        //    return res;
        //}

        public Transaction()
        {
           
        }
    }
}
