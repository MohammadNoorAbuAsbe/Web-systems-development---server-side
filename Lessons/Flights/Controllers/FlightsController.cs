using Flights.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        // GET: api/<FlightsController>
        [HttpGet]
        public IEnumerable<Flight> Get()
        {
            return Flight.Read();
        }88

        [HttpGet("GetFiltered")]
        public IEnumerable<Flight> GetFilteredQuery(string from, string to)
        {
            return Flight.ReadFiltered(from, to);
        }

        [HttpGet("GetFiltered/from/{from}/to/{to}")]
        public IEnumerable<Flight> GetFilteredResourse(string from, string to)
        {
            return Flight.ReadFiltered(from, to);
        }

        // GET api/<FlightsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FlightsController>
        [HttpPost]
        public bool Post([FromBody] Flight flight)
        {

            return flight.Insert();
        }

        // PUT api/<FlightsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlightsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
