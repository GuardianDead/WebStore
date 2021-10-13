using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductSoldRepository
{
    public class ProductSoldRepository : IProductSoldRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<ProductSold> productSoldValidator;

        public ProductSoldRepository(AppDbContext db, IValidator<ProductSold> productSoldValidator)
        {
            this.db = db;
            this.productSoldValidator = productSoldValidator;
        }

        public async ValueTask<IEnumerable<ProductSold>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductsSold.AsNoTracking().ToListAsync(cancellationToken) :
                await db.ProductsSold.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<ProductSold>> GetAllAsync(Expression<Func<ProductSold, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductsSold.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.ProductsSold.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<ProductSold> GetAsync(Expression<Func<ProductSold, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductsSold.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.ProductsSold.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<ProductSold> GetOrDefaultAsync(Expression<Func<ProductSold, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductsSold.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.ProductsSold.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.ProductsSold.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<ProductSold, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.ProductsSold.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<ProductSold, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.ProductsSold.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(ProductSold item,
            CancellationToken cancellationToken = default)
        {
            await productSoldValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.ProductsSold.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<ProductSold> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(item => productSoldValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.ProductsSold.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(ProductSold item,
            CancellationToken cancellationToken = default)
        {
            await productSoldValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductsSold.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<ProductSold> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(item => productSoldValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductsSold.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(ProductSold item,
            CancellationToken cancellationToken = default)
        {
            await productSoldValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductsSold.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<ProductSold> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(item => productSoldValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductsSold.RemoveRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await db.SaveChangesAsync(cancellationToken) > 0;
        }

        public async ValueTask<bool> DisposeAsync()
        {
            await db.DisposeAsync();
            return await new ValueTask<bool>(true);
        }
    }
}
