namespace FootBallApp.BL
{
    public class Player
    {
        string name;
        string team;
        string position;
        double age;

        public Player(string name, string team, string position, double age)
        {
            Name = name;
            Team = team;
            Position = position;
            Age = age;
        }

        public string Name { get => name; set => name = value; }
        public string Team { get => team; set => team = value; }
        public string Position { get => position; set => position = value; }
        public double Age { get => age; set => age = value; }
    }
}
