using System;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;
using BusTrackerDomain;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusTrackerDomain.Test
{
    [TestClass]
    public class BusTrackerTest
    {
        [TestMethod]
        public void GetStationInfo_ForValidStationId_ReturnsStationTimeTable()
        {
            int exampleStationId = 1;
            var exampleDeparture = new DepartureInfo();

            var serviceAgent = Substitute.For<IServiceAgent>();

            serviceAgent.GetStationInfo(exampleStationId).Returns(new List<DepartureInfo> { exampleDeparture });

            BusTracker busTracker = new BusTracker(serviceAgent);

            Assert.AreSame(exampleDeparture, busTracker.GetStationInfo(exampleStationId).FirstOrDefault());
        }
    }
}
