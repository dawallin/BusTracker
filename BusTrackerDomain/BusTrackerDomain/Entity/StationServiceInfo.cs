using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTrackerDomain.Entity
{
    public class StationServiceInfo
    {
        public Service Service { get; set; }

        public Departure Departure { get; set; } 
    }
}
