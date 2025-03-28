using Microsoft.AspNetCore.Mvc;
using FootBallApp.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootBallApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Messi", "Xavi" , "Iniesta" };
        }

        // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            if (id == 1) {
                return new Player("Messi","miami","center", 36);
            }
            return new Player("Xavi", "zzz", "striker", 20);
        }

        // POST api/<PlayersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
