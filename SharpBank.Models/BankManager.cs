using System;
using System.Collections.Generic;

namespace SharpBank.Models
{
    public static class BankManager
    {
        private static int count;
        public static Dictionary<string, Bank> Banks;
        public static List< Transaction> Transactions;

        static BankManager()
        {
            count = 0;
            Banks = new Dictionary<string, Bank>();
            Transactions = new List< Transaction>();
        }

    }
}
