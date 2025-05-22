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
        [HttpGet()]
        public IEnumerable<Movie> Get()
        {
            return Movie.Read();
        }

        [HttpGet("getPaginatedMovies")]
        public PaginationResponse GetPagination(int currentPage, int pageSize, string? title = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return Movie.GetPagination(currentPage, pageSize, title, fromDate, toDate);
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

        [HttpGet("GetRentedMovies")]
        public IEnumerable<RentedMovie> GetRentedMovies(int userId)
        {
            return RentedMovie.GetUserRentedMovies(userId);
        }
        #endregion

        #region POST Methods
        // POST api/<MoviesController>
        [HttpPost("addNewMovie")]
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

        [HttpPost("rentMovie")]
        public IActionResult RentMovie([FromBody] RentRequest rentRequest)
        {
            try
            {
                if (Movie.Rent(rentRequest.UserId, rentRequest.MovieId, rentRequest.RentEnd))
                {
                    return Ok(new
                    {
                        Message = "Movie rented successfully",
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

        [HttpDelete("deleteRented")]
        public bool DeleteRented([FromBody] DeleteRequest req)
        {
            return Models.Movie.DeleteRentedMovie(req.UserId, req.MovieId, req.RentEnd);
        }
        #endregion

        #region PUT Methods
        // UPDATE api/<MoviesController>/5
        [HttpPut("Update/{id}")]
        public bool Put(int id, Movie movie)
        {
            return Models.Movie.UpdateMovie(id, movie);
        }


        [HttpPut("passMovie")]
        public bool PassMovie([FromBody] PassMovieRequest req)
        {
            return Models.RentedMovie.PassMovie(req.movieId, req.currentUserId, req.newUserId);
        }
        #endregion

        #region One Time Use (Unneeded now)
        //[HttpPost("insertMoviesFromJson")]
        //public int InsertJsonToDataBase([FromBody] List<Movie> movies)
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.InsertBatch(movies);
        //}
        #endregion

        public class RentRequest
        {
            public int UserId { get; set; }
            public int MovieId { get; set; }
            public DateTime RentEnd { get; set; }
        }

        public class PassMovieRequest
        {
            public int movieId { get; set; }
            public int currentUserId { get; set; }
            public int newUserId { get; set; }
        }

        public class DeleteRequest
        {
            public int UserId { get; set; }
            public int MovieId { get; set; }
            public DateTime RentEnd { get; set; }
        }
    }
}