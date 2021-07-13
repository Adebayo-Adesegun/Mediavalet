using System.Threading.Tasks;

namespace Mediavalet.Domain.Interfaces
{
    public interface IOrderCounterService
    {
        Task<int> GetNextOrderId();
    }
}