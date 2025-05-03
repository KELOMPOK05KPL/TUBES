using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using Test_API_tubes.Models;
using Tubes_API;
using Tubes_API.Services;

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

    [HttpPost("{id}/rent")]
    public IActionResult RentVehicle(int id, [FromBody] PeminjamanRequest request)
    {
        var result = _service.RentVehicle(id, request.NamaPeminjam);

        if (!result.success)
        {
            return BadRequest(result.message);
        }

        return Ok(new
        {
            success = true,
            message = result.message,
            vehicleId = id
        });
    }

    public class PeminjamanRequest
    {
        public string NamaPeminjam { get; set; }
    }

    // Di Tubes_API/Controllers/VehicleController.cs
    [HttpPost("{id}/return")]
    public IActionResult ReturnVehicle(int id)
    {
        var result = _service.ReturnVehicle(id);

        if (!result.success)
        {
            return BadRequest(result.message);
        }

        return Ok(new
        {
            success = true,
            message = result.message,
            vehicleId = id
        });
    }

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
}