using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SharpBank.API.DTOs.Account;
using SharpBank.Models;
using SharpBank.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharpBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService accountService,IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }
        // GET: api/<AccountsController>
        [HttpGet("{bankId}")]
        public IActionResult Get(Guid bankId)
        {
            try
            {
                var all = accountService.GetAllAccounts(bankId);
                var allDTO = mapper.Map<IEnumerable<GetAccountDTO>>(all);
                return Ok(allDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // GET api/<AccountsController>/5
        [HttpGet("{bankId}/{id}")]
        public IActionResult Get(Guid bankId,Guid id)
        {
            try
            {
                var acc = accountService.GetAccount(bankId,id);
                var accDTO=mapper.Map<GetAccountDTO>(acc);
                return Ok(accDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpGet("balance/{bankId}/{id}")]
        public IActionResult GetBalance(Guid bankId, Guid id)
        {
            var acc=accountService.GetAccount(bankId, id);
            if (accountService.GetAccount(bankId,id)==null)
                return BadRequest();
            var accDTO=mapper.Map<GetAccountBalanceDTO>(acc);
            return Ok(accDTO);
        }
        // POST api/<AccountsController>
        [HttpPost("{bankId}")]
        public IActionResult Post(Guid bankId,[FromBody] CreateAccountDTO accountDTO)
        {
            try
            {
                if (accountDTO == null)
                    return BadRequest();
                var acc = mapper.Map<Account>(accountDTO);
                acc.Password=BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
                acc.Type = Models.Enums.AccountType.Customer;
                acc.Status = Models.Enums.Status.Active;
                acc.Id=Guid.NewGuid();
                acc.BankId=bankId;
                var createdAcc=accountService.CreateAccount(acc);
                return CreatedAtAction(nameof(Get), new {bankId=bankId,id=createdAcc.Id},mapper.Map<GetAccountDTO>(createdAcc));
            }
            catch (Exception)
            {

                return BadRequest(accountDTO);
            }
           

        }

        // PUT api/<AccountsController>/5
        [HttpPut("balance/{bankId}/{id}")]
        public IActionResult PutBalance(Guid bankId, Guid id, [FromBody]UpdateAccountBalanceDTO updateAccountBalance)
        {
            if (updateAccountBalance == null)
                return BadRequest();
            var acc= accountService.GetAccount(bankId, id);
            acc.Balance+=updateAccountBalance.Balance;
            accountService.UpdateAccount(acc);
            return Ok();
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
