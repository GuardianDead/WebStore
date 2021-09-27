using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain;

namespace WebStore.Data.Mocks
{
    public class OrdersMock
    {
        public static void Init(AppDbContext db)
        {
            if(db.Orders.FirstOrDefault() != null)
            {
                return;
            }

            var products1 = db.ProductArticles.Skip(0).Take(3).ToList();
            var delivery1 = db.Deliveries.SingleOrDefault(i => i.Name == "Почта России");
            var products2 = db.ProductArticles.Skip(3).Take(3).ToList();
            var delivery2 = db.Deliveries.SingleOrDefault(i => i.Name == "BoxBerry");
            var products3 = db.ProductArticles.Skip(4).Take(3).ToList();
            var delivery3 = db.Deliveries.SingleOrDefault(i => i.Name == "Почта России");

            var orders = new Order[]
            {
                new Order(products1.ToList(),delivery1,
                OrderPaymentMethodType.Cash,
                DateTime.Now,OrderStatusType.Canceled,
                "Россия, Владимирская область, Муром, Мечникова, 55, 34",
                products1.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery1.DeliveryCost),

                new Order(products2.ToList(),delivery2,
                OrderPaymentMethodType.Card,
                DateTime.Now,OrderStatusType.AwaitingProcessing,
                "Россия, Владимирская область, Муром, Московская, 54, 59",
                products2.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery2.DeliveryCost),

                new Order(products3.ToList(),delivery3,
                OrderPaymentMethodType.Card,
                DateTime.Now,OrderStatusType.Packing,
                "Россия, Владимирская область, Муром, Мечникова, 55, 34",
                products3.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery1.DeliveryCost),
            };

            db.AddRange(orders);
            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            if (db.Orders.FirstOrDefault() != null)
            {
                return;
            }

            var products1 = db.ProductArticles.Skip(0).Take(3).ToList();
            var delivery1 = db.Deliveries.SingleOrDefault(i => i.Name == "Почта России");
            var products2 = db.ProductArticles.Skip(3).Take(3).ToList();
            var delivery2 = db.Deliveries.SingleOrDefault(i => i.Name == "BoxBerry");
            var products3 = db.ProductArticles.Skip(4).Take(3).ToList();
            var delivery3 = db.Deliveries.SingleOrDefault(i => i.Name == "Почта России");

            var orders = new Order[]
            {
                new Order(products1.ToList(),delivery1,
                OrderPaymentMethodType.Cash,
                DateTime.Now,OrderStatusType.Canceled,
                "Россия, Владимирская область, Муром, Мечникова, 55, 34",
                products1.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery1.DeliveryCost),

                new Order(products2.ToList(),delivery2,
                OrderPaymentMethodType.Card,
                DateTime.Now,OrderStatusType.AwaitingProcessing,
                "Россия, Владимирская область, Муром, Московская, 54, 59",
                products2.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery2.DeliveryCost),

                new Order(products3.ToList(),delivery3,
                OrderPaymentMethodType.Card,
                DateTime.Now,OrderStatusType.Packing,
                "Россия, Владимирская область, Муром, Мечникова, 55, 34",
                products3.Aggregate(0.0m,(sum,i) => sum + i.Model.Price) + delivery1.DeliveryCost),
            };

            await db.AddRangeAsync(orders);
            await db.SaveChangesAsync();
        }
    }
}
