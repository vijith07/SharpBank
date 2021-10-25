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
                Console.WriteLine(s)
            }
            Console.WriteLine("Please Enter The Code Carefully :");

            return Console.ReadLine();
        }
        public Gender GetGender()
        {
            Console.WriteLine("Please Enter Your Gender (Male/Female/Other) :");
            Enum.TryParse(Console.ReadLine(), out Gender gender);
            return gender;
        }
        public AccountType GetAccountType()
        {
            Console.WriteLine("Please Enter The Account Type (Staff/Customer) :");
            Enum.TryParse(Console.ReadLine(), out AccountType type);
            return type;
        }
        public Mode GetTransactionMode()
        {
            Console.WriteLine("Please Enter The Transaction Mode (IMPS/RTGS) :");
            Enum.TryParse(Console.ReadLine(), out Mode mode);
            return mode;
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
        public List<string> GetRecipient()
        {
            List<string> res = new List<string>();
            Console.WriteLine("Please Enter Recipient BankId");
            res.Add(Console.ReadLine());
            Console.WriteLine("Please Enter Recipient Account number");
            res.Add(Console.ReadLine());
            return res;
        }
    }
}