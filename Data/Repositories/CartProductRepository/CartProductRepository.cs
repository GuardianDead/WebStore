using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.CartProductRepository
{
    public class CartProductRepository : ICartProductRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<CartProduct> CartProductValidator;

        public CartProductRepository(AppDbContext db, IValidator<CartProduct> CartProductValidator)
        {
            this.db = db;
            this.CartProductValidator = CartProductValidator;
        }

        public async ValueTask<IEnumerable<CartProduct>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.CartProducts.AsNoTracking().ToListAsync(cancellationToken) :
                await db.CartProducts.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<CartProduct>> GetAllAsync(Expression<Func<CartProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.CartProducts.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.CartProducts.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<CartProduct> GetAsync(Expression<Func<CartProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.CartProducts.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.CartProducts.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<CartProduct> GetOrDefaultAsync(Expression<Func<CartProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.CartProducts.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.CartProducts.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.CartProducts.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<CartProduct, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.CartProducts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<CartProduct, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.CartProducts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(CartProduct item,
            CancellationToken cancellationToken = default)
        {
            await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.CartProducts.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<CartProduct> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.CartProducts.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(CartProduct item,
            CancellationToken cancellationToken = default)
        {
            await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.CartProducts.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<CartProduct> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.CartProducts.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(CartProduct item, CancellationToken cancellationToken = default)
        {
            await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.CartProducts.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<CartProduct> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await CartProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.CartProducts.RemoveRange(items);
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
