using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain;

namespace WebStore.Data.Mocks
{
    public class DeliveriesMock
    {
        private static readonly Delivery[] deliveries =
        {
            new Delivery("Почта России",DeliveryMethodType.Post,300,5),
            new Delivery("BoxBerry", DeliveryMethodType.Post, 400, 3),
            new Delivery("СДЭК", DeliveryMethodType.Post, 300, 4),
            new Delivery("Доставка Veco", DeliveryMethodType.Post, 400, 3),

            new Delivery("Магазин Veco", DeliveryMethodType.Pickup, 0, 0),

            new Delivery("DPD", DeliveryMethodType.Courier, 400, 3),
            new Delivery("Курьерская доставка Veco", DeliveryMethodType.Courier, 500, 3),
        };

        public static void Init(AppDbContext db)
        {
            if(db.Deliveries.FirstOrDefault() != null)
            {
                return;
            }

            db.Deliveries.AddRange(deliveries);
            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            if (db.Deliveries.FirstOrDefault() != null)
            {
                return;
            }
            await db.Deliveries.AddRangeAsync(deliveries);
            await db.SaveChangesAsync();
        }
    }
}
