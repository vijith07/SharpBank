using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpBank.API.DTOs.Transaction;
using SharpBank.Data;
using SharpBank.Models;
using SharpBank.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharpBank.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IBankService bankService;

        public TransactionsController(ITransactionService transactionService,IMapper mapper, IAccountService accountService,IBankService bankService)
        {
            this.transactionService = transactionService;
            this.mapper = mapper;
            this.accountService = accountService;
            this.bankService = bankService;
        }

        //GET: api/<TransactionsController>

        [HttpGet("{bankId}/{accountId}")]
        public IActionResult Get(Guid bankId, Guid accountId)
        {
            var transactionList = transactionService.GetAllTransactions(bankId, accountId);
            if (transactionList == null)
            {
                return NotFound();
            }
            var transactionsDTO = mapper.Map<IEnumerable<GetTransactionDTO>>(transactionList);
            foreach (var transactionDTO in transactionsDTO)
            {
                var transaction = transactionList.SingleOrDefault(t => t.Id == transactionDTO.Id);
                transactionDTO.SourceBankId = transaction.SourceAccount.BankId;
                transactionDTO.DestinationBankId = transaction.DestinationAccount.BankId;
            }
            return Ok(transactionsDTO);
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var transaction = transactionService.GetTransaction(id);
            var transactionDTO=mapper.Map<GetTransactionDTO>(transaction);
            transactionDTO.SourceBankId = transaction.SourceAccount.BankId;
            transactionDTO.DestinationBankId=transaction.DestinationAccount.BankId;
            return Ok(transactionDTO);
        }

        //// POST api/<TransactionsController>
        [HttpPost("{bankId}/{accountId}")]
        public IActionResult Post(Guid bankId, Guid accountId, [FromBody] CreateTransactionDTO newTransaction)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                var IdClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
                if (IdClaim.Value.ToString() != accountId.ToString())
                {
                    return Forbid();
                }
                if (newTransaction == null || newTransaction.Amount <= 0)
                    return BadRequest();
                decimal transactionCharges = 0;
                if (bankId == newTransaction.DestinationBankId)
                {
                    if (newTransaction.Mode == Models.Enums.Mode.IMPS)
                    {
                        transactionCharges = newTransaction.Amount * bankService.GetBank(bankId).IMPSToSame;
                    }
                    else if (newTransaction.Mode == Models.Enums.Mode.RTGS)
                    {
                        transactionCharges = newTransaction.Amount * bankService.GetBank(bankId).RTGSToSame;
                    }
                }
                if (bankId != newTransaction.DestinationBankId)
                {
                    if (newTransaction.Mode == Models.Enums.Mode.IMPS)
                    {
                        transactionCharges = newTransaction.Amount * bankService.GetBank(bankId).IMPSToOther;
                    }
                    else if (newTransaction.Mode == Models.Enums.Mode.RTGS)
                    {
                        transactionCharges = newTransaction.Amount * bankService.GetBank(bankId).RTGSToOther;
                    }
                }
                decimal netAmount = newTransaction.Amount + transactionCharges;
                var sourceAcc = accountService.GetAccount(bankId, accountId);

                if (sourceAcc.Balance < netAmount)
                {
                    return BadRequest("Insufficient Balance");
                }
                sourceAcc.Balance -= netAmount;
                accountService.UpdateAccount(sourceAcc);
               var destAcc= accountService.GetAccount(newTransaction.DestinationBankId,newTransaction.DestinationAccountId);
                destAcc.Balance += newTransaction.Amount;
                accountService.UpdateAccount(destAcc);
                var t = mapper.Map<Transaction>(newTransaction);
                t.Id=Guid.NewGuid();
                t.SourceAccountId = accountId;
                t.DestinationAccountId = newTransaction.DestinationAccountId;
                t.DestinationAccount = destAcc;
                t.TransactionCharges = transactionCharges;
                t.NetAmount = netAmount;
                t.Mode=newTransaction.Mode;
                t.On=DateTime.Now;
                transactionService.CreateTransaction(t);
                var getTDto=mapper.Map<GetTransactionDTO>(t);
                return Ok(getTDto);
            }
            catch (Exception)
            {
                BadRequest();
                throw;
            }
        }

        [HttpPost("withdraw/{bankId}/{accountId}")]
        public IActionResult Withdraw(Guid bankId, Guid accountId, [FromBody] DWTransactionDTO newTransaction)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                var IdClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
                if (IdClaim.Value.ToString() != accountId.ToString())
                {
                    return Forbid();
                }
                if (newTransaction == null || newTransaction.Amount <= 0)
                    return BadRequest();
                var sourceAcc = accountService.GetAccount(bankId, accountId);

                if (sourceAcc.Balance < newTransaction.Amount)
                {
                    return BadRequest("Insufficient Balance");
                }
                sourceAcc.Balance -= newTransaction.Amount;
                accountService.UpdateAccount(sourceAcc);
                var t = mapper.Map<Transaction>(newTransaction);
                t.Id = Guid.NewGuid();
                t.Id = Guid.NewGuid();
                t.SourceAccountId = accountId;
                t.DestinationAccountId = accountId;
                t.DestinationAccount = sourceAcc;
                t.TransactionCharges = 0m;
                t.NetAmount = newTransaction.Amount;
                t.Mode = Models.Enums.Mode.Other;
                t.Type = Models.Enums.TransactionType.Debit;
                t.On = DateTime.Now;
                transactionService.CreateTransaction(t);
                return Ok(t.Id);
            }
            catch (Exception)
            {
                BadRequest();
                throw;
            }
        }
        [HttpPost("deposit/{bankId}/{accountId}")]
        public IActionResult Deposit(Guid bankId, Guid accountId, [FromBody] DWTransactionDTO newTransaction)
        {
            try
            {
                if (newTransaction == null || newTransaction.Amount <= 0)
                    return BadRequest();
                var sourceAcc = accountService.GetAccount(bankId, accountId);
                sourceAcc.Balance += newTransaction.Amount;
                accountService.UpdateAccount(sourceAcc);
                var t = mapper.Map<Transaction>(newTransaction);
                t.Id = Guid.NewGuid();
                t.Id = Guid.NewGuid();
                t.SourceAccountId = accountId;
                t.DestinationAccountId = accountId;
                t.DestinationAccount = sourceAcc;
                t.TransactionCharges = 0m;
                t.NetAmount = newTransaction.Amount;
                t.Mode = Models.Enums.Mode.Other;
                t.Type = Models.Enums.TransactionType.Credit;
                t.On = DateTime.Now;
                transactionService.CreateTransaction(t);
                return Ok(t.Id);
            }
            catch (Exception)
            {
                BadRequest();
                throw;
            }
        }

    }
}
