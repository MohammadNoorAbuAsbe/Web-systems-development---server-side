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
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Register([FromBody] User user)
        {
            if (Models.User.Register(user))
            {
                return Ok(new
                {
                    Message = "User registered successfully",
                    Success = true
                });
            }
            return BadRequest(new
            {
                Message = "Email already exists",
                Success = false
            });
        }


        // POST: api/<UsersController>/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var userResponse = Models.User.Login(request.Email, request.Password);
            if (userResponse != null)
            {
                return Ok(userResponse);
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

    public class AddMovieRequest
    {
        public string Email { get; set; }
        public Movie Movie { get; set; }
    }
}
