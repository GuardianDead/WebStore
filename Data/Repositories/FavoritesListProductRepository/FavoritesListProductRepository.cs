using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.FavoritesListProductRepository
{
    public class FavoritesListProductRepository : IFavoritesListProductRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<FavoritesListProduct> FavoritesListProductValidator;

        public FavoritesListProductRepository(AppDbContext db, IValidator<FavoritesListProduct> FavoritesListProductValidator)
        {
            this.db = db;
            this.FavoritesListProductValidator = FavoritesListProductValidator;
        }

        public async ValueTask<IEnumerable<FavoritesListProduct>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesListProducts.AsNoTracking().ToListAsync(cancellationToken) :
                await db.FavoritesListProducts.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<FavoritesListProduct>> GetAllAsync(Expression<Func<FavoritesListProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesListProducts.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.FavoritesListProducts.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<FavoritesListProduct> GetAsync(Expression<Func<FavoritesListProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesListProducts.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.FavoritesListProducts.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<FavoritesListProduct> GetOrDefaultAsync(Expression<Func<FavoritesListProduct, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesListProducts.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.FavoritesListProducts.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.FavoritesListProducts.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<FavoritesListProduct, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.FavoritesListProducts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<FavoritesListProduct, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.FavoritesListProducts.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(FavoritesListProduct item,
            CancellationToken cancellationToken = default)
        {
            await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.FavoritesListProducts.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<FavoritesListProduct> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.FavoritesListProducts.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(FavoritesListProduct item,
            CancellationToken cancellationToken = default)
        {
            await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.FavoritesListProducts.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<FavoritesListProduct> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.FavoritesListProducts.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(FavoritesListProduct item, CancellationToken cancellationToken = default)
        {
            await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.FavoritesListProducts.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<FavoritesListProduct> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await FavoritesListProductValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.FavoritesListProducts.RemoveRange(items);
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
