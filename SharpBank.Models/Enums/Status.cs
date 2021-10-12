using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models.Enums
{
    public enum Status
    {
        Active=1,
        Inactive,
        Dormant,
        Closed,
        Absconded,
        OnParole,
        Missing,
        Broke,
        Bankrupt
    }
}
