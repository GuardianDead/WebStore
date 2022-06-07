using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class FavoriteList
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }

        [Required]
        [Display(AutoGenerateField = false)]
        [DisplayName("Товары")]
        public List<FavoriteProduct> Products { get; set; }

        public FavoriteList()
        {
        }
        public FavoriteList(List<FavoriteProduct> products)
        {
            Products = products;
        }

        public override string ToString() => Id.ToString();
    }
}
