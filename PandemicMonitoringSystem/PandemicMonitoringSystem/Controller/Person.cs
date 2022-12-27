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
    public class PersonController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public PersonController(IDbStratrgy _db)
        {
            db = _db;

        }
        [HttpGet]
        public IActionResult get()
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Person>($@"SELECT * FROM sql6580689.person;").ToList();
                return Ok(result);
            }

        }
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Person>($@"SELECT * FROM sql6580689.person where Id={id};").FirstOrDefault();
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Person model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(Id) FROM sql6580689.person");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.id = Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.person (id , first_name, last_name, age, address_id, family_id) VALUES('{model.id}' , '{model.first_name}', '{model.last_name}', '{model.age}', '{model.address_id}', '{model.family_id}')");
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
