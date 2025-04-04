namespace MoviesSystem.Models
{
    public class Movie
    {
        #region Fields
        static List<Movie> moviesList = new List<Movie>();

        string id;
        string url;
        string primaryTitle;
        string description;
        string primaryImage;
        int year;
        DateTime releaseDate;
        string language;
        double budget;
        double grossWorldwide;
        string genres;
        bool isAdult;
        int runtimeMinutes;
        float averageRating;
        int numVotes;

        #endregion

        #region Constructors
        public Movie(string id, string url, string primaryTitle, string description, string primaryImage, int year, DateTime releaseDate, string language, double budget, double grossWorldwide, string genres, bool isAdult, int runtimeMinutes, float averageRating, int numVotes)
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
        public string Id { get => id; set => id = value; }
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
        #endregion

        #region POST Methods
        public static bool Insert(Movie movie)
        {
            bool result = false;
            if (!moviesList.Any(m => m.Id == movie.Id || m.PrimaryTitle == movie.PrimaryTitle))
            {
                moviesList.Add(movie);
                result = true;
            }
            return result;
        }
        #endregion

        #region GET Methods 
        public static List<Movie> Read()
        {
            return moviesList;
        }

        public static List<Movie> GetByTitle(string title)
        {
            return moviesList.Where(m => m.PrimaryTitle.Contains(title)).ToList();
        }

        public static List<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            return moviesList.Where(m => m.ReleaseDate >= startDate && m.ReleaseDate <= endDate).ToList();
        }
        #endregion

        #region DELETE Methods
        public static bool DeleteById(string id)
        {
            var movieToRemove = moviesList.Where(m => m.id == id).FirstOrDefault();
            if (movieToRemove != null)
            {
                moviesList.Remove(movieToRemove);
                return true;
            }
            return false;
        }
        #endregion

    }
}
