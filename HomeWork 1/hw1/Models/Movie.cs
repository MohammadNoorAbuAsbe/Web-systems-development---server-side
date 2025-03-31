using Microsoft.AspNetCore.Mvc;

namespace hw1.Models
{
    public class Movie
    {

        int Id;
        string url;
        string primaryTitle;
        string description;
        string primaryImage;
        int Year;
        DateTime releaseDate;
        string language;
        double Budget;
        double grossWorldwide;
        string genres;
        bool isAdult;
        int runtimeMinutes;
        float averageRating;
        int numVotes;

        static List<Movie> MoviesList = new List<Movie>();

        public Movie() { }
        public Movie(int id1, string url, string primaryTitle, string description, string primaryImage, int year1, DateTime releaseDate, string language, double budget1, double grossWorldwide, string genres, bool isAdult, int runtimeMinutes, float averageRating, int numVotes)
        {
            Id1 = id1;
            Url = url;
            PrimaryTitle = primaryTitle;
            Description = description;
            PrimaryImage = primaryImage;
            Year1 = year1;
            ReleaseDate = releaseDate;
            Language = language;
            Budget1 = budget1;
            GrossWorldwide = grossWorldwide;
            Genres = genres;
            IsAdult = isAdult;
            RuntimeMinutes = runtimeMinutes;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }

        public int Id1 { get => Id; set => Id = value; }
        public string Url { get => url; set => url = value; }
        public string PrimaryTitle { get => primaryTitle; set => primaryTitle = value; }
        public string Description { get => description; set => description = value; }
        public string PrimaryImage { get => primaryImage; set => primaryImage = value; }
        public int Year1 { get => Year; set => Year = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string Language { get => language; set => language = value; }
        public double Budget1 { get => Budget; set => Budget = value; }
        public double GrossWorldwide { get => grossWorldwide; set => grossWorldwide = value; }
        public string Genres { get => genres; set => genres = value; }
        public bool IsAdult { get => isAdult; set => isAdult = value; }
        public int RuntimeMinutes { get => runtimeMinutes; set => runtimeMinutes = value; }
        public float AverageRating { get => averageRating; set => averageRating = value; }
        public int NumVotes { get => numVotes; set => numVotes = value; }

        public static bool Insert(Movie newMovie)
        {
            foreach(Movie movie in MoviesList)
            {
                if(movie.Id == newMovie.Id || movie.primaryTitle == newMovie.primaryTitle)
                {
                    return false;
                }
            }
            MoviesList.Add(newMovie);
            return true;
        }

        public static List<Movie> Read()
        {
            return MoviesList;
        }

       
        public static List<Movie> GetByTitle(string title)
        {
            List<Movie> listByTitle = new List<Movie>();
            foreach (var movie in MoviesList)
            {
                if (movie.primaryTitle.Contains(title))
                    listByTitle.Add(movie);
            }
            return listByTitle;
        }

        public static List<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            List<Movie> listByReleaseDate = new List<Movie>();

      
            foreach (var movie in MoviesList)
            {
                if (movie.ReleaseDate >= startDate && movie.ReleaseDate <= endDate)
                {
                    listByReleaseDate.Add(movie);
                }
            }

            return listByReleaseDate;
        }
    }
}
