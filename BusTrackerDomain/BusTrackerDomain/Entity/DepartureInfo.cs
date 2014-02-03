using System;

namespace BusTrackerDomain.Entity
{
    public class DepartureInfo
    {
        public Service Service { get; set; }

        public Station Station { get; set; }

        public int RouteId { get; set; }
        public int Direction { get; set; }

        public DateTime DepartureTime { get; set; }
        public TimeSpan Delay { get; set; }

        public string Note { get; set; }
    }
}