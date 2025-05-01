using Test_API_tubes.Models;

namespace Test_API_tubes.Repositories
{
    public static class DataStore
    {
        public static List<User> Users = new();

        public static List<Vehicle> Vehicles = new List<Vehicle>
        {
            new Vehicle(1, "Honda", "Corolla", "Sedan", true),
            new Vehicle(2, "Toyota", "Camry", "Sedan", true),
            new Vehicle(3, "Ford", "Mustang", "Coupe", false),
            new Vehicle(4, "Chevrolet", "Impala", "Sedan", true),
            new Vehicle(5, "BMW", "X5", "SUV", true),
            new Vehicle(6, "Mercedes", "GLA", "SUV", false),
            new Vehicle(7, "Hyundai", "Elantra", "Sedan", true),
            new Vehicle(8, "Kia", "Sportage", "SUV", true),
            new Vehicle(9, "Tesla", "Model S", "Sedan", false),
            new Vehicle(10, "Nissan", "Altima", "Sedan", true),
            new Vehicle(11, "Volkswagen", "Passat", "Sedan", true),
            new Vehicle(12, "Jeep", "Wrangler", "SUV", false),
            new Vehicle(13, "Subaru", "Forester", "SUV", true),
            new Vehicle(14, "Lexus", "RX 350", "SUV", true),
            new Vehicle(15, "Audi", "A4", "Sedan", false),
        };

        public static List<Rental> Rentals = new();
    }

}
