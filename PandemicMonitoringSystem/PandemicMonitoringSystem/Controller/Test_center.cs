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
    public class Test_CenterController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public Test_CenterController(IDbStratrgy _db)
        {
            db = _db;

        }
        [HttpGet]
        public IActionResult get()
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Test_Center>($@"SELECT * FROM sql6580689.test_center;").ToList();
                return Ok(result);
            }

        }
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Test_Center>($@"SELECT * FROM sql6580689.test_center where Id={id};").FirstOrDefault();
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Test_Center model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(Id) FROM sql6580689.test_center");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.id = Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.test_center (id , address, capacity , name) VALUES('{model.id}' , '{model.address}', '{model.capacity}', '{model.name}')");
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
