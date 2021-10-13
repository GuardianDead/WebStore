using Innofactor.EfCoreJsonValueConverter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductSold
    {
        [Key]
        [Required]
        [DisplayName("Номер проданного товара")]
        public Guid Id { get; private init; }
        [Required]
        [DisplayName("Жизнь проданного товара в бд в днях")]
        public int LifeTime { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Заказ")]
        public Order Order { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Продукт")]
        public Product Product { get; set; }

        public ProductSold()
        {
        }
        public ProductSold(Product product, Order order, int lifeTime)
        {
            Id = product.Id;
            Order = order;
            LifeTime = lifeTime;
            Product = product;
        }
    }
}
