using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain.Types;

namespace WebStore.Data.Mocks.DeliveryMock
{
    public class DeliveryMock : IDeliveryMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<Delivery> deliveryValidator;

        public DeliveryMock(AppDbContext db, IValidator<Delivery> deliveryValidator)
        {
            this.db = db;
            this.deliveryValidator = deliveryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Deliveries.AnyAsync(cancellationToken))
                return true;

            Delivery[] deliveries =
            {
                new Delivery("Почта России",DeliveryMethodType.Post,300,5),
                new Delivery("BoxBerry", DeliveryMethodType.Post, 400, 3),
                new Delivery("СДЭК", DeliveryMethodType.Post, 300, 4),
                new Delivery("Доставка Veco", DeliveryMethodType.Post, 400, 3),
                new Delivery("Магазин Veco", DeliveryMethodType.Pickup, 0, 0),
                new Delivery("DPD", DeliveryMethodType.Courier, 400, 3),
                new Delivery("Курьерская доставка Veco", DeliveryMethodType.Courier, 500, 3),
            };

            foreach (Delivery delivery in deliveries)
                await deliveryValidator.ValidateAndThrowAsync(delivery, cancellationToken);

            await db.Deliveries.AddRangeAsync(deliveries, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
