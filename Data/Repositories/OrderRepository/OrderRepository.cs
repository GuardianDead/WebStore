using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Order> orderValidator;

        public OrderRepository(AppDbContext db, IValidator<Order> orderValidator)
        {
            this.db = db;
            this.orderValidator = orderValidator;
        }

        public async ValueTask<IEnumerable<Order>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Orders.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Orders.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Orders.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Orders.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Order> GetAsync(Expression<Func<Order, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Orders.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Orders.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Order> GetOrDefaultAsync(Expression<Func<Order, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Orders.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Orders.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Orders.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Orders.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Order, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Orders.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Order item,
            CancellationToken cancellationToken = default)
        {
            await orderValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Orders.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Order> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Orders.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Order item,
            CancellationToken cancellationToken = default)
        {
            await orderValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Orders.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Order> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Orders.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Order item,
            CancellationToken cancellationToken = default)
        {
            await orderValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Orders.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Order> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await orderValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Orders.RemoveRange(items);
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
