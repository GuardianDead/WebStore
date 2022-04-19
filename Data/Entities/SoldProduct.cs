using Innofactor.EfCoreJsonValueConverter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class SoldProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Дата жизни в базе данных")]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Заказ")]
        public Order Order { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Продукт")]
        public Product Product { get; set; }

        public SoldProduct()
        {
        }
        public SoldProduct(Product product, Order order)
        {
            Order = order;
            ExpirationDate = DateTime.Now.AddDays(product.Article.Model.DaysGuarantee);
            Product = product;
        }
    }
}
