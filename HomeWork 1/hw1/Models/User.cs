namespace hw1.Models
{
    public class User
    {
        int Id;
        string Name;
        string Email;
        string Password;
        bool Active = true;
        static List<User> UsersList = new List<User>();

        public User(int id1, string name1, string email1, string password1, bool active1)
        {
            Id1 = id1;
            Name1 = name1;
            Email1 = email1;
            Password1 = password1;
            Active1 = active1;
        }

        public User() { }
        public int Id1 { get => Id; set => Id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public string Email1 { get => Email; set => Email = value; }
        public string Password1 { get => Password; set => Password = value; }
        public bool Active1 { get => Active; set => Active = value; }

        public static List<User> Read()
        {
            return UsersList;
        }

        public static bool Insert(User user)
        {
            foreach (var existingUser in UsersList)
            {
                if (existingUser.Id1 == user.Id1)
                {
                    return false; 
                }
            }
            UsersList.Add(user);
            return true;
        }
    }
}
