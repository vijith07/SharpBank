using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Account
    {
        [Key]
        public string AccountNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string IFSC { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public Account()
        {

        }
    }
}
