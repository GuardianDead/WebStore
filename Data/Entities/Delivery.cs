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
        public int Id { get; private init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Способ")]
        [EnumDataType(typeof(DeliveryMethodType))]
        [DataType(DataType.Text)]
        public DeliveryMethodType DeliveryMethod { get; set; }
        [Required]
        [DisplayName("Стоимость")]
        [DataType(DataType.Currency)]
        public decimal DeliveryCost { get; set; }
        [Required]
        [DisplayName("Примерное время доставки (в днях)")]
        public int ApproximateDaysDelivery { get; set; }

        public Delivery()
        {
        }
        public Delivery(string name, DeliveryMethodType deliveryMethod,
            decimal deliveryCost, int approximateDaysDelivery)
        {
            Name = name;
            DeliveryMethod = deliveryMethod;
            DeliveryCost = deliveryCost;
            ApproximateDaysDelivery = approximateDaysDelivery;
        }
    }
}
