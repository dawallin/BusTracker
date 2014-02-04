using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTrackerDomain.Entity
{
    public class ServiceStationInfo
    {
        public Station Station { get; set; }

        public Departure NextTo { get; set; }
        public Departure NextReturn { get; set; }
    }
}
