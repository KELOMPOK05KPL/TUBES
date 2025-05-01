using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_API_tubes.Models;
using Test_API_tubes.Repositories;

namespace Test_API_tubes.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalController : ControllerBase
{
    [HttpPost("rent")]
    public IActionResult RentVehicle(int userId, int vehicleId)
    {
        var vehicle = DataStore.Vehicles.FirstOrDefault(v => v.Id == vehicleId && v.IsAvailable);
        if (vehicle == null) return BadRequest("Kendaraan tidak tersedia.");

        vehicle.IsAvailable = false;
        var rental = new Rental
        {
            Id = DataStore.Rentals.Count + 1,
            UserId = userId,
            VehicleId = vehicleId,
            RentDate = DateTime.Now
        };
        DataStore.Rentals.Add(rental);
        return Ok(rental);
    }

    [HttpPost("return")]
    public IActionResult ReturnVehicle(int rentalId)
    {
        var rental = DataStore.Rentals.FirstOrDefault(r => r.Id == rentalId && r.ReturnDate == null);
        if (rental == null) return BadRequest("Data penyewaan tidak valid atau sudah dikembalikan.");

        rental.ReturnDate = DateTime.Now;
        var vehicle = DataStore.Vehicles.FirstOrDefault(v => v.Id == rental.VehicleId);
        if (vehicle != null) vehicle.IsAvailable = true;
        return Ok(rental);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserRentals(int userId)
    {
        var history = DataStore.Rentals.Where(r => r.UserId == userId);
        return Ok(history);
    }

    [HttpGet("all")]
    public IActionResult GetAllRentals() => Ok(DataStore.Rentals);
}