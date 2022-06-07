using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Product
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public string Id { get; init; }
        [Required]
        [DisplayName("Товар продан?")]
        public bool IsSold { get; set; }

        [DisplayName("Артикул")]
        [Display(AutoGenerateField = false)]
        public string ArticleId { get; set; }
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

        public override string ToString() => Id;
    }
}
