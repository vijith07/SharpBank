using SharpBank.Models;

namespace SharpBank.API.DTOs.Bank
{
    public class GetBankDTO
    {


        public Guid Id { get; set; }
        public string Name { get; set; }


        public Currency DefaultCurrency { get; set; }

        public decimal RTGSToSame { get; set; }
        public decimal RTGSToOther { get; set; }
        public decimal IMPSToSame { get; set; }
        public decimal IMPSToOther { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
