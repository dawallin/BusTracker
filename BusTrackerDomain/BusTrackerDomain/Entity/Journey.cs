using System;

namespace BusTrackerDomain.Entity
{
    public class Journey
    {
        public string Key { get; set; }

        public Service Service { get; set; }

        public int RouteId { get; set; }
        public int Direction { get; set; }
    }
}