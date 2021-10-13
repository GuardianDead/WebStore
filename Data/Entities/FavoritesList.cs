using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class FavoritesList
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Товары")]
        public IEnumerable<FavoritesListProduct> Products { get; set; }

        public FavoritesList()
        {
        }
        public FavoritesList(IEnumerable<FavoritesListProduct> products)
        {
            Products = products;
        }
    }
}
