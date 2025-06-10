using Test_API_tubes.Models;

namespace TUBESGUI.factory
{
    public static class ReturnStrategyFactory
    {
        public static IReturnFactory Create(Vehicle vehicle)
        {
            return vehicle.Type switch
            {
                "Motor" => new MotorReturnFactory(),
                "Mobil" => new CarReturnFactory(),
                _ => new DefaultReturnFactory()
            };
        }
    }
}
