using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Test_API_tubes.Models;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly string filePath = "D:\\My Code\\GUI C#\\TUBES\\Tubes_KPL\\Tubes_API\\Repositories\\user.json";

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var users = LoadUsers();

        if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Username sudah ada");

        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        SaveUsers(users);
        return Ok(user);
    }

    [HttpGet("login")]
    public IActionResult Login(string username, string password)
    {
        var users = LoadUsers();
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null) return Unauthorized();
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = LoadUsers();
        return Ok(users);
    }

    private List<User> LoadUsers() =>
        System.IO.File.Exists(filePath)
            ? JsonSerializer.Deserialize<List<User>>(System.IO.File.ReadAllText(filePath)) ?? new List<User>()
            : new List<User>();

    private void SaveUsers(List<User> users) =>
        System.IO.File.WriteAllText(filePath, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
}