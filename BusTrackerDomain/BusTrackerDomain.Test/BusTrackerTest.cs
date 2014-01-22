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
            const int exampleStationId = 1;
            var exampleStationTimeTable = new List<DepartureInfo> { };

            var serviceAgent = Substitute.For<IServiceAgent>();

            serviceAgent.GetStationInfo(exampleStationId).Returns(exampleStationTimeTable);

            var busTracker = new BusTracker(serviceAgent);

            Assert.AreSame(exampleStationTimeTable, busTracker.GetStationInfo(exampleStationId));
        }
    }
}
