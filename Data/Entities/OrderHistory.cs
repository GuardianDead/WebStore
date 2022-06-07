using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class OrderHistory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }

        [Required]
        [Display(AutoGenerateField = false)]
        [DisplayName("Заказы")]
        public List<Order> Orders { get; set; }

        public OrderHistory()
        {
        }
        public OrderHistory(List<Order> orders)
        {
            Orders = orders;
        }

        public override string ToString() => Id.ToString();
    }
}
