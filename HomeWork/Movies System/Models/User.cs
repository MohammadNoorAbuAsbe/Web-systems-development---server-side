using System.Runtime.Intrinsics.Arm;

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
        DateTime? deletedAt;
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
        public DateTime? DeletedAt { get => deletedAt; set => deletedAt = value; }
        #endregion

        #region GET Methods
        public static List<User> Read()
        {
           DBservices dbs = new DBservices();
            return dbs.Read();
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
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 15);
                DBservices dBservices = new DBservices();
                try 
                {
                    if (dBservices.RegisterUser(user) == 1)
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

        public static UserResponse? Login(string email, string password)
        {

            password = BCrypt.Net.BCrypt.HashPassword(password, 15);
            DBservices dBservices = new DBservices();
            try
            {
                if (dBservices.LoginUser(email) == 1)
                {
                    return new UserResponse();
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

            //var user = usersList.FirstOrDefault(u => u.Email == email);
            //if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            //{
            //    return null;
            //}

            //// Return a response object excluding sensitive information
            //return new UserResponse
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    Email = user.Email,
            //    Active = user.Active
            //};
        }
        #endregion

        #region DELETE Methods
        public static bool DeleteUserById(int id)
        {
            DBservices dBservices = new DBservices();
            if (dBservices.DeleteUser(id) == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Update Methods
        public static bool UpdateUser(int id, User user)
        {
            try
            {
                DBservices dBservices = new DBservices();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 15);
                if (dBservices.UpdateUser(id, user) == 0)
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

    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
