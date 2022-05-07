using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Cart
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }

        [Required]
        [Display(AutoGenerateField = false)]
        [DisplayName("Товары")]
        public List<CartProduct> Products { get; set; }

        public Cart()
        {
        }
        public Cart(List<CartProduct> products)
        {
            Products = products;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
