using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class Datastore
    {
        public List<Bank> Banks { get; set; }
        public List<Currency> Currencies { get; set; }
        public Datastore()
        {
            Banks = new List<Bank>();
            Currencies = new List<Currency>{new Currency
            {
                Name = "Indian Rupee",
                Code = "INR",
                ExchangeRate = 1m
            },
            new Currency{
                Name = "Pound Sterling",
                Code = "GBP",
                ExchangeRate = 100m
            },
            new Currency{
                Name = "Euro",
                Code = "EUR",
                ExchangeRate = 90m
            },
            new Currency{
                Name = "US Dollar",
                Code = "USD",
                ExchangeRate = 75m
            },
            new Currency{
                Name = "Bitcoin",
                Code = "BTC",
                ExchangeRate = 4800000m
            },
             new Currency{
                Name = "Shiba Inu",
                Code = "SHIB",
                ExchangeRate = 0.003m
            },
            };
        }

    }
}