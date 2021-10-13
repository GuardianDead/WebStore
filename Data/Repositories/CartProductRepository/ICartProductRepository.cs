using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.CartProductRepository
{
    public interface ICartProductRepository : IRepositoryAsync<CartProduct>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
