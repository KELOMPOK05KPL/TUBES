using System.Text.Json;
using Test_API_tubes.Models;
//using Tubes_RentalAPI.Models;
namespace Tubes_API.Services
{
    public class VehicleService
    {
        private readonly string filePath = "Data/vehicles.json";

        public List<Vehicle> GetAll()
        {
            if (!File.Exists(filePath))
                return new List<Vehicle>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
        }

        public void SaveAll(List<Vehicle> vehicles)
        {
            string json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public Vehicle? GetById(int id) => GetAll().FirstOrDefault(v => v.Id == id);

        public void Add(Vehicle vehicle)
        {
            var vehicles = GetAll();
            vehicle.Id = vehicles.Count > 0 ? vehicles.Max(v => v.Id) + 1 : 1;
            vehicles.Add(vehicle);
            SaveAll(vehicles);
        }

        public void Update(Vehicle updated)
        {
            var vehicles = GetAll();
            var index = vehicles.FindIndex(v => v.Id == updated.Id);
            if (index >= 0)
            {
                vehicles[index] = updated;
                SaveAll(vehicles);
            }
        }

        public void Delete(int id)
        {
            var vehicles = GetAll();
            var updated = vehicles.Where(v => v.Id != id).ToList();
            SaveAll(updated);
        }
    }
}
