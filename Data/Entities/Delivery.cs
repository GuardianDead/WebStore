using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class Delivery
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Способ доставки")]
        [EnumDataType(typeof(DeliveryMethodType))]
        public DeliveryMethodType DeliveryMethod { get; set; }
        [Required]
        [DisplayName("Стоимость")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        [Required]
        [DisplayName("Примерное время доставки (в днях)")]
        public int ApproximateDays { get; set; }

        public Delivery()
        {
        }
        public Delivery(string name, DeliveryMethodType deliveryMethod, decimal cost, int approximateDays)
        {
            Name = name;
            DeliveryMethod = deliveryMethod;
            Cost = cost;
            ApproximateDays = approximateDays;
        }
    }
}
