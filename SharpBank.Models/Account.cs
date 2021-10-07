using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Account
    {
        private string accountNumber;
        public string AccountNumber 
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public Account(string id,string name,string pass)
        {
            this.accountNumber = id;
            this.password = pass;
            this.userName = name;
            balance = 0.0m;
        }
    }
}
