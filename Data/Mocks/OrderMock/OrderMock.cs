using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories;
using WebStore.Data.Repositories.OrderRepository;
using WebStore.Data.Repositories.ProductRepository;
using WebStore.Domain;

namespace WebStore.Data.Mocks.OrderMock
{
    public class OrderMock : IOrderMock
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<Order> orderValidator;
        private readonly IProductRepository productRepository;
        private readonly IDeliveryRepository deliveryRepository;

        public OrderMock(IOrderRepository orderRepository, IValidator<Order> orderValidator,
            IProductRepository productRepository, IDeliveryRepository deliveryRepository)
        {
            this.orderRepository = orderRepository;
            this.orderValidator = orderValidator;
            this.productRepository = productRepository;
            this.deliveryRepository = deliveryRepository;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await orderRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var products = new IEnumerable<Product>[]
            {
                (await productRepository.GetAllAsync(false, cancellationToken)).Skip(0).Take(3),
                (await productRepository.GetAllAsync(false, cancellationToken)).Skip(3).Take(3),
                (await productRepository.GetAllAsync(false, cancellationToken)).Skip(4).Take(3)
            };
            var deliveries = new Delivery[]
            {
                await deliveryRepository.GetAsync(delivery => delivery.Name == "Почта России", false, cancellationToken),
                await deliveryRepository.GetAsync(delivery => delivery.Name == "BoxBerry", false, cancellationToken),
            };

            var orders = new Order[]
            {
                new Order(
                    products[0], deliveries[0],
                    OrderPaymentMethodType.Card, DateTime.Now, OrderStatusType.AwaitingProcessing,
                    new Address("Россия","Муром","Мечникова","55","602267"),
                    products[0].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[0].DeliveryCost,
                    79157675803),
                new Order(
                    products[1], deliveries[1],
                    OrderPaymentMethodType.Cash, DateTime.Now, OrderStatusType.Arrived,
                    new Address("Россия","Муром","Мечникова","55","602267"),
                    products[1].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[1].DeliveryCost,
                    79157675803),
                new Order(
                    products[2], deliveries[0],
                    OrderPaymentMethodType.Card, DateTime.Now, OrderStatusType.Canceled,
                    new Address("Россия","Муром","Мечникова","55","602267"),
                    products[2].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[0].DeliveryCost,
                    79157675803),
            };

            orders.Select(async order => await orderValidator.ValidateAndThrowAsync(order, cancellationToken));

            await orderRepository.AddRangeAsync(orders, cancellationToken);
            return await orderRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
