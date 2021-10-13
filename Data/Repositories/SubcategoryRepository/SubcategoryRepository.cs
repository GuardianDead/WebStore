using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.SubcategoryRepository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Subcategory> subcategoryValidator;

        public SubcategoryRepository(AppDbContext db, IValidator<Subcategory> subcategoryValidator)
        {
            this.db = db;
            this.subcategoryValidator = subcategoryValidator;
        }

        public async ValueTask<IEnumerable<Subcategory>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Subcategories.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Subcategories.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Subcategory>> GetAllAsync(Expression<Func<Subcategory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Subcategories.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Subcategories.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Subcategory> GetAsync(Expression<Func<Subcategory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Subcategories.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Subcategories.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Subcategory> GetOrDefaultAsync(Expression<Func<Subcategory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Subcategories.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Subcategories.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Subcategories.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Subcategory, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Subcategories.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Subcategory, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Subcategories.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Subcategory item,
            CancellationToken cancellationToken = default)
        {
            await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Subcategories.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Subcategory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Subcategories.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Subcategory item,
            CancellationToken cancellationToken = default)
        {
            await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Subcategories.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Subcategory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Subcategories.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Subcategory item,
            CancellationToken cancellationToken = default)
        {
            await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Subcategories.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Subcategory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await subcategoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Subcategories.RemoveRange(items);
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
