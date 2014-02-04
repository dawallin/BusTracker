using System;
using System.Collections.Generic;

namespace BusTrackerDomain.Entity
{
    public class StationInfo
    {
        public Station Station { get; set; }

        public List<StationServiceInfo> StationServiceInfo { get; set; }
    }
}