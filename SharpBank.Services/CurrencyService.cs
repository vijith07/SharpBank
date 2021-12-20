using SharpBank.Data;
using SharpBank.Models;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly AppDbContext appDbContext;

        public CurrencyService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Currency GetCurrencyFromCode(string code)
        {
            return appDbContext.Currencies.FirstOrDefault(c => c.Code == code);
        }
    }
}
