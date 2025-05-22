namespace Movies_System.Models
{
    public class RentedMovie
    {
        private Movie movie;
        private DateTime rentStart;
        private DateTime rentEnd;
        private int totalPrice;
        private DateTime? deletedAt;
        private User user;

        public RentedMovie(Movie movie, DateTime rentStart, DateTime rentEnd, int totalPrice, DateTime? deletedAt, User user)
        {
            User = user;
            Movie = movie;
            RentStart = rentStart;
            RentEnd = rentEnd;
            TotalPrice = totalPrice;
            DeletedAt = deletedAt;
        }

        public RentedMovie() { }

        public User User { get => user; set => user = value; }
        public Movie Movie { get => movie; set => movie = value; }
        public DateTime RentStart { get => rentStart; set => rentStart = value; }
        public DateTime RentEnd { get => rentEnd; set => rentEnd = value; }
        public int TotalPrice { get => totalPrice; set => totalPrice = value; }
        public DateTime? DeletedAt { get => deletedAt; set => deletedAt = value; }

        public static List<RentedMovie> GetUserRentedMovies(int userId)
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetUserRentedMovies(userId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        public static bool PassMovie(int movieId, int currentUserId, int newUserId)
        {
            DBservices dBservices = new DBservices();
            try
            {
                if (currentUserId == newUserId)
                {
                    return false;
                }
                if (dBservices.PassMovie(movieId, currentUserId, newUserId) == 1) 
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
