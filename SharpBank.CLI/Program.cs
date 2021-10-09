using System;
using System.Collections.Generic;
using SharpBank.Services;
using SharpBank.Models;
namespace SharpBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
           
            BankManagerServices.AddBank("Axis Bank");
            BankManagerServices.AddBank("ICICI Bank");
            BankManagerServices.AddBank("HDFC Bank");
            BankManagerServices.AddBank("Canara Bank");

            bool isRunning = true;
            int currentMenu = 0;
            string userIFSC = "";
            string userAccountNumber = "";
            string userPassword = "";
            while (isRunning) { 
                if (currentMenu == 0) {
                    Menu.BankMenu();
                    int bnk = Inputs.GetSelection();
                    userIFSC = bnk.ToString();
                    currentMenu++;
                }
                if (currentMenu == 1) {
                    Menu.LoginMenu();
                    LoginOptions option = (LoginOptions)Enum.Parse(typeof(LoginOptions), Console.ReadLine());
                    switch(option)
                    {
                        case LoginOptions.Create:
                            Console.WriteLine("Enter Your Name");
                            string userName = Console.ReadLine();

                            Console.WriteLine("Enter Your Possword");
                            userPassword = Console.ReadLine();

                            userAccountNumber = BankServices.AddAccount(userIFSC, userName, userPassword);
                            Console.WriteLine("Your account number is " + userAccountNumber + "  and bank IFSC " + userIFSC + " Dont forget it bsdk");
                            break;
                        case LoginOptions.Login:
                            userAccountNumber = Inputs.GetAccountNumber();
                            userPassword = Inputs.GetPassword();
                            currentMenu++;
                            break;
                        case LoginOptions.Back:
                            currentMenu--;
                            break;
                        case LoginOptions.Exit:
                            isRunning = false;
                            break;
                    }
                }
                if (currentMenu == 2) {
                    Menu.UserMenu();
                    UserOptions option = (UserOptions)Enum.Parse(typeof(UserOptions), Console.ReadLine());
                    decimal amount = 0m;
                    switch (option)
                    {
                        case UserOptions.Deposit:
                            amount = Inputs.GetAmount();
                            TransactionServices.Deposit(userIFSC, userAccountNumber, amount);
                            break;
                        case UserOptions.Withdraw:
                            amount = Inputs.GetAmount();
                            TransactionServices.Withdraw(userIFSC, userAccountNumber, amount);
                            break;
                        case UserOptions.Transfer:
                            List<string> recp = Inputs.GetRecipient();
                            amount = Inputs.GetAmount();
                            TransactionServices.Transfer(userIFSC, userAccountNumber, recp[0], recp[1], amount);
                            break;
                        case UserOptions.ShowBalance:
                            {
                                Console.WriteLine("Your Balance is: " + AccountServices.GetBalance(userIFSC, userAccountNumber));
                                break;
                            }
                        case UserOptions.TransactionHistory:
                            List<Transaction> hist = AccountServices.GetTransactionHistory(userIFSC, userAccountNumber);
                            foreach (Transaction t in hist)
                            {
                                Console.WriteLine(t.ToString());
                            }
                            break;
                        case UserOptions.Exit:
                            currentMenu = 0;
                            break;
                        default:
                            Console.WriteLine("Invalid ma");
                            break;

                    }

                }
       
            }

        }
        public enum LoginOptions
        {
            Create=1,
            Login,
            Back,
            Exit
        }
        public enum UserOptions
        {
            Deposit=1,
            Withdraw,
            Transfer,
            ShowBalance,
            TransactionHistory,
            Exit

        }
    }
}
