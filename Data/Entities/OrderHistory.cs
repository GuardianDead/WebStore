using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class OrderHistory
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Заказы")]
        public IEnumerable<Order> Orders { get; set; }

        public OrderHistory()
        {
        }
        public OrderHistory(IEnumerable<Order> orders)
        {
            Orders = orders;
        }
    }
}
