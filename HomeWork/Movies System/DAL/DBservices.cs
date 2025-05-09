using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Movies_System.Models;
using Movies_System.DAL;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    private readonly bool _isTestMode;
    public DBservices()
    {
        _isTestMode = DbConfig.IsTestMode;
    }

    public DBservices(bool isTestMode)
    {
        _isTestMode = isTestMode;
    }

    private string GetStoredProcedureName(string baseName)
    {
        return _isTestMode ? $"{baseName}_Test" : baseName;
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
    public int InsertBatch(List<Movie> movies)
    {
        using (SqlConnection con = connect("myProjDB"))
        using (SqlCommand cmd = new SqlCommand("SP_InsertMovies_2025", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable movieTable = new DataTable();
            movieTable.Columns.Add("Url", typeof(string));
            movieTable.Columns.Add("PrimaryTitle", typeof(string));
            movieTable.Columns.Add("Description", typeof(string));
            movieTable.Columns.Add("PrimaryImage", typeof(string));
            movieTable.Columns.Add("Year", typeof(int));
            movieTable.Columns.Add("ReleaseDate", typeof(DateTime));
            movieTable.Columns.Add("Language", typeof(string));
            movieTable.Columns.Add("Budget", typeof(decimal));
            movieTable.Columns.Add("GrossWorldwide", typeof(decimal));
            movieTable.Columns.Add("Genres", typeof(string));
            movieTable.Columns.Add("IsAdult", typeof(bool));
            movieTable.Columns.Add("RuntimeMinutes", typeof(int));
            movieTable.Columns.Add("AverageRating", typeof(decimal));
            movieTable.Columns.Add("NumVotes", typeof(int));

            foreach (Movie movie in movies)
            {
                movieTable.Rows.Add(
                    movie.Url,
                    movie.PrimaryTitle,
                    movie.Description,
                    movie.PrimaryImage,
                    movie.Year,
                    movie.ReleaseDate,
                    movie.Language,
                    movie.Budget,
                    movie.GrossWorldwide,  // Use 0 if not provided
                    movie.Genres,
                    movie.IsAdult,
                    movie.RuntimeMinutes,
                    movie.AverageRating,    // Use 0 if not provided
                    movie.NumVotes         // Use 0 if not provided
                );
            }

            cmd.Parameters.Add(new SqlParameter("@Movies", SqlDbType.Structured)
            {
                TypeName = "dbo.MovieListType",
                Value = movieTable
            });

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
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a Movie into the Movies table
    //--------------------------------------------------------------------------------------------------
    #endregion

    #region Inserts
    public int RegisterUser(User user)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", user.Name);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        paramDic.Add("@active", user.Active);
        string spName = GetStoredProcedureName("SP_InsertUser_2025");
        return ExecuteSQLCommand(paramDic, spName);
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
        string spName = GetStoredProcedureName("SP_InsertMovie_2025");
        return ExecuteSQLCommand(paramDic, spName);
    }
    #endregion

    #region Updates
    public int UpdateUser(int id, User user)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        paramDic.Add("@name", user.Name);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        paramDic.Add("@active", user.Active);
        string spName = GetStoredProcedureName("SP_UpdateUser_2025");
        return ExecuteSQLCommand(paramDic, spName);
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
        string spName = GetStoredProcedureName("SP_UpdateMovie_2025");
        return ExecuteSQLCommand(paramDic, spName);
    }

    public int LoginUser(string email)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@email", email);
        string spName = GetStoredProcedureName("SP_LoginUser_2025");
        return ExecuteSQLCommand(paramDic, spName);
    }
    #endregion

    #region Deletes
    public int DeleteUser(int id)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        string spName = GetStoredProcedureName("SP_DeleteUser_2025");
        return ExecuteSQLCommand(paramDic, spName);
    }

    public int DeleteMovie(int id)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);
        string spName = GetStoredProcedureName("SP_DeleteMovie_2025");
        return ExecuteSQLCommand(paramDic, spName);
    }
    #endregion

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

    //---------------------------------------------------------------------------------
    // This method get the Users
    //---------------------------------------------------------------------------------
    public List<User> Read()
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


        cmd = CreateCommandWithStoredProcedureGeneral("SP_GetUser_2025", con, null);       // create the command

        List<User> users = new List<User>();

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        try
        {
            while (dataReader.Read()) {
                User u = new User();
                u.Id = Convert.ToInt32(dataReader["Id"]);
                u.Name = dataReader["Name"].ToString();
                u.Email = dataReader["Email"].ToString();
                u.Active = Convert.ToBoolean(dataReader["Active"]);
                users.Add(u);
            }
            return users;
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

}
