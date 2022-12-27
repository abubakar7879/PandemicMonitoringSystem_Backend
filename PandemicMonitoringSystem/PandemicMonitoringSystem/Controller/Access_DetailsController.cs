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
    public class Access_DetailsController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public Access_DetailsController(IDbStratrgy _db)
        {
            db = _db;

        }
        [HttpGet]
        public IActionResult get()
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Person>($@"SELECT * FROM sql6580689.access_detail;").ToList();
                return Ok(result);
            }

        }
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Access_Details>($@"SELECT * FROM sql6580689.access_detail where Id={id};").FirstOrDefault();
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Access_Details model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(Id) FROM sql6580689.access_detail");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.Id = Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.access_detail (Id , Date) VALUES('{model.Id}' , '{model.Date}')");
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
