using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories;
using WebStore.Domain;

namespace WebStore.Data.Mocks.DeliveryMock
{
    public class DeliveryMock : IDeliveryMock
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IValidator<Delivery> deliveryValidator;

        public DeliveryMock(IDeliveryRepository deliveryRepository, IValidator<Delivery> deliveryValidator)
        {
            this.deliveryRepository = deliveryRepository;
            this.deliveryValidator = deliveryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await deliveryRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

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

            deliveries.Select(async delivery => await deliveryValidator.ValidateAndThrowAsync(delivery, cancellationToken));

            await deliveryRepository.AddRangeAsync(deliveries, cancellationToken);
            return await deliveryRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
