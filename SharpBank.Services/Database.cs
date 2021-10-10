using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    static class  Database
    {
        public static List<Bank> Banks { get; set; }
        public static List<Account> Accounts { get; set; }
        public static List<Transaction> Transactions { get; set; }
        static Database()
        {
            Banks = new List<Bank> {
               new Bank{
               BankName = "Yaxis",
               IFSC = "001"
               },
               new Bank{
                    BankName = "YESBI",
                    IFSC = "002"
               },
                new Bank{
               BankName = "FDHC",
               IFSC = "003"
               },
               new Bank{
                    BankName = "YCYCY",
                    IFSC = "004"
               }
           };

            Accounts = new List<Account>
            {
                new Account
                {
                    UserName="Shriram",
                    AccountNumber="006",
                    Password="006",
                    IFSC="001",
                    Balance=0m
                },
                new Account
                {
                    UserName="Vijith",
                    AccountNumber="007",
                    Password="007",
                    IFSC="003",
                    Balance=0m
                },
                new Account
                {
                    UserName="Mohith Abhiram",
                    AccountNumber="008",
                    Password="008",
                    IFSC="001",
                    Balance=0m
                }
            };
            Transactions = new List<Transaction>();
        }
    }
}
