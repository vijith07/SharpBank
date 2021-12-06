using SharpBank.Models;

namespace SharpBank.API.DTOs.Bank
{
    public class UpdateBankDTO
    {
        
        public string Name { get; set; }
        public ICollection<Currency> Currencies { get; set; }
        public decimal RTGSToSame { get; set; }
        public decimal RTGSToOther { get; set; }
        public decimal IMPSToSame { get; set; }
        public decimal IMPSToOther { get; set; }

    }
}
