using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Product
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public string Id { get; init; }
        [Required]
        [DisplayName("Артикул")]
        public ProductArticle Article { get; set; }

        public Product()
        {
        }
        public Product(ProductArticle article)
        {
            Id = Guid.NewGuid().ToString("N");
            Article = article;
        }
    }
}
