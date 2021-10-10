using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Bank
    {
        [Required]
        public string BankName { get; set; }
        [Key]
        public string IFSC { get; set; }
        public string ImagePath { get; set; }

        public Bank()
        {

        }

    }
}
