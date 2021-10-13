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
using WebStore.Data.Repositories.AppIdentityUserRepository;

namespace WebStore.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly IValidator<User> userValidator;

        public UserRepository(UserManager<User> UserManager, IValidator<User> userValidator)
        {
            this.userManager = UserManager;
            this.userValidator = userValidator;
        }

        public async ValueTask<IEnumerable<User>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await userManager.Users.AsNoTracking().ToListAsync(cancellationToken) :
                await userManager.Users.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await userManager.Users.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await userManager.Users.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await userManager.Users.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await userManager.Users.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<User> GetOrDefaultAsync(Expression<Func<User, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await userManager.Users.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await userManager.Users.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await userManager.Users.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await userManager.Users.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<User, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await userManager.Users.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(User item,
            CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await userManager.CreateAsync(item)).Succeeded;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<User> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await userValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await userManager.CreateAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(User item,
            CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await userManager.UpdateAsync(item)).Succeeded;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<User> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await userValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await userManager.UpdateAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(User item,
            CancellationToken cancellationToken = default)
        {
            await userValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await userManager.DeleteAsync(item)).Succeeded;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<User> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await userValidator.ValidateAndThrowAsync(item, cancellationToken));
            foreach (var item in items)
            {
                if (!(await userManager.DeleteAsync(item)).Succeeded)
                {
                    return await new ValueTask<bool>(false);
                }
            }
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DisposeAsync()
        {
            userManager.Dispose();
            return await new ValueTask<bool>(true);
        }
    }
}
