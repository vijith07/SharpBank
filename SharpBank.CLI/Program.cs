using System;
using System.Collections.Generic;
using SharpBank.Services;
using SharpBank.Models;
using SharpBank.Models.Exceptions;
using SharpBank.CLI.Controllers;
using SharpBank.CLI.Enums;

namespace SharpBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {


            Inputs inputs = new Inputs();
            Datastore datastore = new Datastore();

            BankService bankService = new BankService(datastore);
            AccountService accountService = new AccountService(bankService);
            TransactionService transactionService = new TransactionService(accountService, bankService);

            BanksController banksController = new BanksController(bankService, inputs);
            AccountsController accountsController = new AccountsController(accountService, inputs);
            TransactionsController transactionsController = new TransactionsController(transactionService,accountService,inputs);
            //SEED

            banksController.CreateBank("AXIS");
            banksController.CreateBank("SBI");
            banksController.CreateBank("HDFC");
            banksController.CreateBank("ICICI");

            Menu menu = new Menu();

            int currentMenu = 0;
            string userBankId = "";
            string userAccountId = "";
            string userPassword = "";
            while (true)
            {
                if (currentMenu == 0)
                {
                    menu.BankMenu(datastore,bankService);
                    string bnk = inputs.GetSelection();
                    userBankId = bnk;
                    currentMenu++;
                }
                if (currentMenu == 1)
                {
                    menu.LoginMenu();
                    LoginOptions option = (LoginOptions)Enum.Parse(typeof(LoginOptions), Console.ReadLine());
                    switch (option)
                    {
                        case LoginOptions.StaffLogin:
                            userAccountId = inputs.GetAccountId();
                            userPassword = inputs.GetPassword();
                            try
                            {
                                accountService.ValidateStaff(userBankId, userAccountId, userPassword);
                                currentMenu++;
                                currentMenu++;
                            }
                            catch (Models.Exceptions.UnauthorizedAccessException)
                            {
                                Console.WriteLine("You are not Authorized to Access this Page");
                            }
                            catch (PasswordIncorrectException)
                            {
                                Console.WriteLine("Password Entered is Incorrect");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something is Wrong");
                            }
                            //userAccountId = accountsController.CreateAccount(userBankId);
                            //Console.WriteLine("Your account number is " + userAccountId.ToString("D10")+" and your BankID is " + userBankId.ToString("D10") + " Dont forget it .");
                            break;
                        case LoginOptions.CustomerLogin:
                            userAccountId = inputs.GetAccountId();
                            userPassword = inputs.GetPassword();
                            try
                            {
                                accountService.ValidateUser(userBankId, userAccountId, userPassword);
                                currentMenu++;
                            }
                            catch (PasswordIncorrectException)
                            {
                                Console.WriteLine("Password Entered is Incorrect");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something is Wrong");
                            }
                            
                            //userAccountId = accountsController.CreateAccount(userBankId);
                            //Console.WriteLine("Your account number is " + userAccountId.ToString("D10")+" and your BankID is " + userBankId.ToString("D10") + " Dont forget it .");
                            break;
                        case LoginOptions.Back:
                            currentMenu--;
                            break;
                        case LoginOptions.Exit:
                            Environment.Exit(0);
                            break;
                    }
                }
                if (currentMenu == 2)
                {
                    menu.UserMenu();
                    UserOptions option = (UserOptions)Enum.Parse(typeof(UserOptions), Console.ReadLine());
                    decimal amount = 0m;
                    switch (option)
                    {
                        case UserOptions.Deposit:
                            Console.WriteLine("NOTE: ALL THE DEPOSITS WILL BE CONVERTED TO INR");
                            string Code = inputs.GetCurrencyCode(banksController.GetCurrencies(bankService.GetAcceptedCurrencies(userBankId)));
                            amount = inputs.GetAmount();
                            transactionsController.Deposit(userBankId, userAccountId, amount,Code);
                            break;
                        case UserOptions.Withdraw:
                            amount = inputs.GetAmount();
                            transactionsController.Withdraw(userBankId, userAccountId, amount);
                            break;
                        case UserOptions.Transfer:
                            List<string> recp = inputs.GetRecipient();
                            amount = inputs.GetAmount();
                            try
                            {
                                transactionsController.Transfer(userBankId, userAccountId, recp[0], recp[1], amount);
                            }
                            catch (BankIdException)
                            {
                                Console.WriteLine("Enter Valid Bank Details");
                            }
                            catch (AccountIdException)
                            {
                                Console.WriteLine("Enter Valid Account Details");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something is Wrong");
                            }
                            break;
                        case UserOptions.ShowBalance:
                            {
                                Console.WriteLine("Your Balance is: " + accountsController.GetBalance(userBankId, userAccountId));
                                break;
                            }
                        case UserOptions.TransactionHistory:
                            List<Transaction> hist = accountsController.GetTransactionHistory(userBankId, userAccountId);

                            Console.WriteLine("  TransactionId    | Source Bank    | Source Account    | Dest. Bank    | Dest Account    |    Mode    |    Type    |    Amount    |    Charges    |    NetAmount  |    Timestamp   ");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            foreach (Transaction t in hist)
                            {
                                Console.WriteLine(t.ToString());
                            }
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            break;
                        case UserOptions.Logout:
                            currentMenu--;
                            break;
                        case UserOptions.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;

                    }

                }
                if (currentMenu == 3)
                {
                    menu.StaffMenu();
                    StaffOptions option = (StaffOptions)Enum.Parse(typeof(StaffOptions), Console.ReadLine());
                    decimal amount = 0m;
                    switch (option)
                    {
                        case StaffOptions.CreateAccount:
                            string newuserAccountId = accountsController.CreateAccount(userBankId);
                            Console.WriteLine("New Account is Created  account number is " + newuserAccountId +" and your BankID is " + userBankId + " and the password generated is '(first letter of the name in uppercase)@123'  Dont forget it .");
                            break;
                        case StaffOptions.UpdateAcount:
                            //string newuserAccountId=i
                            //transactionsController.Withdraw(userBankId, userAccountId, amount);
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.DeleteAccount:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.ShowAccountTransactionHistory:
                            userAccountId = inputs.GetAccountId();
                            List<Transaction> hist = accountsController.GetTransactionHistory(userBankId, userAccountId);

                            Console.WriteLine("TransactionId     |     Source Bank     |    Source Account    |    Dest. Bank    |    Dest Account    |    Mode    |    Amount    |    Charges    |   NetAmount   |   Timestamp ");
                            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            foreach (Transaction t in hist)
                            {
                                Console.WriteLine(t.ToString());
                            }
                            break;

                        case StaffOptions.ShowBankTransactionHistory:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.AddIMPSOther:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.AddIMPSSame:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.AddRTGSOther:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.AddRTGSSame:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.AddNewCurrency:
                            string Code = inputs.GetCurrencyCode(banksController.GetCurrencies(datastore.Currencies));
                            try
                            {
                                banksController.AddNewCurrency(userBankId, Code);
                            }
                            catch (InvalidCurrencyException)
                            {
                                Console.WriteLine("Currency does not exist ");
                            }
                            catch (CurrencyExistsException)
                            {
                                Console.WriteLine("Currency already exists ");
                            }
                            break;
                        case StaffOptions.RevertTransaction:
                            Console.WriteLine("Functionality Yet To be Implemented");
                            break;
                        case StaffOptions.Logout:
                            currentMenu--;
                            currentMenu--;
                            break;
                        case StaffOptions.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;

                    }

                }

            }

        }

    }
}