using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Services;

namespace SharpBank.CLI
{
    class Menu
    {
        public void BankMenu(Datastore datastore,BankService bankService)
        {
            Console.WriteLine("BANK MENU:   ");
            Console.WriteLine("Choose and Enter tHe option below");
            Console.WriteLine("Choose Your Bank");
            Console.WriteLine("Bank Id    | Name");
            Console.WriteLine("------------------");
            int c = 1;
            List<string> banks = bankService.GetBanks();
            foreach (string bank in banks)
            {
                Console.WriteLine(bank);
            }
            Console.WriteLine("----------------------------------");
        }
        public void LoginMenu()
        {
            Console.WriteLine("LOGIN MENU:   ");
            Console.WriteLine("Choose and Enter tHe option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("-------------------------");
            Console.WriteLine("   1   | Create Account");
            Console.WriteLine("   2   | Login");
            Console.WriteLine("   3   | Back");
            Console.WriteLine("   4   | Exit");
            Console.WriteLine("-------------------------");
        }
        public void UserMenu()
        {
            Console.WriteLine("USER MENU:   ");
            Console.WriteLine("Choose and Enter tHe option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("   1   | Deposit");
            Console.WriteLine("   2   | Withdraw");
            Console.WriteLine("   3   | Transfer");
            Console.WriteLine("   4   | Show Balance");
            Console.WriteLine("   5   | Show Transaction History");
            Console.WriteLine("----------------------------------");
        }
    }
}