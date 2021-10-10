using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBank.Models;
using SharpBank.Models.Exceptions;

namespace SharpBank.Services
{
   public static class BankManagerServices
    {
        private static string GenerateIFSC()
        {
            string code = (Database.Banks.Count + 1).ToString();
            return code;
        }

    }
}
