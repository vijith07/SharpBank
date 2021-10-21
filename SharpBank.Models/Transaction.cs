using SharpBank.Models.Enums;
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
        public string Id { get; set; }
        public string SourceBankId { get; set; }
        public string DestinationBankId { get; set; }
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionCharges { get; set; }
        public decimal  NetAmount { get; set; }
        public DateTime On { get; set; }
        public Enums.Type Type { get; set; }
        public Mode Mode { get; set; }
        public override string ToString()
        {
            string res = $"  {Id.ToString("D10")}  | {SourceBankId.ToString("D10")}  |   {SourceAccountId.ToString("D10")}   |   {DestinationBankId.ToString("D10")}  |  {DestinationAccountId.ToString("D10")} | {Amount.ToString("C3")} | {On}";
            return res;
        }
    }
}