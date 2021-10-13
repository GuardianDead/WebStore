using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Cart> cartValidator;

        public CartRepository(AppDbContext db, IValidator<Cart> cartValidator)
        {
            this.db = db;
            this.cartValidator = cartValidator;
        }

        public async ValueTask<IEnumerable<Cart>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Carts.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Carts.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Cart>> GetAllAsync(Expression<Func<Cart, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Carts.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Carts.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Cart> GetAsync(Expression<Func<Cart, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Carts.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Carts.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Cart> GetOrDefaultAsync(Expression<Func<Cart, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Carts.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Carts.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Carts.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Cart, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Carts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Cart, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Carts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Cart item,
            CancellationToken cancellationToken = default)
        {
            await cartValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Carts.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Cart> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await cartValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Carts.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Cart item, CancellationToken cancellationToken = default)
        {
            await cartValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Carts.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Cart> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await cartValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Carts.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Cart item, CancellationToken cancellationToken = default)
        {
            await cartValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Carts.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Cart> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await cartValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Carts.RemoveRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (var transaction = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                if (await db.SaveChangesAsync(cancellationToken) >= 0)
                {
                    await transaction.CommitAsync(cancellationToken);
                    return await new ValueTask<bool>(true);
                }
                else
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return await new ValueTask<bool>(false);
                }
            }
        }

        public async ValueTask<bool> DisposeAsync()
        {
            await db.DisposeAsync();
            return await new ValueTask<bool>(true);
        }
    }
}
