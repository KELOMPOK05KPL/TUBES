using System.Threading.Tasks;
using Tubes_KPL.Services;

namespace TUBESGUI.factory
{
    public interface IReturnFactory
    {
        Task<string> HandleReturnAsync(int vehicleId, PeminjamanService service);
    }
}
