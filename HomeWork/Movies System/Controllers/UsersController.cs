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
            try
            {
                if (Models.User.Register(user))
                {
                    return Ok(new RegisterResponse
                    {
                        Message = "User registered successfully",
                        Success = true
                    });
                }
                return BadRequest(new RegisterResponse
                {
                    Message = "Email already exists",
                    Success = false
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new RegisterResponse
                {
                    Message = ex.Message,
                    Success = false
                });
            }
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

        #region Delete Methods
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return Models.User.DeleteUserById(id);
        }
        #endregion

        #region PUT Methods
        // DELETE api/<MoviesController>/5
        [HttpPut("Update/{id}")]
        public bool Put(int id, [FromBody] User user)
        {
            return Models.User.UpdateUser(id, user);
        }
        #endregion

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
