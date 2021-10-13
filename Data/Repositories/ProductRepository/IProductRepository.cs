using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductRepository
{
    public interface IProductRepository : IRepositoryAsync<Product>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
