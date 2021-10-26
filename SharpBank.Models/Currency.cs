﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
