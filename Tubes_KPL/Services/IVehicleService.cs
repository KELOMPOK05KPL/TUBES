using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_KPL.Models;

namespace Tubes_KPL.Services
{
    public interface IVehicleService
    {
        void AddVehicle(Vehicle vehicle);
        Vehicle GetVehicleById(string id);
        List<Vehicle> GetAllVehicles();
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(string id);
    }
}
