using System.Threading.Tasks;
using Tubes_KPL.Services;

namespace TUBESGUI.factory
{
    public class MotorReturnFactory : IReturnFactory
    {
        public async Task<string> HandleReturnAsync(int vehicleId, PeminjamanService service)
        {
            var result = await service.HandleAction("return", vehicleId);
            return result ? "Motor berhasil dikembalikan." : "Gagal mengembalikan motor.";
        }
    }
}
