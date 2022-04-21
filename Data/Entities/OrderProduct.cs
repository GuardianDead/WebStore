using Innofactor.EfCoreJsonValueConverter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class OrderProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Срок хранения в бд")]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Товар")]
        public Product Product { get; set; }

        public OrderProduct()
        {
        }
        public OrderProduct(Product product)
        {
            ExpirationDate = DateTime.Now.AddDays(product.Article.Model.DaysGuarantee);
            Product = product;
        }
    }
}
