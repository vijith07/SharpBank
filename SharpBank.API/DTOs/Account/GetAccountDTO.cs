using SharpBank.Models.Enums;

namespace SharpBank.API.DTOs.Account
{
    public class GetAccountDTO
    {

        public Guid Id { get; set; }
        public Guid BankId { get; set; }
        public string Name { get; set; }

        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public AccountType Type { get; set; }

    }
}
