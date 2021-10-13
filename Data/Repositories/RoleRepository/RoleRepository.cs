using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Data.Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IValidator<Role> roleValidator;

        public RoleRepository(RoleManager<Role> roleManager, IValidator<Role> roleValidator)
        {
            this.roleManager = roleManager;
            this.roleValidator = roleValidator;
        }

        public async ValueTask<IEnumerable<Role>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken) :
                await roleManager.Roles.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Role>> GetAllAsync(Expression<Func<Role, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await roleManager.Roles.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await roleManager.Roles.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Role> GetAsync(Expression<Func<Role, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await roleManager.Roles.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await roleManager.Roles.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Role> GetOrDefaultAsync(Expression<Func<Role, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await roleManager.Roles.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await roleManager.Roles.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await roleManager.Roles.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await roleManager.Roles.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Role, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await roleManager.Roles.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Role item,
            CancellationToken cancellationToken = default)
        {
            await roleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await roleManager.CreateAsync(item)).Succeeded;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Role> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await roleValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await roleManager.CreateAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Role item,
            CancellationToken cancellationToken = default)
        {
            await roleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await roleManager.UpdateAsync(item)).Succeeded;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Role> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await roleValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await roleManager.UpdateAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Role item,
            CancellationToken cancellationToken = default)
        {
            await roleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await roleManager.DeleteAsync(item)).Succeeded;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Role> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await roleValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await roleManager.DeleteAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DisposeAsync()
        {
            roleManager.Dispose();
            return await new ValueTask<bool>(true);
        }
    }
}
