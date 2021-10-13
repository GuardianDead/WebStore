using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Cart
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Товары")]
        public IEnumerable<CartProduct> Products { get; set; }

        public Cart()
        {
        }
        public Cart(IEnumerable<CartProduct> products)
        {
            Products = products;
        }
    }
}
