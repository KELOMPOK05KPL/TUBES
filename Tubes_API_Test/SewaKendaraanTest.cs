using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using config;
using controller;
using Test_API_tubes.Models;
using System.Text.Json;
using System.Text;

[TestClass]
public class PinjamKendaraanTestsSimplified
{

    [TestMethod]
    public async Task PinjamKendaraan_Success_ReturnsTrue()
    {
        // Arrange
        int vehicleId = 1;
        string namaPeminjam = "Alice";
        var _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        var _kendaraanTersedia = new List<VehicleDto>();
        var _mockConfig = new Mock<RuntimeConfig>();
        string _baseUrl = "http://localhost:5000";

        // Mock POST (rent) dan GET (vehicle details)
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                if (request.Method == HttpMethod.Post && request.RequestUri.ToString() == $"{_baseUrl}/api/vehicles/{vehicleId}/rent")
                {
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("") };
                }
                else if (request.Method == HttpMethod.Get && request.RequestUri.ToString() == $"{_baseUrl}/api/vehicles/{vehicleId}")
                {
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(new VehicleDto { Id = vehicleId, State = 1 }), Encoding.UTF8, "application/json") };
                }
                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound }; // Default
            });

        var sistemSewa = new Sistemsewa<VehicleDto>(_kendaraanTersedia, _mockConfig.Object, _httpClient, _baseUrl);

        // Act
        var result = await sistemSewa.PinjamKendaraan(vehicleId, namaPeminjam);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task PinjamKendaraan_Failure_ReturnsFalse()
    {
        // Arrange
        int vehicleId = 2;
        string namaPeminjam = "Bob";
        var _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        var _kendaraanTersedia = new List<VehicleDto>();
        var _mockConfig = new Mock<RuntimeConfig>();
        string _baseUrl = "http://localhost:5000";

        // Mock semua HTTP requests untuk gagal
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent("Generic error") });

        var sistemSewa = new Sistemsewa<VehicleDto>(_kendaraanTersedia, _mockConfig.Object, _httpClient, _baseUrl);

        // Act
        var result = await sistemSewa.PinjamKendaraan(vehicleId, namaPeminjam);

        // Assert
        Assert.IsFalse(result);
    }
}