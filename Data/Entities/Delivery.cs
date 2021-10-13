using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain;

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
        public DeliveryMethodType DeliveryMethod { get; set; }
        [Required]
        [DisplayName("Стоимость")]
        public decimal DeliveryCost { get; set; }
        [Required]
        [DisplayName("Примерное время доставки в днях")]
        public int ApproximateDeliveryTime { get; set; }

        public Delivery()
        {
        }
        public Delivery(string name, DeliveryMethodType deliveryMethod,
            decimal deliveryCost, int approximateDeliveryTime)
        {
            Name = name;
            DeliveryMethod = deliveryMethod;
            DeliveryCost = deliveryCost;
            ApproximateDeliveryTime = approximateDeliveryTime;
        }
    }
}
