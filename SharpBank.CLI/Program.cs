using System;
using SharpBank.Services;
namespace SharpBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string ifs = BankManagerServices.AddBank("Yaxis Byank");
            string num = BankServices.AddAccount(ifs,"Raju","Chutki");
            TransactionServices.Deposit(ifs, num, 100.0m);
            Console.WriteLine(AccountServices.GetBalance(ifs,num));
        }
    }
}
