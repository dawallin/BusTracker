using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusTrackerDomain.Interface;
using BusTrackerDomain;
using BusTrackerWeb.SkanetrafikenServiceAgent;

namespace BusTrackerWeb.Controllers
{
    public class BusTrackerController : Controller
    {
        static readonly IServiceAgent agent = new ServiceAgent(new WebCallProxy());
        static readonly BusTracker busTracker = new BusTracker(agent);

        //
        // GET: /Bustracker/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Bustracker/StationInfo/Id
        public ActionResult StationInfo(int id)
        {
            var stationInfo = busTracker.GetStationInfo(id);
            return View(stationInfo);
        }

        //
        // GET: /Bustracker/ServiceInfo/Id
        public ActionResult ServiceInfo(int id)
        {
            var serviceInfo = busTracker.GetServiceInfo(id);
            return View(serviceInfo);
        }

        //
        // GET: /Bustracker/ServiceInfo2/Id
        public ActionResult ServiceInfo2(int id)
        {
            var serviceInfo = busTracker.GetServiceInfo(id);
            return View(serviceInfo);
        }
    }
}
