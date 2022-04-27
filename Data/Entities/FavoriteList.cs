using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class FavoriteList
    {
        [Key]
        [Required]
        [DisplayName("Индификатор")]
        public int Id { get; init; }

        [Required]
        [DisplayName("Товары")]
        public List<FavoriteProduct> Products { get; set; }
        [DisplayName("Пользователь")]
        public User User { get; set; }

        public FavoriteList()
        {
        }
        public FavoriteList(List<FavoriteProduct> products, User user = null)
        {
            Products = products;
            User = user;
        }
    }
}
