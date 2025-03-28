namespace Flights.BL
{
    public class Flight
    {
        string from;
        string to;
        string airline;
        double price;

        static List<Flight> flightList = new List<Flight>();

        public Flight(string from, string to, string airline, double price)
        {
            From = from;
            To = to;
            Airline = airline;
            Price = price;
        }

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Airline { get => airline; set => airline = value; }
        public double Price { get => price; set => price = value; }

        public Flight()
        {
        }

        public bool Insert() {
            bool success = false;
            flightList.Add(this);
            if (flightList.Contains(this))
            {
                success = true;
            }
            return success;
        }

        public static List<Flight> Read()
        {
            return flightList;
        }

        public static List<Flight> ReadFiltered(string from, string to)
        {
            List<Flight> filteredList = new List<Flight>();
            foreach (Flight flight in flightList)
            {
                if (flight.From == from && flight.To == to)
                {
                    filteredList.Add(flight);
                }
            }
            return flightList;
        }


    }
}
