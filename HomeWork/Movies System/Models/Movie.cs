namespace Movies_System.Models
{
    public class Movie
    {
        #region Fields
        static List<Movie> moviesList = new List<Movie>();

        int id;
        string url = string.Empty;
        string primaryTitle = string.Empty;
        string description = string.Empty;
        string primaryImage = string.Empty;
        int year;
        DateTime releaseDate;
        string language = string.Empty;          // Value N/A indicates not provided
        double budget;                           // Value -1 indicates not provided
        double grossWorldwide;                   // Value -1 indicates not provided
        string genres = string.Empty;
        bool isAdult;
        int runtimeMinutes;
        float averageRating;
        int numVotes;
        DateTime? deletedAt;

        #endregion

        #region Constructors
        public Movie(int id, string url, string primaryTitle, string description, string primaryImage, int year, DateTime releaseDate, string language, double budget, double grossWorldwide, string genres, bool isAdult, int runtimeMinutes, float averageRating, int numVotes)
        {
            Id = id;
            Url = url;
            PrimaryTitle = primaryTitle;
            Description = description;
            PrimaryImage = primaryImage;
            Year = year;
            ReleaseDate = releaseDate;
            Language = language;
            Budget = budget;
            GrossWorldwide = grossWorldwide;
            Genres = genres;
            IsAdult = isAdult;
            RuntimeMinutes = runtimeMinutes;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }
        public Movie()
        {
        }
        #endregion

        #region Properties
        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public string PrimaryTitle { get => primaryTitle; set => primaryTitle = value; }
        public string Description { get => description; set => description = value; }
        public string PrimaryImage { get => primaryImage; set => primaryImage = value; }
        public int Year { get => year; set => year = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string Language { get => language; set => language = value; }
        public double Budget { get => budget; set => budget = value; }
        public double GrossWorldwide { get => grossWorldwide; set => grossWorldwide = value; }
        public string Genres { get => genres; set => genres = value; }
        public bool IsAdult { get => isAdult; set => isAdult = value; }
        public int RuntimeMinutes { get => runtimeMinutes; set => runtimeMinutes = value; }
        public float AverageRating { get => averageRating; set => averageRating = value; }
        public int NumVotes { get => numVotes; set => numVotes = value; }
        public DateTime? DeletedAt { get => deletedAt; set => deletedAt = value; }
        #endregion

        #region POST Methods
        public static bool Insert(Movie movie)
        {
            try
            {
                DBservices dBservices = new DBservices();
                if (dBservices.AddMovie(movie) == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GET Methods 
        public static List<Movie> Read()
        {
            return moviesList;
        }

        public static List<Movie> GetByTitle(string title)
        {
            return moviesList.Where(m => m.PrimaryTitle.ToLower().Contains(title.ToLower())).ToList();
        }

        public static List<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            return moviesList.Where(m => m.ReleaseDate >= startDate && m.ReleaseDate <= endDate).ToList();
        }
        #endregion

        #region DELETE Methods
        public static bool DeleteMovieById(int id)
        {
            DBservices dBservices = new DBservices();
            if (dBservices.DeleteMovie(id) == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region UPDATE Methods
        public static bool UpdateMovie(int id,Movie movie)
        {
            DBservices dBservices = new DBservices();
            if (dBservices.UpdateMovie(id, movie) == 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}