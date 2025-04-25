using Microsoft.AspNetCore.Mvc;
using Movies_System.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        #region GET Methods
        // GET: api/<MoviesController>
        [HttpGet("cart")]
        public IEnumerable<Movie> Get()
        {
            return Movie.Read();
        }


        [HttpGet("getByTitle")]
        public IEnumerable<Movie> GetByTitle(string title)
        {
            return Movie.GetByTitle(title);
        }


        [HttpGet("filterByDate")]
        public IEnumerable<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            return Movie.GetByReleaseDate(startDate, endDate);
        }
        #endregion

        #region POST Methods
        // POST api/<MoviesController>
        [HttpPost("addToCart")]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (Movie.Insert(movie))
            {
                return Ok(new
                {
                    Message = "Movie added successfully",
                    Success = true
                });
            }
            return BadRequest(new
            {
                Message = "Movie already exists",
                Success = false
            });
        }
        #endregion

        #region Delete Methods
        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return Movie.RemoveFromList(id);
        }
        #endregion
    }
}