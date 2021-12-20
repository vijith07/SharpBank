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
    public class Account
    {
        [Key]

        public Guid Id { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }

        public AccountType Type { get; set; }
        public ICollection<Transaction>? DebitTransactions { get; set; }
        public ICollection<Transaction>? CreditTransactions { get; set; }
    }
}
