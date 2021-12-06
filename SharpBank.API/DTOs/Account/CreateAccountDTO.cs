using SharpBank.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SharpBank.API.DTOs.Account
{
    public class CreateAccountDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage = "Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }

    }
}
