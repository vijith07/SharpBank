using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;

namespace SharpBank.Services.Interfaces
{
    public interface IBankService
    {
        public Bank CreateBank(Bank bank);
        public Bank UpdateBank(Bank bank); 
        public Bank DeleteBank(Guid Id);
        public IEnumerable<Bank> GetAllBanks();
        public Bank GetBank(Guid bankId);
    }
}
