using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain.Types;

namespace WebStore.Data.Mocks.OrderMock
{
    public class OrderMock : IOrderMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<Order> orderValidator;

        public OrderMock(AppDbContext db, IValidator<Order> orderValidator)
        {
            this.db = db;
            this.orderValidator = orderValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Orders.AnyAsync(cancellationToken))
                return true;

            var products = await db.Products.ToListAsync(cancellationToken);
            var selectedProducts = new IEnumerable<Product>[]
            {
                products.Skip(0).Take(3),
                products.Skip(3).Take(3),
                products.Skip(4).Take(3)
            };
            var deliveries = new Delivery[]
            {
                await db.Deliveries.SingleOrDefaultAsync(delivery => delivery.Name == "Почта России", cancellationToken),
                await db.Deliveries.SingleOrDefaultAsync(delivery => delivery.Name == "BoxBerry", cancellationToken),
            };

            var orders = new Order[]
            {
                new Order(
                    products: selectedProducts[0].ToList(),
                    delivery: deliveries[0],
                    orderPaymentMethodType: OrderPaymentMethodType.Card,
                    dateTimeCreation: DateTime.Now,
                    orderStatusType: OrderStatusType.AwaitingProcessing,
                    address: new Address(country: "Россия",city: "Муром",street: "Мечникова",houseNumber: "55",postalCode: "602267"),
                    totalCost: selectedProducts[0].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[0].Cost,
                    trackNumber: "ZH4152621324RW",
                    email: "kakawkawww13@mail.ru"
                    )
                    {
                        PhoneNumber = "79157675803"
                    },
                new Order(
                    products: selectedProducts[1].ToList(),
                    delivery: deliveries[1],
                    orderPaymentMethodType: OrderPaymentMethodType.Cash,
                    dateTimeCreation: DateTime.Now,
                    orderStatusType: OrderStatusType.Arrived,
                    address: new Address(country: "Россия",city: "Муром",street: "Мечникова",houseNumber: "55",postalCode: "602267"),
                    totalCost: selectedProducts[1].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[1].Cost,
                    trackNumber: "ZH3262363235WF",
                    email: "kakawkawww12@mail.ru"
                    )
                    {
                        PhoneNumber = "79157675803"
                    },
                new Order(
                    products: selectedProducts[2].ToList(),
                    delivery: deliveries[0],
                    orderPaymentMethodType: OrderPaymentMethodType.Card,
                    dateTimeCreation: DateTime.Now,
                    orderStatusType: OrderStatusType.Canceled,
                    address: new Address("Россия","Муром","Мечникова","55","602267"),
                    totalCost: selectedProducts[2].Aggregate(0.0m,(sum,productarticle) => sum + productarticle.Article.Model.Price) + deliveries[0].Cost,
                    trackNumber: "ZH3262363235WF",
                    email: "kakawkawww17@mail.ru"
                    )
                    {
                        PhoneNumber = "79157672475"
                    },
            };

            foreach (Order order in orders)
                await orderValidator.ValidateAndThrowAsync(order, cancellationToken);

            await db.Orders.AddRangeAsync(orders, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
