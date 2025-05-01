using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_API_tubes.Models;
using Test_API_tubes.Repositories;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVehicles() => Ok(DataStore.Vehicles);

    [HttpPost]
    public IActionResult AddVehicle(Vehicle vehicle)
    {
        vehicle.Id = DataStore.Vehicles.Count + 1;
        DataStore.Vehicles.Add(vehicle);
        return Ok(vehicle);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVehicle(int id, Vehicle updated)
    {
        var vehicle = DataStore.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle == null) return NotFound();
        vehicle.Brand = updated.Brand;
        vehicle.Type = updated.Type;
        vehicle.IsAvailable = updated.IsAvailable;
        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVehicle(int id)
    {
        var vehicle = DataStore.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle == null) return NotFound();
        DataStore.Vehicles.Remove(vehicle);
        return Ok();
    }
}