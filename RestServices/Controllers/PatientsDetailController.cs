using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProgram;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsDetailController : ControllerBase
    {
        JsonCRUDprogram crud = new JsonCRUDprogram();

        // GET: api/<PatientsDetailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientsDetailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientsDetailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientsDetailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientsDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
