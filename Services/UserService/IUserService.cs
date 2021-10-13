using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Services.UserService
{
    public interface IUserService
    {
        public ValueTask<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken = default);
        public ValueTask<bool> ConfirmEmailAsync(User user, string confirmToken, CancellationToken cancellationToken = default);

        public ValueTask<bool> AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);
    }
}
