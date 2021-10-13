using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductModelRepository
{
    public class ProductModelRepository : IProductModelRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<ProductModel> productModelValidator;

        public ProductModelRepository(AppDbContext db, IValidator<ProductModel> productModelValidator)
        {
            this.db = db;
            this.productModelValidator = productModelValidator;
        }

        public async ValueTask<IEnumerable<ProductModel>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductModels.AsNoTracking().ToListAsync(cancellationToken) :
                await db.ProductModels.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<ProductModel>> GetAllAsync(Expression<Func<ProductModel, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductModels.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.ProductModels.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<ProductModel> GetAsync(Expression<Func<ProductModel, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductModels.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.ProductModels.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<ProductModel> GetOrDefaultAsync(Expression<Func<ProductModel, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductModels.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.ProductModels.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.ProductModels.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<ProductModel, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.ProductModels.AsNoTracking().AnyAsync(expression, cancellationToken);
        }


        public async ValueTask<bool> FindAsync(Expression<Func<ProductModel, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.ProductModels.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(ProductModel item,
            CancellationToken cancellationToken = default)
        {
            await productModelValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.ProductModels.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<ProductModel> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productModelValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.ProductModels.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(ProductModel item,
            CancellationToken cancellationToken = default)
        {
            await productModelValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductModels.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<ProductModel> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productModelValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductModels.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(ProductModel item,
            CancellationToken cancellationToken = default)
        {
            await productModelValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductModels.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<ProductModel> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productModelValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductModels.RemoveRange(items);
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
