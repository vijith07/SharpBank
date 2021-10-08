using System;
using SharpBank.Services;
namespace SharpBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string ifs = BankManagerServices.AddBank("Yaxis Byank");
            string num1 = BankServices.AddAccount(ifs,"Raju","Chutki");
            TransactionServices.Deposit(ifs, num1, 100.0m);
            string num2 = BankServices.AddAccount(ifs, "Raju", "Chutki");
            TransactionServices.Deposit(ifs, num2, 10.0m);
            TransactionServices.Transfer(ifs, num1, ifs, num2, 50m);
            Console.WriteLine(AccountServices.GetBalance(ifs,num1));
        }
    }
}
