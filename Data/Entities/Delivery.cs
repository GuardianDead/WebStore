using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class Delivery
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Способ доставки")]
        [Display(Name = "Способ доставки")]
        [EnumDataType(typeof(DeliveryMethodType))]
        public DeliveryMethodType DeliveryMethod { get; set; }
        [Required]
        [DisplayName("Стоимость")]
        public int Cost { get; set; }
        [Required]
        [DisplayName("Примерное время доставки (в днях)")]
        public int ApproximateDays { get; set; }

        public Delivery()
        {
        }
        public Delivery(string name, DeliveryMethodType deliveryMethod, int cost, int approximateDays)
        {
            Name = name;
            DeliveryMethod = deliveryMethod;
            Cost = cost;
            ApproximateDays = approximateDays;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
