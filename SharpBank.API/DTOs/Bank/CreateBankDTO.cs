using SharpBank.Models;
using System.ComponentModel.DataAnnotations;

namespace SharpBank.API.DTOs.Bank
{
    public class CreateBankDTO
    {
        [Required]
        public string Name { get; set; }
        
        public decimal RTGSToSame { get; set; }
        public decimal RTGSToOther { get; set; }
        public decimal IMPSToSame { get; set; }
        public decimal IMPSToOther { get; set; }
        public string CreatedBy { get; set; }
    }
}
