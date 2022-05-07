using Innofactor.EfCoreJsonValueConverter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    [ReadOnly(true)]
    public class OrderProduct
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Срок хранения в бд")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [JsonField]
        [DisplayName("Товар")]
        public Product Product { get; init; }

        public OrderProduct()
        {
        }
        public OrderProduct(Product product)
        {
            ExpirationDate = DateTime.Now.AddDays(product.Article.Model.DaysGuarantee);
            Product = product;
        }

        public override string ToString()
        {
            return $"{Product.Id} - {Product.Article.Model.Name}";
        }
    }
}
