using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusTrackerDomain;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;
using BusTrackerWeb.SkanetrafikenServiceAgent;

namespace BusTrackerWeb
{
    public class StationController : ApiController
    {
        static readonly IServiceAgent agent = new ServiceAgent(new WebCallProxy());
        static readonly BusTracker busTracker = new BusTracker(agent);

        //// GET api/<controller>
        //public IEnumerable<StationInfo> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public StationInfo Get(int id)
        {
            var stationInfo = busTracker.GetStationInfo(id);
            return stationInfo;
        }
    }
}