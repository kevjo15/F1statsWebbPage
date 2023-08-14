namespace Skolinlämning.Models
{
    public class RaceInfo
    {
        public class Race
        {
            public string RaceName { get; set; }
            public Circuit Circuit { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }

        public class Circuit
        {
            public string CircuitId { get; set; }
            public string CircuitName { get; set; }
            public Location Location { get; set; }
        }

        public class Location
        {
            public string Locality { get; set; }
            public string Country { get; set; }
        }
    }
}
