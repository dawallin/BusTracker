using BusTrackerWeb.SkanetrafikenServiceAgent;
using BusTrackerWeb.SkanetrafikenServiceAgent.Entity;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SkanetrafikenServiceAgent.Test
{
    [TestFixture]
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

        private string CreateGetDepartureArrivalResponse(GetDepartureArrivalResult.Line line, string stationName)
        {
            return SerializeObject<GetDepartureArrivalResult>(new GetDepartureArrivalResult()
                {
                    Lines = new List<GetDepartureArrivalResult.Line>()
                        {
                            line
                        },
                    StopAreaData = new GetDepartureArrivalResult.StopAreaDataObject()
                        {
                            Name = stationName
                        }
                });
        }

        [Test]
        public void ParseProxyResult_GivenAServiceIdInProxyAnswer_ServiceIdIsParsed()
        {
            const string exampleStationName = "TestStation";
            const int exampleStationId = 0;
            const int exampleNo = 1;

            var exampleResult = CreateGetDepartureArrivalResponse(new GetDepartureArrivalResult.Line()
                {
                    No = exampleNo,
                }, exampleStationName);

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(exampleNo, serviceAgent.GetStationInfo(exampleStationId).StationServiceInfo.FirstOrDefault().Service.ServiceId);
        }

        [Test]
        public void ParseProxyResult_GivenAJourneyTimeWithoutDeviationInProxyAnswer_JourneyTimeIsParsed()
        {
            const string exampleStationName = "TestStation";
            const int exampleStationId = 0;
            DateTime exampleDateTime = DateTime.Parse("2001-01-01");

            var exampleResult = CreateGetDepartureArrivalResponse(new GetDepartureArrivalResult.Line()
            {
                JourneyDateTime = exampleDateTime,
            }, exampleStationName);

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(exampleDateTime, serviceAgent.GetStationInfo(exampleStationId).StationServiceInfo.FirstOrDefault().Departure.DepartureTime);
        }

        [Test]
        public void ParseProxyResult_GivenADeviationInProxyAnswer_DeviationIsParsed()
        {
            const string exampleStationName = "TestStation";
            const int exampleStationId = 0;
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
                }, exampleStationName);

            var serviceProxy = Substitute.For<IServiceProxy>();

            serviceProxy.Call(Arg.Any<string>()).Returns(exampleResult);

            var serviceAgent = new ServiceAgent(serviceProxy);

            Assert.AreEqual(TimeSpan.FromMinutes(exampleDeviation), serviceAgent.GetStationInfo(exampleStationId).StationServiceInfo.FirstOrDefault().Departure.Delay);
        }
    }
}
