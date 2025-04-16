using Microsoft.AspNetCore.Mvc;
using Movies_System.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies_System.Controllers
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
        // POST: api/<UsersController>/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Register([FromBody] User user)
        {
            if (Models.User.Register(user))
            {
                return Ok("User registered successfully");
            }
            return BadRequest("Email already exists");
        }

        // POST: api/<UsersController>/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (Models.User.Login(request.Email, request.Password))
            {
                return Ok("Login successful");
            }
            return Unauthorized("Invalid email or password");
        }
        #endregion
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
