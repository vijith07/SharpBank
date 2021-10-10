using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.CLI
{
    public static class Inputs
    {
        public static string GetAccountNumber()
        {
            Console.WriteLine("Please Enter Your Account Number :");
            return Console.ReadLine();
        }
        public static string GetPassword()
        {
            Console.WriteLine("Please Enter Your Password :");
            return Console.ReadLine();
        }
        public static string GetName()
        {
            Console.WriteLine("Please Enter Your Name :");
            return Console.ReadLine();
        }
        public static int GetSelection()
        {
            try
            {
                Console.WriteLine("Please Enter Your Selection :");

                return Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException e)
            {
                Console.WriteLine("Invalid Selection");
            }
            //Goback
            return -1;
        }
        public static decimal GetAmount()
        {
            Console.WriteLine("Please Enter The Amount :");
            return Convert.ToDecimal(Console.ReadLine());
        }
        public static List<string> GetRecipient()
        {
            List<string> res = new List<string>();
            Console.WriteLine("Please Enter Recipient IFSC");
            res.Add(Console.ReadLine());
            Console.WriteLine("Please Enter Recipient Account number");
            res.Add(Console.ReadLine());
            return res;
        }
    }
}
