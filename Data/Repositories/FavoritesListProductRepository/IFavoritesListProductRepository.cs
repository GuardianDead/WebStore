using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.FavoritesListProductRepository
{
    public interface IFavoritesListProductRepository : IRepositoryAsync<FavoritesListProduct>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}