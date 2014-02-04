using System;

namespace BusTrackerDomain.Entity
{
    public class Departure
    {
        public int RouteId { get; set; }
        public int Direction { get; set; }

        public DateTime DepartureTime { get; set; }
        public TimeSpan Delay { get; set; }

        public string Note { get; set; }
    }
}