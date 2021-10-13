using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories.ListFavoritesRepository;

namespace WebStore.Data.Repositories.FavoritesListRepository
{
    public class FavoritesListRepository : IFavoritesListRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<FavoritesList> favoritesListValidator;

        public FavoritesListRepository(AppDbContext db, IValidator<FavoritesList> favoritesListValidator)
        {
            this.db = db;
            this.favoritesListValidator = favoritesListValidator;
        }

        public async ValueTask<IEnumerable<FavoritesList>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesLists.AsNoTracking().ToListAsync(cancellationToken) :
                await db.FavoritesLists.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<FavoritesList>> GetAllAsync(Expression<Func<FavoritesList, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesLists.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.FavoritesLists.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<FavoritesList> GetAsync(Expression<Func<FavoritesList, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesLists.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.FavoritesLists.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<FavoritesList> GetOrDefaultAsync(Expression<Func<FavoritesList, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.FavoritesLists.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.FavoritesLists.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.FavoritesLists.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<FavoritesList, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.FavoritesLists.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<FavoritesList, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.FavoritesLists.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(FavoritesList item,
            CancellationToken cancellationToken = default)
        {
            await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.FavoritesLists.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<FavoritesList> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.FavoritesLists.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(FavoritesList item, CancellationToken cancellationToken = default)
        {
            await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.FavoritesLists.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<FavoritesList> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.FavoritesLists.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(FavoritesList item, CancellationToken cancellationToken = default)
        {
            await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.FavoritesLists.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<FavoritesList> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await favoritesListValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.FavoritesLists.RemoveRange(items);
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
