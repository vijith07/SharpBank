using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Bank
    {
        private string bankName;
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        private string ifsc;
        public string IFSC
        {
            get { return ifsc; }
            set { ifsc = value; }
        }
        private Dictionary<string, Account> accounts;
        

        public Bank(string name,string code)
        {
            this.bankName = name;
            this.ifsc = code;
            accounts = new Dictionary<string, Account>();
        }

        public Account getAccount(string id)
        {
            return accounts[id];
        }

        public void addAccount(Account acc)
        {
            accounts.Add(acc.AccountNumber,acc);
        }

        public void setAccount(string id,Account acc) {
            accounts[id] = acc;
        }

        public void RemoveAccount(string accountNumber)
        {
            accounts.Remove(accountNumber);
        }
        public int Count { get { return accounts.Count;} }
        
    }
}
