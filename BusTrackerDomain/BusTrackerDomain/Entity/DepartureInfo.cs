using System;

namespace BusTrackerDomain.Entity
{
    public class DepartureInfo
    {
        public int ServiceId;
        public int StationId;
        public string StationName;

        public int RouteId;
        public int Direction;

        public DateTime DepartureTime;
        public TimeSpan Delay;

        public string Note;
    }
}