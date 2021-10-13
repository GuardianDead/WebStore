using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Repositories.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext db;
        private readonly IValidator<Address> addressValidator;

        public AddressRepository(AppDbContext db, IValidator<Address> addressValidator)
        {
            this.db = db;
            this.addressValidator = addressValidator;
        }

        public async ValueTask<IEnumerable<Address>> GetAllAsync(bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Addresses.AsNoTracking().ToListAsync(cancellationToken) :
                await db.Addresses.ToListAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Address>> GetAllAsync(Expression<Func<Address, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Addresses.AsNoTracking().Where(expression).ToListAsync(cancellationToken) :
                await db.Addresses.Where(expression).ToListAsync(cancellationToken);
        }

        public async ValueTask<Address> GetAsync(Expression<Func<Address, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Addresses.AsNoTracking().SingleAsync(expression, cancellationToken) :
                await db.Addresses.SingleAsync(expression, cancellationToken);
        }

        public async ValueTask<Address> GetOrDefaultAsync(Expression<Func<Address, bool>> expression,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return asNoTracking ?
                await db.Addresses.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken) :
                await db.Addresses.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await db.Addresses.AsNoTracking().AnyAsync(cancellationToken);
        }

        public async ValueTask<bool> AnyAsync(Expression<Func<Address, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await db.Addresses.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> FindAsync(Expression<Func<Address, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await db.Addresses.AsNoTracking().AnyAsync(expression, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(Address item,
            CancellationToken cancellationToken = default)
        {
            await addressValidator.ValidateAndThrowAsync(item, cancellationToken);
            return (await db.Addresses.AddAsync(item, cancellationToken)).State == EntityState.Added;
        }

        public async ValueTask<bool> AddRangeAsync(IEnumerable<Address> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await addressValidator.ValidateAndThrowAsync(item, cancellationToken));
            await db.Addresses.AddRangeAsync(items, cancellationToken);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> UpdateAsync(Address item,
            CancellationToken cancellationToken = default)
        {
            await addressValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Addresses.Update(item).State == EntityState.Modified;
        }

        public async ValueTask<bool> UpdateRangeAsync(IEnumerable<Address> items,
            CancellationToken cancellationToken = default)
        {
            items.Select(async item => await addressValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Addresses.UpdateRange(items);
            return await new ValueTask<bool>(true);
        }

        public async ValueTask<bool> DeleteAsync(Address item, CancellationToken cancellationToken = default)
        {
            await addressValidator.ValidateAndThrowAsync(item, cancellationToken);
            return db.Addresses.Remove(item).State == EntityState.Deleted;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<Address> items, CancellationToken cancellationToken = default)
        {
            items.Select(async item => await addressValidator.ValidateAndThrowAsync(item, cancellationToken));
            db.Addresses.RemoveRange(items);
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
