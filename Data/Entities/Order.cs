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
        public int Id { get; private init; }
        [Required]
        [DisplayName("Товары")]
        public IEnumerable<Product> Products { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public Address Address { get; set; }
        [Required]
        [DisplayName("Доставка")]
        public Delivery Delivery { get; set; }
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        public OrderPaymentMethodType OrderPaymentMethodType { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; set; }
        [Required]
        [DisplayName("Статус")]
        public OrderStatusType OrderStatusType { get; set; }
        [Required]
        [DisplayName("Сумма")]
        public decimal SummaryCost { get; set; }

        public Order()
        {
        }
        public Order(IEnumerable<Product> products, Delivery delivery,
            OrderPaymentMethodType paymentMethodType, DateTime dateTimeCreation,
            OrderStatusType statusType, Address address, decimal summaryCost,
            long phoneNumber)
        {
            Products = products;
            Delivery = delivery;
            OrderPaymentMethodType = paymentMethodType;
            DateTimeCreation = dateTimeCreation;
            OrderStatusType = statusType;
            Address = address;
            SummaryCost = summaryCost;
            PhoneNumber = phoneNumber.ToString();
        }
    }
}
