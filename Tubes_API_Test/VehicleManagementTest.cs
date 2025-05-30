using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tubes_KPL.Services;
using Test_API_tubes.Models;

[TestClass]
public class VehicleManagementTest
{
    private Mock<HttpMessageHandler> _handlerMock;
    private HttpClient _httpClient;
    private VehicleManagementService _service;

    [TestInitialize]
    public void Setup()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_handlerMock.Object);
        _service = new VehicleManagementService(_httpClient, "https://example.com");
    }

    [TestMethod]
    public async Task TestGetAllAsync_Should_ReturnListOfVehicles()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle(1, "Mobil", "Toyota", "Avanza", VehicleState.Available),
            new Vehicle(2, "Motor", "Honda", "Vario", VehicleState.Rented)
        };

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(vehicles))
            });

        var result = await _service.TestGetAllAsync();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Toyota", result[0].Brand);
        Assert.AreEqual("Honda", result[1].Brand);
    }



    [TestMethod]
    public async Task TestCreateAsync_Should_ReturnTrueOnSuccess()
    {
        var newVehicle = new Vehicle(0, "Mobil", "Toyota", "Avanza", VehicleState.Available);

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created
            });

        var result = await _service.TestCreateAsync(newVehicle);

        Assert.IsTrue(result);
    }
}