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
            catch (IFSCException e)
            {

                Console.WriteLine("Bank already exists.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return id;


        }
        public Bank GetBank(string bankId)
        {

            try
            {
                Bank b = bankService.GetBank(bankId);
                if (b == null)
                {
                    throw new IFSCException();
                }
                return b;
            }
            catch (IFSCException e)
            {

                Console.WriteLine("Bank does not exist.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
    }
}