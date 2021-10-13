using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IValidator<User> userValidator;

        public UserService(UserManager<User> userManager, IValidator<User> userValidator)
        {
            this.userManager = userManager;
            this.userValidator = userValidator;
        }

        public async ValueTask<bool> AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);
            if (claims == Enumerable.Empty<Claim>() || claims is null) throw new ArgumentNullException(nameof(claims));
            return (await userManager.AddClaimsAsync(user, claims)).Succeeded;
        }

        public async ValueTask<bool> ConfirmEmailAsync(User user, string confirmToken, CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);
            if (string.IsNullOrEmpty(confirmToken)) throw new ArgumentNullException(nameof(confirmToken));
            return (await userManager.ConfirmEmailAsync(user, confirmToken)).Succeeded;
        }

        public async ValueTask<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}
