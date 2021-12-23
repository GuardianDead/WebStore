using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain.Consts;

namespace WebStore.Data.Mocks.RoleMock
{
    public class RoleMock : IRoleMock
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IValidator<Role> roleValidator;

        public RoleMock(RoleManager<Role> roleManager, IValidator<Role> roleValidator)
        {
            this.roleManager = roleManager;
            this.roleValidator = roleValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await roleManager.Roles.AnyAsync(cancellationToken))
                return true;

            var roles = new Role[]
            {
                new Role(RoleConst.Admin),
                new Role(RoleConst.Moderator),
                new Role(RoleConst.User),
            };

            foreach (Role role in roles)
                await roleValidator.ValidateAndThrowAsync(role, cancellationToken);

            foreach (Role role in roles)
            {
                var identityAddRoleResult = await roleManager.CreateAsync(role);
                if (!identityAddRoleResult.Succeeded)
                    throw new NotImplementedException($"Роль с именем {role.Name} не удалось добавить в базу данных");
            }
            return true;
        }
    }
}
