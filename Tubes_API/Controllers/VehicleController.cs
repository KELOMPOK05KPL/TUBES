using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using Test_API_tubes.Models;
using Tubes_API.Services;
using Tubes_API.Models;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _service;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "http://localhost:5176";
    private readonly string _riwayatFilePath = "Data/RiwayatPeminjaman.json";

    public VehicleController(VehicleService service, HttpClient httpClient)
    {
        _service = service;
        _httpClient = httpClient;
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

    [HttpPut("{id}/return")]
    public IActionResult ReturnVehicle(int id)
    {
        var vehicle = _service.GetById(id);
        if (vehicle == null || vehicle.State != VehicleState.Rented)
            return BadRequest("Kendaraan tidak valid atau tidak sedang dipinjam.");

        vehicle.State = VehicleState.Available;
        _service.Update(vehicle);

        return Ok();
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
