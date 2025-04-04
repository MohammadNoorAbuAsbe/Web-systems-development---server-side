using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region GET Methods
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Models.User.Read();
        }
        #endregion

        #region POST Methods
        // POST api/<UsersController>
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return Models.User.Insert(user);
        }
        #endregion
    }
}
