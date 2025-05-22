using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Movies_System.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public DBservices()
    {

    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the appsettings.json 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    #region (One Time Use / Unneeded now)
    //--------------------------------------------------------------------------------------------------
    // This method inserts all Movies into the Movies table (One Time Use / Unneeded now)
    //--------------------------------------------------------------------------------------------------
    //public int InsertBatch(List<Movie> movies)
    //{
    //    using (SqlConnection con = connect("myProjDB"))
    //    using (SqlCommand cmd = new SqlCommand("SP_InsertMovies_2025", con))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        DataTable movieTable = new DataTable();
    //        movieTable.Columns.Add("Url", typeof(string));
    //        movieTable.Columns.Add("PrimaryTitle", typeof(string));
    //        movieTable.Columns.Add("Description", typeof(string));
    //        movieTable.Columns.Add("PrimaryImage", typeof(string));
    //        movieTable.Columns.Add("Year", typeof(int));
    //        movieTable.Columns.Add("ReleaseDate", typeof(DateTime));
    //        movieTable.Columns.Add("Language", typeof(string));
    //        movieTable.Columns.Add("Budget", typeof(double));
    //        movieTable.Columns.Add("GrossWorldwide", typeof(double));
    //        movieTable.Columns.Add("Genres", typeof(string));
    //        movieTable.Columns.Add("IsAdult", typeof(bool));
    //        movieTable.Columns.Add("RuntimeMinutes", typeof(int));
    //        movieTable.Columns.Add("AverageRating", typeof(double));
    //        movieTable.Columns.Add("NumVotes", typeof(int));

    //        foreach (Movie movie in movies)
    //        {
    //            movieTable.Rows.Add(
    //                movie.Url,
    //                movie.PrimaryTitle,
    //                movie.Description,
    //                movie.PrimaryImage,
    //                movie.Year,
    //                movie.ReleaseDate,
    //                movie.Language,
    //                movie.Budget,
    //                movie.GrossWorldwide,  // Use 0 if not provided
    //                movie.Genres,
    //                movie.IsAdult,
    //                movie.RuntimeMinutes,
    //                movie.AverageRating,    // Use 0 if not provided
    //                movie.NumVotes         // Use 0 if not provided
    //            );
    //        }

    //        cmd.Parameters.Add(new SqlParameter("@Movies", SqlDbType.Structured)
    //        {
    //            TypeName = "dbo.MovieListType",
    //            Value = movieTable
    //        });

    //        try
    //        {
    //            int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //            return numEffected;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //        }

    //    }
    //}

    //--------------------------------------------------------------------------------------------------
    // This method inserts a Movie into the Movies table
    //--------------------------------------------------------------------------------------------------
    #endregion

    #region Getters

    #region Users Getters
    public List<User> GetUsers()
    {
        return GetUsersHelper("SP_GetUsers_2025", new Dictionary<string, object>(), users => users);
    }

    public User? GetUserByEmail(string email)
    {
        var parameters = new Dictionary<string, object> { { "@email", email } };
        return GetUsersHelper("SP_GetUserByEmail_2025", parameters, users => users.FirstOrDefault());
    }

    public User? GetUserById(int id)
    {
        var parameters = new Dictionary<string, object> { { "@id", id } };
        return GetUsersHelper("SP_GetUserById_2025", parameters, users => users.FirstOrDefault());
    }

    public User? GetUserByName(string name)
    {
        var parameters = new Dictionary<string, object> { { "@name", name } };
        return GetUsersHelper("SP_GetUserByName_2025", parameters, users => users.FirstOrDefault());
    }

    public List<User> GetUserByActive(bool isActive)
    {
        var parameters = new Dictionary<string, object> { { "@isActive", isActive } };
        return GetUsersHelper("SP_GetUsersByActive_2025", parameters, users => users);
    }

    public List<User> GetUserByDeletedAt(DateTime deletedAt)
    {
        var parameters = new Dictionary<string, object> { { "@deletedAt", deletedAt } };
        return GetUsersHelper("SP_GetUsersByDeletedAt_2025", parameters, users => users);
    }

    public User? LoginUser(string email)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@email", email);
        return GetUsersHelper("SP_LoginUser_2025", paramDic, users => users.FirstOrDefault());
    }

    private T GetUsersHelper<T>(string storedProcedureBaseName, Dictionary<string, object> parameters, Func<List<User>, T> resultSelector)
    {
        string spName = storedProcedureBaseName;
        List<User> users = ExecuteSQLCommand_ReturnTList(parameters, spName, MapUser);
        return resultSelector(users);
    }
    #endregion

    #region Movies Getters
    public List<Movie> GetMoviesByDate(DateTime startDate, DateTime endDate)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@StartDate", startDate },
            { "@EndDate", endDate }
        };
        return GetMoviesHelper("SP_GetMoviesByDateRange", parameters, movies => movies);
    }

    public List<Movie> GetMoviesByTitle(string title)
    {
        var parameters = new Dictionary<string, object> { { "@Title", title } };
        return GetMoviesHelper("SP_GetMoviesByTitle_2025", parameters, movies => movies);
    }

    public List<Movie> GetAllMovies()
    {
        return GetMoviesHelper("SP_GetAllMovies_2025", new Dictionary<string, object>(), movies => movies);
    }

    public PaginationResponse GetLimitedMovies(int currentPage, int pageSize, string? title = null, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@page", currentPage },
            { "@pageSize", pageSize },
            { "@title", title ?? (object)DBNull.Value },
            { "@fromDate", fromDate ?? (object)DBNull.Value },
            { "@toDate", toDate ?? (object)DBNull.Value }
        };

        List<Movie> movies = new List<Movie>();
        int totalCount = 0;

        using (var con = connect("myProjDB"))
        using (var cmd = CreateCommandWithStoredProcedureGeneral("SP_GetLimitedMovies_2025", con, parameters))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                movies.Add(MapMovie(reader));
            }

            if (reader.NextResult() && reader.Read())
            {
                totalCount = reader.GetInt32(0);
            }
        }

        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PaginationResponse
        {
            Movies = movies,
            TotalPages = totalPages
        };
    }

    public int GetMoviesCount()
    {
        var parameters = new Dictionary<string, object>();
        string spName = "SP_GetMoviesCount_2025";
        int numOfMovies = ExecuteSQLCommand_ReturnInt(parameters, spName);
        return numOfMovies;
    }

    private T GetMoviesHelper<T>(string storedProcedureBaseName, Dictionary<string, object> parameters, Func<List<Movie>, T> resultSelector)
    {
        string spName = storedProcedureBaseName;
        List<Movie> movies = ExecuteSQLCommand_ReturnTList(parameters, spName, MapMovie);
        return resultSelector(movies);
    }
    #endregion

    public List<RentedMovie> GetUserRentedMovies(int userId)
    {
        var parameters = new Dictionary<string, object> { { "@userID", userId } };
        return GetRentedMoviesHelper("SP_GetUserRentedMovies_2025", parameters, rentedMovies => rentedMovies);
    }

    private T GetRentedMoviesHelper<T>(string storedProcedureBaseName, Dictionary<string, object> parameters, Func<List<RentedMovie>, T> resultSelector)
    {
        string spName = storedProcedureBaseName;
        List<RentedMovie> rentedMovies = ExecuteSQLCommand_ReturnTList(parameters, spName, MapRentedMovie);
        return resultSelector(rentedMovies);
    }
    #endregion

    #region Inserts
    public User? RegisterUser(User user)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", user.Name);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        string spName = "SP_InsertUser_2025";
        return GetUsersHelper(spName, paramDic, users => users.FirstOrDefault());
    }

    public int AddMovie(Movie movie)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@url", movie.Url);
        paramDic.Add("@primaryTitle", movie.PrimaryTitle);
        paramDic.Add("@description", movie.Description);
        paramDic.Add("@primaryImage", movie.PrimaryImage);
        paramDic.Add("@year", movie.Year);
        paramDic.Add("@releaseDate", movie.ReleaseDate);
        paramDic.Add("@language", movie.Language);
        paramDic.Add("@budget", movie.Budget);
        paramDic.Add("@grossWorldwide", movie.GrossWorldwide);
        paramDic.Add("@genres", movie.Genres);
        paramDic.Add("@isAdult", movie.IsAdult);
        paramDic.Add("@runtimeMinutes", movie.RuntimeMinutes);
        paramDic.Add("@averageRating", movie.AverageRating);
        paramDic.Add("@numVotes", movie.NumVotes);
        string spName = "SP_InsertMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }

    public int RentMovie(int userId, int movieId, DateTime rentEnd)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@userId", userId);
        paramDic.Add("@movieId", movieId);
        paramDic.Add("@rentEnd", rentEnd);
        string spName = "SP_RentMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }
    #endregion

    #region Updates
    public User? UpdateUser(int id, User user)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        paramDic.Add("@name", user.Name);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        paramDic.Add("@active", user.Active);
        string spName = "SP_UpdateUser_2025";
        return GetUsersHelper(spName, paramDic, users => users.FirstOrDefault());
    }

    public int UpdateMovie(int id,Movie movie)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        paramDic.Add("@url", movie.Url);
        paramDic.Add("@primaryTitle", movie.PrimaryTitle);
        paramDic.Add("@description", movie.Description);
        paramDic.Add("@primaryImage", movie.PrimaryImage);
        paramDic.Add("@year", movie.Year);
        paramDic.Add("@releaseDate", movie.ReleaseDate);
        paramDic.Add("@language", movie.Language);
        paramDic.Add("@budget", movie.Budget);
        paramDic.Add("@grossWorldwide", movie.GrossWorldwide);
        paramDic.Add("@genres", movie.Genres);
        paramDic.Add("@isAdult", movie.IsAdult);
        paramDic.Add("@runtimeMinutes", movie.RuntimeMinutes);
        paramDic.Add("@averageRating", movie.AverageRating);
        paramDic.Add("@numVotes", movie.NumVotes);
        string spName = "SP_UpdateMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }

    
    public int PassMovie(int movieId, int currentUserId, int newUserId)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@movieId", movieId);
        paramDic.Add("@currentUserId", currentUserId);
        paramDic.Add("@newUserId", newUserId);
        string spName = "SP_PassMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }

    #endregion

    #region Deletes
    public int DeleteUser(int id)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        string spName = "SP_DeleteUser_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }

    public int DeleteMovie(int id)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        string spName = "SP_DeleteMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }

    public int DeleteRentedMovie(int userId, int movieId, DateTime rentEnd)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@userId", userId);
        paramDic.Add("@movieId", movieId);
        paramDic.Add("@rentEnd", rentEnd);
        string spName = "SP_DeleteRentedMovie_2025";
        return ExecuteSQLCommand(paramDic, spName);
    }
    #endregion

    #region Mappers
    private User MapUser(SqlDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Email = reader.GetString(2),
            Password = reader.GetString(3),
            Active = reader.GetBoolean(4)
        };
    }

    private Movie MapMovie(SqlDataReader reader)
    {
        return new Movie
        {
            Id = reader.GetInt32(0),
            Url = reader.GetString(1),
            PrimaryTitle = reader.GetString(2),
            Description = reader.GetString(3),
            PrimaryImage = reader.GetString(4),
            Year = reader.GetInt32(5),
            ReleaseDate = reader.GetDateTime(6),
            Language = reader.GetString(7),
            Budget = reader.GetDouble(8),
            GrossWorldwide = reader.GetDouble(9),
            Genres = reader.GetString(10),
            IsAdult = reader.GetBoolean(11),
            RuntimeMinutes = reader.GetInt32(12),
            AverageRating = (float)reader.GetDouble(13),
            NumVotes = reader.GetInt32(14),
            PriceToRent = reader.GetInt32(15),
            RentalCount = reader.GetInt32(16),
            DeletedAt = reader.IsDBNull(17) ? (DateTime?)null : reader.GetDateTime(17)
        };
    }

    private RentedMovie MapRentedMovie(SqlDataReader reader)
    {
        return new RentedMovie
        {
            Movie = new Movie
            {
                Id = reader.GetInt32(0),
                Url = reader.GetString(1),
                PrimaryTitle = reader.GetString(2),
                Description = reader.GetString(3),
                PrimaryImage = reader.GetString(4),
                Year = reader.GetInt32(5),
                ReleaseDate = reader.GetDateTime(6),
                Language = reader.GetString(7),
                Budget = reader.GetDouble(8),
                GrossWorldwide = reader.GetDouble(9),
                Genres = reader.GetString(10),
                IsAdult = reader.GetBoolean(11),
                RuntimeMinutes = reader.GetInt32(12),
                AverageRating = (float)reader.GetDouble(13),
                NumVotes = reader.GetInt32(14),
                PriceToRent = reader.GetInt32(15),
                RentalCount = reader.GetInt32(16),
                DeletedAt = reader.IsDBNull(17) ? (DateTime?)null : reader.GetDateTime(17)
            },
            RentStart = reader.GetDateTime(20),
            RentEnd = reader.GetDateTime(21),
            TotalPrice = reader.GetInt32(22),
            DeletedAt = reader.IsDBNull(23) ? (DateTime?)null : reader.GetDateTime(23),
            User = new User
            {
                Id = reader.GetInt32(24),
                Name = reader.GetString(25),
                Email = reader.GetString(26),
                Password = reader.GetString(27),
                Active = reader.GetBoolean(28),
                DeletedAt = reader.IsDBNull(29) ? (DateTime?)null : reader.GetDateTime(29)
            }
        };
    }
    #endregion

    #region SQL commands
    public int ExecuteSQLCommand_ReturnInt(Dictionary<string, object> paramDic, string spName)
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        cmd = CreateCommandWithStoredProcedureGeneral(spName, con, paramDic);       // create the command
        try
        {
            reader = cmd.ExecuteReader(); // execute the command
            if (reader.Read())
            {
                int numEffected = reader.GetInt32(0);
                return numEffected;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            reader?.Close();
            con?.Close();
        }
    }

    public List<T> ExecuteSQLCommand_ReturnTList<T>(Dictionary<string, object> paramDic, string spName, Func<SqlDataReader, T> mapper)
    {

        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        List<T> resultList = new List<T>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedureGeneral(spName, con, paramDic);       // create the command

        try
        {
            reader = cmd.ExecuteReader(); // execute the command
            while (reader.Read())
            {
                T item = mapper(reader);
                resultList.Add(item);
            }
            return resultList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            reader?.Close();
            con?.Close();
        }

    }

    public int ExecuteSQLCommand(Dictionary<string, object> paramDic, string spName)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedureGeneral(spName, con, paramDic);       // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }


        return cmd;
    }
    #endregion
}

