using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandemicMonitoringSystem.Abstraction;
using System.Threading.Tasks;
using System;
using PandemicMonitoringSystem.Models;
using System.Linq;

namespace PandemicMonitoringSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDbStratrgy db;
        public DepartmentController(IDbStratrgy dbStratrgy)
        {
            db = dbStratrgy;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var con = db.Connection)
                {
                    var data = await con.QueryAsync<department>(@"SELECT id , name FROM sql6580689.department");
                    return Ok(data.ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var data = await con.QueryAsync<department>(@$"SELECT id , name FROM sql6580689.department where id = {id}");
                    return Ok(data.ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(department model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var GetmaxId = await con.QueryAsync<int?>(@"SELECT max(id) FROM sql6580689.department");//get max if from database for such table
                    int Id = 0;
                    if (GetmaxId.FirstOrDefault() != null)
                    {
                        Int32.TryParse(GetmaxId.First().Value.ToString(), out Id); // trying to parse In case we got null
                    }
                    Id++; // incrment 
                    model.id= Id;//adding in column
                    var data = await con.ExecuteAsync(@$"INSERT INTO sql6580689.department (name , id ) VALUES('{model.name}' , '{model.id}')");
                    return Ok(data);
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(department model)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var data = await con.ExecuteAsync(@$"UPDATE sql6580689.department set name = '{model.name}' where id = {model.id}");
                    return Ok(data);
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var con = db.Connection)
                {
                    var data = await con.ExecuteAsync(@$"DELETE FROM sql6580689.department where id = {id}");
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
