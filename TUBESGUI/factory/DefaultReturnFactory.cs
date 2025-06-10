using System.Threading.Tasks;
using Tubes_KPL.Services;

namespace TUBESGUI.factory
{
    public class DefaultReturnFactory : IReturnFactory
    {
        public async Task<string> HandleReturnAsync(int vehicleId, PeminjamanService service)
        {
            return "Jenis kendaraan tidak dikenali. Pengembalian dibatalkan.";
        }
    }
}
