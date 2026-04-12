using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Test_API_tubes.Models;
using Tubes_KPL.LihatKendaraan;

namespace Tubes_API_Test
{
    [TestClass]
    public class LihatKendaraanTest
    {
        // Create a mockable HttpMessageHandler
        public class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseFactory;

            public MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
            {
                _responseFactory = responseFactory;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_responseFactory(request));
            }
        }

        [TestMethod]
        public async Task GetKendaraanDariApi()
        {
            var expectedVehicles = new List<Vehicle>
            {
                new Vehicle(1, "Mobil", "Toyota", "Avanza", VehicleState.Rented),
                new Vehicle(2, "Motor", "Honda", "250RR", VehicleState.Rented)
            };

            var mockHandler = new MockHttpMessageHandler(request =>
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedVehicles)
                };
            });

            var httpClient = new HttpClient(mockHandler);
            var kendaraanViewer = new KendaraanViewer(httpClient, "http://localhost:5176/api/vehicles");

            var result = await kendaraanViewer.GetKendaraanDariApi();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedVehicles.Count, result.Count);
            Assert.AreEqual(expectedVehicles[0].Brand, result[0].Brand);
            Assert.AreEqual(expectedVehicles[1].Model, result[1].Model);
        }

    }
}