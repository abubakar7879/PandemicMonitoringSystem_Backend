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
    public class Vaccination_recordController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public Vaccination_recordController(IDbStratrgy _db)
        {
            db = _db;

        }
        [HttpGet]
        public IActionResult get()
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Vaccination_recordController>($@"SELECT * FROM sql6580689.vaccination_record;").ToList();
                return Ok(result);
            }

        }
        [HttpGet("{vaccine_id}")]
        public IActionResult get(int vaccine_id)
        {
            using (var con = db.Connection)
            {
                var result = con.Query<Vaccination_recordController>($@"SELECT * FROM sql6580689.vaccination_record where Id={vaccine_id};").FirstOrDefault();
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Vaccination_record model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(vaccine_id) FROM sql6580689.vaccination_record");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.vaccine_id = Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.family (vaccine_id , person_id, dosage_num) VALUES('{model.vaccine_id}' , '{model.person_id}', '{model.dosage_num}')");
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
