using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        #region GET Methods
        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return Movie.Read();
        }


        [HttpGet("GetByTitle")]
        public IEnumerable<Movie> GetByTitle(string title)
        {
            return Movie.GetByTitle(title);
        }


        [HttpGet("from/{startDate}/until/{endDate}")]
        public IEnumerable<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            return Movie.GetByReleaseDate(startDate, endDate);
        }
        #endregion

        #region POST Methods
        // POST api/<MoviesController>
        [HttpPost]
        public bool Post([FromBody] Movie movie)
        {
            return Movie.Insert(movie);
        }
        #endregion

        #region DELETE Methods
        [HttpDelete]
        public bool DeleteMovie(string id)
        {
            return Movie.DeleteById(id);
        }
        #endregion

    }
}
