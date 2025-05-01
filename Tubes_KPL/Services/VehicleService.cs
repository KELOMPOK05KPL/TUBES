using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_KPL.Models;
using Tubes_KPL.Utilities;

namespace Tubes_KPL.Services
{
    public class VehicleService : IVehicleService
    {
        // Table-Driven: Simpan data dalam Dictionary (bisa diganti database)
        private readonly Dictionary<string, Vehicle> _vehiclesTable;

        public VehicleService()
        {
            _vehiclesTable = new Dictionary<string, Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (!_vehiclesTable.ContainsKey(vehicle.Id))
            {
                _vehiclesTable.Add(vehicle.Id, vehicle);
            }
            else
            {
                throw new Exception("Kendaraan sudah terdaftar.");
            }
        }

        public Vehicle GetVehicleById(string id)
        {
            return TableDrivenHelper.GetFromTable(_vehiclesTable, id, "Kendaraan tidak ditemukan");
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehiclesTable.Values.ToList();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            TableDrivenHelper.UpdateInTable(_vehiclesTable, vehicle.Id, vehicle, "Kendaraan tidak ditemukan");
        }

        public void DeleteVehicle(string id)
        {
            TableDrivenHelper.DeleteFromTable(_vehiclesTable, id, "Kendaraan tidak ditemukan");
        }
    }
}
