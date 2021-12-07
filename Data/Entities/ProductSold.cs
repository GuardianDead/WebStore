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
        [DisplayName("Срок жизни товара в базе данных (в днях)")]
        public int DaysLifeTime { get; set; }
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
        public ProductSold(Product product, Order order, int daysLifeTime)
        {
            Id = product.Id;
            Order = order;
            DaysLifeTime = daysLifeTime;
            Product = product;
        }
    }
}
