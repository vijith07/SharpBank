using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpBank.API.DTOs.Bank;
using SharpBank.Models;
using SharpBank.Services;
using SharpBank.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharpBank.API.Controllers
{
    [Authorize(Roles="Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService bankService;
        private readonly ICurrencyService currencyService;
        private readonly IMapper mapper;

        public BanksController(IBankService bankService,ICurrencyService currencyService, IMapper mapper)
        {
            this.bankService = bankService;
            this.currencyService = currencyService;
            this.mapper = mapper;
        }
        // GET: api/<BanksController>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var all = bankService.GetAllBanks();
                var allDTO=mapper.Map<IEnumerable<GetBankDTO>>(all);
                return Ok(allDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [AllowAnonymous]

        // GET api/<BanksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // POST api/<BanksController>
        [Authorize(Roles="Staff")]
        [HttpPost]
        public IActionResult Post([FromBody]CreateBankDTO newBank)
        {
            if(newBank == null)
            {
                return BadRequest(newBank);
            }
            var tempBank = mapper.Map<Bank>(newBank);
            tempBank.Id=Guid.NewGuid();
            tempBank.DefaultCurrencyId = currencyService.GetCurrencyFromCode("INR").Id;
            tempBank.CreatedOn = DateTime.Now;
            tempBank.UpdatedBy = tempBank.CreatedBy;
            tempBank.UpdatedOn = DateTime.Now;

            var createdBank=bankService.CreateBank(tempBank); 

            return Ok(createdBank);
        }

        // PUT api/<BanksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BanksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
