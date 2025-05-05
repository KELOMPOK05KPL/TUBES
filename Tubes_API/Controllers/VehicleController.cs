using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using Test_API_tubes.Models;
using Tubes_API;
using Tubes_API.Services;
using Tubes_API.Models;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _service;
    public VehicleController(VehicleService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vehicle>> GetAll() => _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Vehicle> GetById(int id)
    {
        var vehicle = _service.GetById(id);
        return vehicle == null ? NotFound() : Ok(vehicle);
    }

    [HttpPost]
    public IActionResult Create(Vehicle vehicle)
    {
        _service.Add(vehicle);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    

    public class PeminjamanRequest
    {
        public string NamaPeminjam { get; set; }
    }

    // Di Tubes_API/Controllers/VehicleController.cs
    

    [HttpPut("{id}")]
    public IActionResult Update(int id, Vehicle updated)
    {
        if (id != updated.Id) return BadRequest();
        _service.Update(updated);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }

    [HttpPost("{id}/{actionType}")]
    public IActionResult HandleVehicleAction(int id, string actionType, [FromBody] ActionRequest? request = null)
    {
        var actions = new Dictionary<string, Func<int, ActionRequest?, IActionResult>>(StringComparer.OrdinalIgnoreCase)
    {
        { "rent", (vid, req) =>
            {
                if (req == null || string.IsNullOrWhiteSpace(req.NamaPeminjam))
                    return BadRequest("Nama peminjam harus diisi.");
                var result = _service.RentVehicle(vid, req.NamaPeminjam);
                return result.success ? Ok(result) : BadRequest(result.message);
            }
        },
        { "return", (vid, _) =>
            {
                var result = _service.ReturnVehicle(vid);
                return result.success ? Ok(result) : BadRequest(result.message);
            }
        }
    };

        if (!actions.TryGetValue(actionType, out var handler))
            return BadRequest("Action tidak dikenal. Gunakan 'rent' atau 'return'.");

        return handler(id, request);
    }


}