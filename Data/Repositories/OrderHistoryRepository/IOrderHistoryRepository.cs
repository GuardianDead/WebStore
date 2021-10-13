using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.OrderHistoryRepository
{
    public interface IOrderHistoryRepository : IRepositoryAsync<OrderHistory>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
