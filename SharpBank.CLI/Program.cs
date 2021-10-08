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
           
            BankManagerServices.AddBank("Acksis Bank");
            BankManagerServices.AddBank("Yaxis Bank");
            BankManagerServices.AddBank("Puxis Bank");
            BankManagerServices.AddBank("Yesu Bank");

            bool isRunning = true;
            int currentMenu = 0;
            string userIFSC = "";
            string userAccountNumber = "";
            string userPassword = "";
            while (isRunning) { 
                if (currentMenu == 0) {
                    ShowMenu(currentMenu);
                    int bnk = Inputs.GetSelection();
                    userIFSC = bnk.ToString();
                    currentMenu++;
                    
                }
                if (currentMenu == 1) {
                    ShowMenu(currentMenu);
                    int sel = Inputs.GetSelection();
                    if (sel == 1) {
                        Console.WriteLine("Enter Your Name");
                        string userName = Console.ReadLine();

                        Console.WriteLine("Enter Your Possword");
                        userPassword = Console.ReadLine();

                        userAccountNumber = BankServices.AddAccount(userIFSC, userName, userPassword);
                        Console.WriteLine("Your account number is " + userAccountNumber + "  and bank IFSC " + userIFSC + " Dont forget it bsdk");
                    }
                    if (sel == 2) {
                        userAccountNumber = Inputs.GetAccountNumber();
                        userPassword = Inputs.GetPassword();
                        currentMenu++;
                    }
                    if (sel == 3) currentMenu--;
                    if (sel == 4) isRunning = false;
                }
                if (currentMenu == 2) {
                    ShowMenu(currentMenu);
                    int sel = Inputs.GetSelection();
                    decimal amount = 0m;
                    switch (sel)
                    {
                        case 1:
                            amount = Inputs.GetAmount();
                            TransactionServices.Deposit(userIFSC, userAccountNumber, amount);
                            break;
                        case 2:
                            amount = Inputs.GetAmount();
                            TransactionServices.Withdraw(userIFSC, userAccountNumber, amount);
                            break;
                        case 3:
                            List<string> recp = Inputs.GetRecipient();
                            amount = Inputs.GetAmount();
                            TransactionServices.Transfer(userIFSC, userAccountNumber, recp[0], recp[1], amount);
                            break;
                        case 4:
                            {
                                Console.WriteLine("Your Balance is: " + AccountServices.GetBalance(userIFSC, userAccountNumber));
                                break;
                            }
                        case 5:
                            List<Transaction> hist = AccountServices.GetTransactionHistory(userIFSC, userAccountNumber);
                            foreach (Transaction t in hist) {
                                Console.WriteLine(t.ToString());
                            }
                            break;
                        case 6:
                            currentMenu = 0;
                            break;
                        default:
                            Console.WriteLine("Invalid ma");

                            break;
                    }

                }
            
            
            
            }

        }
        public static void ShowMenu(int choice) {

            switch (choice) {

                case 0:Menu.BankMenu();break;
                case 1:Menu.LoginMenu();break;
                case 2:Menu.UserMenu();break;
            }
        }

    }
}
