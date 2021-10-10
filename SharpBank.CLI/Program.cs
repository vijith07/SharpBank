using System;
using System.Collections.Generic;
using SharpBank.Services;
using SharpBank.Models;
using SharpBank.CLI.Controllers;

namespace SharpBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {

            

            bool isRunning = true;
            int currentMenu = 0;
             Account acc=null;
            string userIFSC = "";
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
                            acc= AccountsController.CreateAccount(userIFSC);
                            Console.WriteLine("Your account number is " + acc.AccountNumber + "  and bank IFSC " + acc.IFSC + " Dont forget it .");
                            break;
                        case LoginOptions.Login:
                            string userAccountNumber = Inputs.GetAccountNumber();
                            string userPassword = Inputs.GetPassword();
                            acc = AccountsController.GetAccount(userIFSC, userAccountNumber);
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
                            TransactionServices.Deposit(acc ,amount);
                            break;
                        case UserOptions.Withdraw:
                            amount = Inputs.GetAmount();
                            TransactionServices.Withdraw(acc , amount);
                            break;
                        case UserOptions.Transfer:
                            List<string> recp = Inputs.GetRecipient();
                            amount = Inputs.GetAmount();
                            Account recipAcc = AccountsController.GetAccount(recp[0], recp[1]);
                            TransactionServices.Transfer(acc,  recipAcc,amount);
                            break;
                        case UserOptions.ShowBalance:
                            {
                                Console.WriteLine("Your Balance is: " + AccountsController.GetBalance(acc));
                                break;
                            }
                        case UserOptions.TransactionHistory:
                            List<Transaction> hist = AccountServices.GetTransactionHistory(acc);
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
