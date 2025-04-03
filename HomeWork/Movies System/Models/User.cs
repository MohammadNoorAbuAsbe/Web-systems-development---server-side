namespace Movies_System.Models
{
    public class User
    {
        #region Fields
        static List<User> usersList = new List<User>();

        int id;
        string name;
        string email;
        string password;
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

        #region POST Methods
        public static bool Insert(User user)
        {
            bool result = false;
            if (!usersList.Any(u => u.Id == user.Id))
            {
                usersList.Add(user);
                result = true;
            }
            return result;
        }
        #endregion

        #region GET Methods
        public static List<User> Read()
        {
            return usersList;
        }
        #endregion
    }
}
