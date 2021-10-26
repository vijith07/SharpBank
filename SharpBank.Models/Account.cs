﻿using SharpBank.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public AccountType Type { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
