using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class FavoriteProduct
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }

        [DisplayName("Артикул")]
        [Display(AutoGenerateField = false)]
        public string ArticleId { get; set; }
        [Required]
        [DisplayName("Артикул")]
        public ProductArticle Article { get; set; }

        public FavoriteProduct()
        {
        }
        public FavoriteProduct(ProductArticle article)
        {
            Article = article;
        }

        public override string ToString() => $"{Id} - {Article.Model.Name}";
    }
}
