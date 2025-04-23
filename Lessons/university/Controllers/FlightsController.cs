using Microsoft.AspNetCore.Mvc;
using RuppinProj.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RuppinProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        // GET: api/<FlightsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FlightsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FlightsController>
        [HttpPost]
        public void Post([FromBody] Flight flight)
        {
            flight.Insert();
        }

        // PUT api/<FlightsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlightsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO, call Flight Delete method
            return Ok(id);
        }
    }
}
