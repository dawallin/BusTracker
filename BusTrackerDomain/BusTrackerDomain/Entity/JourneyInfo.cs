using System;
using System.Collections.Generic;

namespace BusTrackerDomain.Entity
{
    public class JourneyInfo
    {
        public Journey Journey { get; set; }

        public Station NextStation { get; set; }
    }
}