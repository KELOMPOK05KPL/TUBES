using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tubes_KPL.Pengembalian;
using System.Collections.Generic;

[TestClass]
public class PengembalianKendaraanTest
{
    private readonly string _baseUrl = "https://localhost:44376";
    private readonly string _riwayatFilePath = "Data/RiwayatPeminjaman.json";

    [TestMethod]
    public async Task CariKendaraanDipinjamAsync_ShouldReturnVehicle_WhenExists()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(handlerMock.Object);
        var service = new PengembalianKendaraan(httpClient, _baseUrl, _riwayatFilePath);

        // Simulasi data JSON
        var jsonData = "[{\"VehicleId\": 1, \"Brand\": \"Toyota\", \"Type\": \"Mobil\", \"Peminjam\": \"tri\", \"Status\": \"Dipinjam\"}]";
        await File.WriteAllTextAsync(_riwayatFilePath, jsonData);

        // Act
        var result = await service.CariKendaraanDipinjamAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Toyota", result.Brand);
        Assert.AreEqual("Dipinjam", result.Status);
    }

    [TestMethod]
    public async Task KembalikanKendaraan_ShouldUpdateStatus_WhenSuccessful()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri.ToString().Contains("/api/vehicles/1/return")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Pengembalian berhasil")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new PengembalianKendaraan(httpClient, _baseUrl, _riwayatFilePath);

        var jsonData = "[{\"VehicleId\": 1, \"Brand\": \"Toyota\", \"Type\": \"Mobil\", \"Peminjam\": \"tri\", \"Status\": \"Dipinjam\"}]";
        await File.WriteAllTextAsync(_riwayatFilePath, jsonData);

        // Act
        var success = await service.ProsesPengembalianAsync(1);
        var updatedRiwayat = await service.GetRiwayatPeminjaman();
        var updatedVehicle = updatedRiwayat.Find(r => r.VehicleId == 1);

        // Assert
        Assert.IsTrue(success);
        Assert.IsNotNull(updatedVehicle);
        Assert.AreEqual("Dikembalikan", updatedVehicle.Status);
    }
}
