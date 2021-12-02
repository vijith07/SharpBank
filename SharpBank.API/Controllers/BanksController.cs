using Microsoft.AspNetCore.Mvc;
using SharpBank.Services;
using SharpBank.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharpBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService bankService;

        public BanksController(IBankService bankService)
        {
            this.bankService = bankService;
        }
        // GET: api/<BanksController>
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bankService.GetAllBanks());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // GET api/<BanksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BanksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
