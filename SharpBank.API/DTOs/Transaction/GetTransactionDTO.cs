using SharpBank.Models.Enums;

namespace SharpBank.API.DTOs.Transaction
{
    public class GetTransactionDTO
    {

        public Guid Id { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid SourceBankId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public Guid DestinationBankId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionCharges { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime On { get; set; }
        public Mode Mode { get; set; }
    }
}
