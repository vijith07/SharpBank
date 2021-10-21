using SharpBank.Models;
using SharpBank.Services;
using SharpBank.Models.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models.Enums;

namespace SharpBank.CLI.Controllers
{
    class AccountsController
    {
        private readonly AccountService accountService;
        private readonly Inputs inputs;

        public AccountsController(AccountService accountService, Inputs inputs)
        {
            this.accountService = accountService;
            this.inputs = inputs;
        }
        public string CreateAccount(string bankId)
        {
            string id = "";
            try
            {
                string name = inputs.GetName();
                string password = inputs.GetPassword();
                Gender gender = inputs.GetGender();
                AccountType type = inputs.GetAccountType();
                id = accountService.AddAccount(name, bankId, gender,type);
            }
            catch (AccountIdException e)
            {

                Console.WriteLine("Account Number already exists.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return id;
        }
        public Account GetAccount(string bankId, string accountId)
        {

            try
            {
                Account acc = accountService.GetAccount(bankId, accountId);
                return acc;
            }
            catch (AccountIdException e)
            {

                Console.WriteLine("Account  doesnot  exist.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
        public decimal GetBalance(string bankId, string accountId)
        {
            try
            {
                Account acc = accountService.GetAccount(bankId, accountId);
                if (acc == null)
                {
                    throw new AccountIdException();
                }
                return acc.Balance;
            }
            catch (AccountIdException e)
            {

                Console.WriteLine("Account  doesnot  exist.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return -1m;
        }
        public List<Transaction> GetTransactionHistory(string bankId, string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                transactions = accountService.GetAccount(bankId, accountId).Transactions.ToList();
                return transactions;
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
    }
}