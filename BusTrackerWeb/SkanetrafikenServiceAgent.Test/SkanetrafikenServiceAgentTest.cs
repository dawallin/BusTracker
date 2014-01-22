using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using BusTrackerDomain;
using BusTrackerWeb.SkanetrafikenServiceAgent;
using BusTrackerWeb.SkanetrafikenServiceAgent.Entity;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace SkanetrafikenServiceAgent.Test
{
    [TestClass]
    public class SkanetrafikenServiceAgentTest
    {
        private string SerializeObject<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        private string CreateGetDepartureArrivalResponse(GetDepartureArrivalResult.Line line)
        {
            return SerializeObject<GetDepartureArrivalResult>(new GetDepartureArrivalResult()
                {
                    Lines = new List<GetDepartureArrivalResult.Line>()
                        {
                            line
                        }
                });
        }

        [TestMethod]
        public void ParseProxyResult_GivenAServiceIdInProxyAnswer_ServiceIdIsParsed()
        {
            const int exampleStation = 0;
            const int exampleNo = 1;

            var exampleResult = CreateGetDepartureArrivalResponse(new GetDepartureArrivalResult.Line()
                {
                    No = exampleNo,
                });

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(exampleNo, serviceAgent.GetStationInfo(exampleStation).FirstOrDefault().ServiceId);
        }

        [TestMethod]
        public void ParseProxyResult_GivenAJourneyTimeWithoutDeviationInProxyAnswer_JourneyTimeIsParsed()
        {
            const int exampleStation = 0;
            DateTime exampleDateTime = DateTime.Parse("2001-01-01");

            var exampleResult = CreateGetDepartureArrivalResponse(new GetDepartureArrivalResult.Line()
            {
                JourneyDateTime = exampleDateTime,
            });

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(exampleDateTime, serviceAgent.GetStationInfo(exampleStation).FirstOrDefault().DepartureTime);
        }

        [TestMethod]
        public void ParseProxyResult_GivenADeviationInProxyAnswer_DeviationIsParsed()
        {
            const int exampleStation = 0;
            const int exampleDeviation = 1;

            var exampleResult = CreateGetDepartureArrivalResponse(new GetDepartureArrivalResult.Line()
            {
                RealTime = new GetDepartureArrivalResult.RealTime()
                {
                    RealTimeInfo = new GetDepartureArrivalResult.RealTimeInfo()
                    {
                        DepTimeDeviation = exampleDeviation
                    }
                }
            });

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(TimeSpan.FromMinutes(exampleDeviation), serviceAgent.GetStationInfo(exampleStation).FirstOrDefault().Delay);
        }
    }
}
