using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;
using WebStore.Data.Repositories.RoleRepository;
using WebStore.Domain.Consts;

namespace WebStore.Data.Mocks.RoleMock
{
    public class RoleMock : IRoleMock
    {
        private readonly IRoleRepository roleRepository;
        private readonly IValidator<Role> roleValidator;

        public RoleMock(IRoleRepository roleRepository, IValidator<Role> roleValidator)
        {
            this.roleRepository = roleRepository;
            this.roleValidator = roleValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await roleRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            Role[] roles = new Role[]
            {
                new Role(RoleConst.Admin),
                new Role(RoleConst.Moderator),
                new Role(RoleConst.User),
                new Role(RoleConst.Guest),
            };

            roles.Select(async role => await roleValidator.ValidateAndThrowAsync(role, cancellationToken));

            return await roleRepository.AddRangeAsync(roles, cancellationToken);
        }
    }
}
