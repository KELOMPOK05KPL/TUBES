using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

[TestClass]
public class LoginRegisterTests
{
    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _sendAsync;

        public MockHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync)
        {
            _sendAsync = sendAsync;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sendAsync(request);
        }
    }

    [TestMethod]
    public async Task TriggerLoginAsync_ShouldReturnTrue_WhenLoginSuccessful()
    {
        // Arrange
        var handler = new MockHttpMessageHandler(async (request) =>
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        });

        var httpClient = new HttpClient(handler);
        var loginSystem = new LoginRegister(httpClient);

        // Act
        var result = await loginSystem.TriggerLoginAsync("testuser", "password");

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(LoginRegister.AuthState.Authenticated, loginSystem.GetState());
    }

    [TestMethod]
    public async Task TriggerLoginAsync_ShouldReturnFalse_WhenLoginFails()
    {
        // Arrange
        var handler = new MockHttpMessageHandler(async (request) =>
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("Unauthorized")
            };
        });

        var httpClient = new HttpClient(handler);
        var loginSystem = new LoginRegister(httpClient);

        // Act
        var result = await loginSystem.TriggerLoginAsync("wronguser", "wrongpass");

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(LoginRegister.AuthState.Failed, loginSystem.GetState());
    }

    [TestMethod]
    public void Logout_ShouldSetStateToIdle_WhenAuthenticated()
    {
        // Arrange
        var loginSystem = new LoginRegister();
        typeof(LoginRegister).GetField("_currentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                             .SetValue(loginSystem, LoginRegister.AuthState.Authenticated);

        // Act
        loginSystem.Logout();

        // Assert
        Assert.AreEqual(LoginRegister.AuthState.Idle, loginSystem.GetState());
    }

    [TestMethod]
    public async Task ListUsersAsync_ShouldPrintUserList_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1", Role = "User" },
            new User { Id = 2, Username = "admin", Role = "Admin" }
        };

        var json = JsonSerializer.Serialize(users);
        var handler = new MockHttpMessageHandler((request) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        });

        var httpClient = new HttpClient(handler);
        var loginSystem = new LoginRegister(httpClient);

        // Act
        await loginSystem.ListUsersAsync();

        // No assertion, only output check. Could redirect console for assertions if needed.
    }

    [TestMethod]
    public async Task TriggerAsync_ShouldGoToRegisteringState_WhenRegisterCalled()
    {
        // Arrange
        var handler = new MockHttpMessageHandler((request) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });

        var httpClient = new HttpClient(handler);
        var loginSystem = new LoginRegister(httpClient);

        // Act
        await loginSystem.TriggerAsync("register", "newuser", "newpass");

        // Assert
        Assert.AreEqual(LoginRegister.AuthState.Idle, loginSystem.GetState()); // Karena RegisterAsync mengembalikan ke Idle saat sukses
    }
}
