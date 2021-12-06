using SharpBank.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SharpBank.API.DTOs.Account
{
    public class UpdateAccountDTO
    {

        public string Name { get; set; }

        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public Gender Gender { get; set; }
        public Status Status { get; set; }

    }
}
