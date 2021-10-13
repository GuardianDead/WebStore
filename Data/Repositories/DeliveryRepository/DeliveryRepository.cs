using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.DeliveryRepository
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Delivery> deliveryValidator;

        public DeliveryRepository(AppDbContext db, IValidator<Delivery> deliveryValidator)
        {
            this.db = db;
            this.deliveryValidator = deliveryValidator;
        }

        public async ValueTask<IEnumerable<Delivery>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Deliveries.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Deliveries.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Delivery>> GetAllAsync(Expression<Func<Delivery, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Deliveries.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Deliveries.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Delivery> GetAsync(Expression<Func<Delivery, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Deliveries.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Deliveries.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Delivery> GetOrDefaultAsync(Expression<Func<Delivery, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Deliveries.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Deliveries.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Deliveries.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Delivery, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Deliveries.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Delivery, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Deliveries.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Delivery item,
            CancellationToken cancellationToken = default)
        {
            await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Deliveries.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Delivery> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Deliveries.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Delivery item, CancellationToken cancellationToken = default)
        {
            await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Deliveries.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Delivery> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Deliveries.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Delivery item, CancellationToken cancellationToken = default)
        {
            await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Deliveries.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Delivery> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await deliveryValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Deliveries.RemoveRange(items);
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
