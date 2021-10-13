using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Product> productValidator;

        public ProductRepository(AppDbContext db, IValidator<Product> productValidator)
        {
            this.db = db;
            this.productValidator = productValidator;
        }

        public async ValueTask<IEnumerable<Product>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Products.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Products.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Products.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Products.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Products.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Products.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Product> GetOrDefaultAsync(Expression<Func<Product, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Products.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Products.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Products.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Product, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Products.AsNoTracking().AnyAsync(expression, cancellationToken);
        }


        public async ValueTask<bool> FindAsync(Expression<Func<Product, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Products.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Product item,
            CancellationToken cancellationToken = default)
        {
            await productValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Products.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Product> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Products.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Product item,
            CancellationToken cancellationToken = default)
        {
            await productValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Products.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Product> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Products.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Product item,
            CancellationToken cancellationToken = default)
        {
            await productValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Products.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Product> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Products.RemoveRange(items);
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
