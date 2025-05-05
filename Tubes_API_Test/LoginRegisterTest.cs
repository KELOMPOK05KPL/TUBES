using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

[TestClass]
public class LoginRegisterTests
{
    [TestMethod]
    public async Task Login_Success_Should_SetStateToAuthenticated()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Contains("/login")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Login success")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var login = new Login_Register(httpClient);

        // Act
        var result = await login.TriggerLoginAsync("testuser", "testpass");

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(Login_Register.State.Authenticated, login.GetState());
    }

    [TestMethod]
    public async Task Login_Failed_Should_SetStateToFailed()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Contains("/login")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("Unauthorized")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var login = new Login_Register(httpClient);

        // Act
        var result = await login.TriggerLoginAsync("wronguser", "wrongpass");

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(Login_Register.State.Failed, login.GetState());
    }
}
