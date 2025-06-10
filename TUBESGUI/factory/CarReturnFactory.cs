using System.Threading.Tasks;
using Tubes_KPL.Services;

namespace TUBESGUI.factory
{
    public class CarReturnFactory : IReturnFactory
    {
        public async Task<string> HandleReturnAsync(int vehicleId, PeminjamanService service)
        {
            var result = await service.HandleAction("return", vehicleId);
            return result ? "Mobil berhasil dikembalikan." : "Gagal mengembalikan mobil.";
        }
    }
}
