using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_API_tubes.Models;
using Test_API_tubes.Repositories;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        user.Id = DataStore.Users.Count + 1;
        DataStore.Users.Add(user);
        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User login)
    {
        var user = DataStore.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
        if (user == null) return Unauthorized();
        return Ok(user);
    }
}