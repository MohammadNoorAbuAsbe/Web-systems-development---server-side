namespace Movies_System.Models
{
    public class User
    {
        #region Fields  
        static List<User> usersList = new List<User>();

        int id;
        string name = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        bool active = true;
        #endregion

        #region Constructors  
        public User(int id, string name, string email, string password, bool active)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Active = active;
        }

        public User()
        {
        }
        #endregion

        #region Properties  
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }
        #endregion

        #region GET Methods
        public static List<User> Read()
        {
            return usersList;
        }
        #endregion

        #region Authentication Methods
        public static bool Register(User user)
        {
            if (usersList.Any(u => u.Email == user.Email))
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 15);

            usersList.Add(user);
            return true;
        }

        public static bool Login(string email, string password)
        {
            var user = usersList.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
        #endregion
    }
}
