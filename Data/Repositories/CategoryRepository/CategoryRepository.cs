using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Category> categoryValidator;

        public CategoryRepository(AppDbContext db, IValidator<Category> categoryValidator)
        {
            this.db = db;
            this.categoryValidator = categoryValidator;
        }

        public async ValueTask<IEnumerable<Category>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Categories.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Categories.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Categories.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Categories.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Category> GetAsync(Expression<Func<Category, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Categories.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Categories.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Category> GetOrDefaultAsync(Expression<Func<Category, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Categories.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Categories.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Categories.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Categories.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Category, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Categories.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Category item,
            CancellationToken cancellationToken = default)
        {
            await categoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Categories.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Category> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await categoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Categories.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Category item, CancellationToken cancellationToken = default)
        {
            await categoryValidator.ValidateAndThrowAsync(item);
            return db.Categories.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Category> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await categoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Categories.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Category item, CancellationToken cancellationToken = default)
        {
            await categoryValidator.ValidateAndThrowAsync(item);
            return db.Categories.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Category> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await categoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Categories.RemoveRange(items);
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
