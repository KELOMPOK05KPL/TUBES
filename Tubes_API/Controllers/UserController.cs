using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using Test_API_tubes.Models;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly string filePath = "D:\\My Code\\GUI C#\\TUBES\\Tubes_KPL\\Tubes_API\\Repositories\\user.json";

    private List<User> LoadUsers()
    {
        if (!System.IO.File.Exists(filePath))
            return new List<User>();

        var json = System.IO.File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    private void SaveUsers(List<User> users)
    {
        var json = JsonSerializer.Serialize(users);
        System.IO.File.WriteAllText(filePath, json);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = LoadUsers();
        return Ok(users);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var users = LoadUsers();
        var userToDelete = users.FirstOrDefault(u => u.Id == id);

        if (userToDelete == null)
            return NotFound("User tidak ditemukan.");

        users.Remove(userToDelete);
        SaveUsers(users);
        return Ok("User berhasil dihapus.");
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] User newUser)
    {
        var users = LoadUsers();
        newUser.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(newUser);
        SaveUsers(users);
        return CreatedAtAction(nameof(GetAllUsers), new { id = newUser.Id }, newUser);
    }
}

