using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductModelRepository
{
    public interface IProductModelRepository : IRepositoryAsync<ProductModel>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
