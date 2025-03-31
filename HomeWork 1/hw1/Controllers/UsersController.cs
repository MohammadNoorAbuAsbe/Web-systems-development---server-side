using hw1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hw1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Models.User.Read();
        }

        [HttpPost]
        public bool Post([FromBody] User newUser)
        {
            return Models.User.Insert(newUser);
        }
    }
}
