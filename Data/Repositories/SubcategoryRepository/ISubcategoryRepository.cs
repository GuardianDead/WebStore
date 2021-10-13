using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories
{
    public interface ISubcategoryRepository : IRepositoryAsync<Subcategory>
    {
        public ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
