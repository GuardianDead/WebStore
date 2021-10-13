using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ListFavoritesRepository
{
    public interface IFavoritesListRepository : IRepositoryAsync<FavoritesList>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
