using System;
using System.Collections.Generic;

namespace SharpBank.Models
{
    public static class BankManager
    {
        private static int count;
        private static Dictionary<string, Bank> Banks;
        private static Dictionary<string, Transaction> Transactions;

        static BankManager()
        {
            count = 0;
            Banks = new Dictionary<string, Bank>();
            Transactions = new Dictionary<string, Transaction>();
        }

    }
}
