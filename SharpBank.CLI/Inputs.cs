using SharpBank.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharpBank.CLI
{
    public class Inputs
    {
        public string GetAccountId()
        {
            Console.WriteLine("Please Enter Your ID :");
            return Console.ReadLine();
        }
        public string GetPassword()
        {
            Console.WriteLine("Please Enter Your Password :");
            return Console.ReadLine();
        }
        public string GetName()
        {
            Console.WriteLine("Please Enter The Name :");
            return Console.ReadLine();
        }
        public string GetCurrencyCode(List<string> Currencies)
        {
            Console.WriteLine("Choose the Currency You would Like to add");
            foreach(string s in Currencies)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Please Enter The Code Carefully :");

            return Console.ReadLine();
        }
        public Gender GetGender()
        {
            Console.WriteLine("Please Enter Your Gender (Male/Female/Other) :");
            try
            {
                Enum.TryParse(Console.ReadLine(), out Gender gender);
                return gender;

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
            }
            return (Gender)1;
        }
        public AccountType GetAccountType()
        {
            Console.WriteLine("Please Enter The Account Type (Staff/Customer) :");
            
            try
            {
                Enum.TryParse(Console.ReadLine(), out AccountType type);
                return type;

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
            }
            return (AccountType)2;
        }
        public Mode GetTransactionMode()
        {
            Console.WriteLine("Please Enter The Transaction Mode (IMPS/RTGS) :");

            try
            {
                 Enum.TryParse(Console.ReadLine(), out Mode mode);
                return mode;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
            }
            return (Mode)3;
        }
        public string GetSelection()
        {
            try
            {
                Console.WriteLine("Please Enter Your Selection :");

                return Console.ReadLine();
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid Selection");
            }
            //Goback
            return "";
        }
        public decimal GetAmount()
        {
            Console.WriteLine("Please Enter The Amount :");
            return Convert.ToDecimal(Console.ReadLine());
        }
        public string GetRecipientBankId()
        {
            Console.WriteLine("Please Enter Recipient BankId");
            return Console.ReadLine();

        }
        public string GetRecipientAccountId()
        {
            Console.WriteLine("Please Enter Recipient Account number");
            return Console.ReadLine();
        }
    }
}