using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class CartProduct
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [Required]
        [DisplayName("Товар выбран?")]
        public bool IsSelected { get; set; }

        [DisplayName("Артикул")]
        [Display(AutoGenerateField = false)]
        public string ArticleId { get; set; }
        [Required]
        [DisplayName("Артикул")]
        public ProductArticle Article { get; set; }

        public CartProduct()
        {
        }
        public CartProduct(ProductArticle article, int count)
        {
            Article = article;
            Count = count;
        }

        public override string ToString() => $"{Id} - {Article.Model.Name}";
    }
}
