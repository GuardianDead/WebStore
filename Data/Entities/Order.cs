using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain;

namespace WebStore.Data.Entities
{
    public class Order
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Товары")]
        public virtual List<ProductArticle> ProductArticles { get; set; }
        [Required]
        [DisplayName("Доставка")]
        public virtual Delivery Delivery { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        public OrderPaymentMethodType OrderPaymentMethodType { get; set; }
        [Required]
        [DisplayName("Время")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayName("Статус")]
        public OrderStatusType OrderStatusType { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Сумма")]
        public decimal SummaryCost { get; set; }

        public Order()
        {
        }
        public Order(List<ProductArticle> productArticles, Delivery delivery, OrderPaymentMethodType paymentMethodType,
            DateTime orderDate, OrderStatusType statusType, string address, decimal summaryCost)
        {
            ProductArticles = productArticles;
            Delivery = delivery;
            OrderPaymentMethodType = paymentMethodType;
            OrderDate = orderDate;
            OrderStatusType = statusType;
            Address = address;
            SummaryCost = summaryCost;
        }
    }
}
