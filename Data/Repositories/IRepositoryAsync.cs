using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Data.Repositories
{
    public interface IRepositoryAsync<TEntity>
    {
        public ValueTask<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        public ValueTask<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false,
            CancellationToken cancellationToken = default);

        public ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false,
            CancellationToken cancellationToken = default);
        public ValueTask<TEntity> GetOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false,
            CancellationToken cancellationToken = default);

        public ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default);
        public ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        public ValueTask<bool> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        public ValueTask<bool> AddAsync(TEntity item, CancellationToken cancellationToken = default);
        public ValueTask<bool> AddRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);

        public ValueTask<bool> UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
        public ValueTask<bool> UpdateRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);

        public ValueTask<bool> DeleteAsync(TEntity item, CancellationToken cancellationToken = default);
        public ValueTask<bool> DeleteRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);

        public ValueTask<bool> DisposeAsync();
    }
}