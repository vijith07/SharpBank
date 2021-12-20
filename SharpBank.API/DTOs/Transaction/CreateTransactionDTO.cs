using SharpBank.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SharpBank.API.DTOs.Transaction
{
    public class CreateTransactionDTO
    {
        [Required]
        public Guid DestinationAccountId { get; set; }
        [Required]

        public Guid DestinationBankId { get; set; }
        [Required]

        public decimal Amount { get; set; }
        [Required]

        public Mode Mode { get; set; }
    }
}
