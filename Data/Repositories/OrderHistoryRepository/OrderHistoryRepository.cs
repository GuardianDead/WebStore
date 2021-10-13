using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.OrderHistoryRepository
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<OrderHistory> orderHistoryValidator;

        public OrderHistoryRepository(AppDbContext db, IValidator<OrderHistory> orderHistoryValidator)
        {
            this.db = db;
            this.orderHistoryValidator = orderHistoryValidator;
        }

        public async ValueTask<IEnumerable<OrderHistory>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.OrderHistories.AsNoTracking().ToListAsync(cancellationToken) :
                await db.OrderHistories.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<OrderHistory>> GetAllAsync(Expression<Func<OrderHistory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.OrderHistories.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.OrderHistories.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<OrderHistory> GetAsync(Expression<Func<OrderHistory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.OrderHistories.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.OrderHistories.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<OrderHistory> GetOrDefaultAsync(Expression<Func<OrderHistory, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.OrderHistories.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.OrderHistories.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.OrderHistories.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<OrderHistory, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.OrderHistories.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<OrderHistory, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.OrderHistories.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(OrderHistory item,
            CancellationToken cancellationToken = default)
        {
            await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.OrderHistories.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<OrderHistory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.OrderHistories.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(OrderHistory item,
            CancellationToken cancellationToken = default)
        {
            await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.OrderHistories.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<OrderHistory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.OrderHistories.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(OrderHistory item,
            CancellationToken cancellationToken = default)
        {
            await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.OrderHistories.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<OrderHistory> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderHistoryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.OrderHistories.RemoveRange(items);
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
