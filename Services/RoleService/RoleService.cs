using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> userManager;
        private readonly IValidator<User> userValidator;
        private readonly IValidator<Role> roleValidator;

        public RoleService(UserManager<User> userManager, IValidator<User> userValidator, IValidator<Role> roleValidator)
        {
            this.userManager = userManager;
            this.userValidator = userValidator;
            this.roleValidator = roleValidator;
        }

        public async ValueTask<bool> AddToRoleAsync(User user, Role role, CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);
            await roleValidator.ValidateAndThrowAsync(role, cancellationToken);
            return (await userManager.AddToRoleAsync(user, role.Name)).Succeeded;
        }

        public async ValueTask<bool> AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException(nameof(roleName));
            return (await userManager.AddToRoleAsync(user, roleName)).Succeeded;
        }
    }
}
