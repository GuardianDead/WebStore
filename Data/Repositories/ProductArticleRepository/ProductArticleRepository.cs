using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.ProductArticleRepository
{
    public class ProductArticleRepository : IProductArticleRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<ProductArticle> productArticleValidator;

        public ProductArticleRepository(AppDbContext db, IValidator<ProductArticle> productArticleValidator)
        {
            this.db = db;
            this.productArticleValidator = productArticleValidator;
        }

        public async ValueTask<IEnumerable<ProductArticle>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductArticles.AsNoTracking().ToListAsync(cancellationToken) :
                await db.ProductArticles.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<ProductArticle>> GetAllAsync(Expression<Func<ProductArticle, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductArticles.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.ProductArticles.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<ProductArticle> GetAsync(Expression<Func<ProductArticle, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductArticles.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.ProductArticles.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<ProductArticle> GetOrDefaultAsync(Expression<Func<ProductArticle, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.ProductArticles.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.ProductArticles.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.ProductArticles.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<ProductArticle, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.ProductArticles.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<ProductArticle, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.ProductArticles.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(ProductArticle item,
            CancellationToken cancellationToken = default)
        {
            await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.ProductArticles.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<ProductArticle> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.ProductArticles.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(ProductArticle item,
            CancellationToken cancellationToken = default)
        {
            await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductArticles.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<ProductArticle> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductArticles.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(ProductArticle item,
            CancellationToken cancellationToken = default)
        {
            await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.ProductArticles.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<ProductArticle> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await productArticleValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.ProductArticles.RemoveRange(items);
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
