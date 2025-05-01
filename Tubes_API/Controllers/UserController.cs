using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_API_tubes.Models;
using Test_API_tubes.Repositories;

namespace Test_API_tubes.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers() => Ok(DataStore.Users);

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = DataStore.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        DataStore.Users.Remove(user);
        return Ok();
    }
}