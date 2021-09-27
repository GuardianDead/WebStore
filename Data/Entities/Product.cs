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
        public Guid Id { get; private init; }
        [Required]
        [DisplayName("Артикул")]
        public virtual ProductArticle Article { get; set; }

        public Product()
        {
        }
        public Product(ProductArticle article)
        {
            Id = Guid.NewGuid();
            Article = article;
        }
    }
}
