using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Test_API_tubes.Models;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly string filePath;

    public UserController()
    {
        // Get the correct path relative to the application root
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        filePath = Path.Combine(basePath, "Data", "user.json");
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            var users = LoadUsers();

            if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
                return BadRequest("Username sudah ada");

            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            SaveUsers(users);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpGet("login")]
    public IActionResult Login(string username, string password)
    {
        try
        {
            Console.WriteLine($"[API] Login attempt: username={username}, password={password}");
            var users = LoadUsers();
            Console.WriteLine($"[API] Total users in database: {users.Count}");
            
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                Console.WriteLine($"[API] User not found for username: {username}");
                return Unauthorized("User tidak ditemukan");
            }
            
            Console.WriteLine($"[API] Login successful for: {username}");
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[API] Error during login: {ex.Message}");
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = LoadUsers();
        return Ok(users);
    }

    private List<User> LoadUsers()
    {
        try
        {
            Console.WriteLine($"[API] Loading users from: {filePath}");
            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<List<User>>(content) ?? new List<User>();
                Console.WriteLine($"[API] Loaded {users.Count} users");
                return users;
            }
            else
            {
                Console.WriteLine($"[API] File not found: {filePath}");
                return new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[API] Error loading users: {ex.Message}");
            return new List<User>();
        }
    }

    private void SaveUsers(List<User> users) =>
        System.IO.File.WriteAllText(filePath, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
}