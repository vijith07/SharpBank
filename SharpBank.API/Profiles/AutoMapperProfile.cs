﻿using AutoMapper;
using SharpBank.API.DTOs.Account;
using SharpBank.API.DTOs.Bank;
using SharpBank.API.DTOs.Transaction;
using SharpBank.Models;

namespace SharpBank.API.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBankDTO,Bank>();
            CreateMap<UpdateBankDTO, Bank>();
            CreateMap<Bank,GetBankDTO>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<UpdateAccountBalanceDTO, Account>();
            CreateMap<AuthenticateAccountDTO, Account>();
            CreateMap<Account, GetAccountDTO>();
            CreateMap<Account, GetAccountBalanceDTO>();
            CreateMap<Transaction, GetTransactionDTO>();
            CreateMap<DWTransactionDTO, Transaction>();
        }
    }
}
