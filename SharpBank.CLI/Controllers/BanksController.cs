using SharpBank.Models;
using SharpBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models.Exceptions;



namespace SharpBank.CLI.Controllers
{
    class BanksController
    {
        private readonly BankService bankService;
        private readonly Inputs inputs;

        public BanksController(BankService bankService, Inputs inputs)
        {
            this.bankService = bankService;
            this.inputs = inputs;
        }

        public string CreateBank(string v)
        {
            string id = "";
            try
            {
                string name = v;
                id = bankService.AddBank(name);

            }
            catch (BankIdException e)
            {

                Console.WriteLine("Bank already exists.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return id;


        }
        public void AddNewCurrency(string bankId,string code)
        {
            bankService.AddCurrency(bankId,code);

        }
        public List<string> GetCurrencies(List<Currency> currencies)
        {
            string s = "";
            List<string> CurrencyList = new List<string>();
            foreach (Currency curr in currencies)
            {
                s=curr.Code + " " + curr.Name + " " + curr.ExchangeRate.ToString();
                CurrencyList.Add(s);
            }
            return CurrencyList;
        }
    }
}