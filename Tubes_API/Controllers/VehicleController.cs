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
    public IActionResult HandleVehicleAction(
    int id,
    string actionType,
    [FromBody] ActionRequest? request = null)
    {
        var actions = new Dictionary<string, Func<int, ActionRequest?, IActionResult>>(
            StringComparer.OrdinalIgnoreCase)
    {
        {
            "rent", (vid, req) =>
            {
                if (req == null || string.IsNullOrEmpty(req.NamaPeminjam))
                    return BadRequest("NamaPeminjam wajib diisi untuk penyewaan");

                var result = _service.RentVehicle(vid, req.NamaPeminjam);
                return new OkObjectResult(new {
                    success = true,
                    message = result.message,
                    vehicleId = vid
                });
            }
        },
        {
            "return", (vid, _) =>
            {
                var result = _service.ReturnVehicle(vid);
                if (!result.success)
                    return BadRequest(result.message);

                return new OkObjectResult(new {
                    success = true,
                    message = result.message,
                    vehicleId = vid
                });
            }
        }
    };

        if (!actions.TryGetValue(actionType, out var handler))
            return BadRequest("Action tidak valid. Gunakan 'rent' atau 'return'");

        try
        {
            return handler(id, request);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Terjadi error: {ex.Message}");
        }
    }

}