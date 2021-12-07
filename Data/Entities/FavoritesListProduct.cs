using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class FavoritesListProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Товар")]
        public ProductArticle ProductArticle { get; set; }

        public FavoritesListProduct()
        {
        }
        public FavoritesListProduct(ProductArticle productArticle)
        {
            ProductArticle = productArticle;
        }
    }
}
