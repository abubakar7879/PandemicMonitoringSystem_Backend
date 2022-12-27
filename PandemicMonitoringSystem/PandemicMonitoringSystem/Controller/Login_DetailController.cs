using Dapper;
using Microsoft.AspNetCore.Mvc;
using PandemicMonitoringSystem.Abstraction;
using PandemicMonitoringSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PandemicMonitoringSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login_DetailController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public Login_DetailController(IDbStratrgy _db)
        {
            db = _db;

        }
        [HttpGet]
        public IActionResult get()
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Login_Detail>($@"SELECT * FROM sql6580689.login_detail;").ToList();
                return Ok(result);
            }

        }
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Login_Detail>($@"SELECT * FROM sql6580689.login_detail where Id={id};").FirstOrDefault();
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Login_Detail model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(Id) FROM sql6580689.login_detail");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.Id = Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.login_detail (Id , password, authroization_level) VALUES('{model.Id}' , '{model.password}', '{model.authroization_level}')");
                    return Ok(data);
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }




    }
}
