using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.CLI.Enums
{
    public enum StaffOptions
    {
         CreateAccount=1,
         GetAccountDetails,
         UpdateAcount,
         DeleteAccount,
         AddIMPSSame,
         AddIMPSOther,
         AddRTGSSame,
         AddRTGSOther,
         ShowAccountTransactionHistory,
         ShowBankTransactionHistory,
         RevertTransaction,
         AddNewCurrency,
         Back,
         Exit
    }
}
