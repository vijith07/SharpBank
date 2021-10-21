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
            Console.WriteLine("Choose and Enter The option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("-------------------------");
            Console.WriteLine("   1   | StaffLogin");
            Console.WriteLine("   2   | UserLogin");
            Console.WriteLine("   3   | Back");
            Console.WriteLine("   4   | Exit");
            Console.WriteLine("-------------------------");
        }
        public void StaffMenu()
        {
            Console.WriteLine("STAFF MENU:   ");
            Console.WriteLine("Choose and Enter tHe option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("   1   | CreateAccount");
            Console.WriteLine("   2   | Get Account Details");
            Console.WriteLine("   3   | UpdateAcount");
            Console.WriteLine("   4   | DeleteAccount");
            Console.WriteLine("   5   | AddIMPSSame");
            Console.WriteLine("   6   | AddIMPSOther");
            Console.WriteLine("   7   | AddRTGSSame");
            Console.WriteLine("   8   | AddRTGSOther");
            Console.WriteLine("   9   | ShowAccountTransactionHistory");
            Console.WriteLine("  10   | ShowBankTransactionHistory");
            Console.WriteLine("  11   | RevertTransaction");
            Console.WriteLine("  12   | AddNewCurrency");
            Console.WriteLine("  13   | Back");
            Console.WriteLine("  14   | Exit");
            Console.WriteLine("----------------------------------");
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
            Console.WriteLine("   6   | Back");
            Console.WriteLine("   7   | Exit");
            Console.WriteLine("----------------------------------");
        }
    }
}