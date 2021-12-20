using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services.Interfaces
{
    public interface ICurrencyService
    {
        public Currency GetCurrencyFromCode(string code);

    }
}
