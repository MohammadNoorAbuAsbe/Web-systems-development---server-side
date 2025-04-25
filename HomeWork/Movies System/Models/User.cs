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

        public static User? GetByEmail(string email)
        {
            return usersList.FirstOrDefault(u => u.Email == email);
        }
        #endregion

        #region Authentication Methods
        // The registerLock ensures thread safety when multiple threads attempt to register users simultaneously.
        // Without this lock, concurrent access to the usersList could result in concurrency Issue, 
        // such as duplicate registrations or inconsistent state of the list.
        private static readonly object registerLock = new object();
        public static bool Register(User user)
        {
            lock (registerLock)
            {
                if (usersList.Any(u => u.Email == user.Email))
                {
                    return false;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 15);
                user.Id = usersList.Count > 0 ? usersList.Max(u => u.Id) + 1 : 1;
                usersList.Add(user);
                return true;
            }
        }

        public static object? Login(string email, string password)
        {
            var user = usersList.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            // Return a response object excluding sensitive information
            return new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Active
            };
        }
        #endregion
    }
}
