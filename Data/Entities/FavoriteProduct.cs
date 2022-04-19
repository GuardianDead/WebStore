using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class FavoriteProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
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
    }
}
