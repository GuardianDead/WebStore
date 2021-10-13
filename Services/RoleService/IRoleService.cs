using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Services.RoleService
{
    public interface IRoleService
    {
        public ValueTask<bool> AddToRoleAsync(User user, Role role, CancellationToken cancellationToken = default);
        public ValueTask<bool> AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default);
    }
}
