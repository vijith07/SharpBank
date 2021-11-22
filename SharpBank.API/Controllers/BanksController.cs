using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SharpBank.API.Models;

namespace SharpBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBanks()
        {
            List<Bank> banks = new List<Bank>();
            using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;" +
                "pwd=password321*;database=sharpbank"))
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM bank;";
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bank bank = new Bank();
                    bank.id = reader["id"].ToString();
                    bank.name = reader["name"].ToString();
                    bank.created_by = reader["created_by"].ToString();
                    bank.updated_by = reader["updated_by"].ToString();
                    bank.created_on = (DateTime)reader["created_on"];
                    bank.updated_on = (DateTime)reader["updated_on"];
                    bank.IMPSToOther = (decimal)reader["IMPSToOther"];
                    bank.IMPSToSame = (decimal)reader["IMPSToSame"];
                    bank.RTGSToOther = (decimal)reader["RTGSToOther"];
                    bank.RTGSToSame = (decimal)reader["RTGSToSame"];
                    bank.default_currency = reader["default_currency"].ToString().ToUpper();
                    banks.Add(bank);


                }

            }
            return Ok(banks);
        }
    }
}
