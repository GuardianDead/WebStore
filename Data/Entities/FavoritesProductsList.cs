using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class FavoritesProductsList
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Товары")]
        public List<FavoriteProduct> Products { get; set; }

        public FavoritesProductsList()
        {
        }
        public FavoritesProductsList(List<FavoriteProduct> products)
        {
            Products = products;
        }
    }
}
