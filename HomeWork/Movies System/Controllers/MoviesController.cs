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
            try
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
            catch (Exception ex) 
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }
        #endregion

        #region Delete Methods
        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
         public bool Delete(int id)
        {
            return Models.Movie.DeleteMovieById(id);
        }
        #endregion

        #region PUT Methods
        // UPDATE api/<MoviesController>/5
        [HttpPut("Update/{id}")]
        public bool Put(int id, Movie movie)
        {
            return Models.Movie.UpdateMovie(id, movie);
        }
        #endregion

        #region One Time Use (Unneeded now)
        [HttpPost("insertMoviesFromJson")]
        public int InsertJsonToDataBase([FromBody] List<Movie> movies)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertBatch(movies);
        }
        #endregion
    }
}