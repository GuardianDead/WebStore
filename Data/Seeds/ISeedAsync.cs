using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Data
{
    public interface ISeedAsync
    {
        public ValueTask<bool> SeedAsync(CancellationToken cancellationToken = default);
    }
}