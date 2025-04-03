namespace TennisProj.Models
{
    public class Player
    {
        string name;
        double age;
        double winRate;

        static List<Player> playerList= new List<Player>();

        public Player(string name, double age, double winRate)
        {
            Name = name;
            Age = age;
            WinRate = winRate;
        }

        public string Name { get => name; set => name = value; }
        public double Age { get => age; set => age = value; }
        public double WinRate { get => winRate; set => winRate = value; }


        Player() { }

        public int Insert() {

            playerList.Add(this);
            return 1;
        
        }

        static public List<Player> Read()
        {
            return playerList;
        }

    }



}
