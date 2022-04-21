using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Types;

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
        public List<OrderProduct> Products { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public Address Address { get; set; }
        [Required]
        [DisplayName("Доставка")]
        public Delivery Delivery { get; set; }
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        [EnumDataType(typeof(PaymentMethodType))]
        public PaymentMethodType PaymentMethod { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; set; }
        [Required]
        [DisplayName("Статус")]
        [EnumDataType(typeof(OrderStatusType))]
        public OrderStatusType Status { get; set; }
        [Required]
        [DisplayName("Сумма")]
        public decimal TotalCost { get; set; }
        [Required]
        [DisplayName("Трек номер")]
        public string TrackNumber { get; set; }
        [Required]
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("ФИО заказчика")]
        public string CustomerFullName { get; set; }

        public Order()
        {
        }
        public Order(List<OrderProduct> products, Delivery delivery,
            PaymentMethodType orderPaymentMethodType, DateTime dateTimeCreation,
            OrderStatusType orderStatusType, Address address, decimal totalCost,
            string trackNumber, string email, string customerFullName, string phoneNumber)
        {
            Products = products;
            Delivery = delivery;
            PaymentMethod = orderPaymentMethodType;
            DateTimeCreation = dateTimeCreation;
            Status = orderStatusType;
            Address = address;
            TotalCost = totalCost;
            TrackNumber = trackNumber;
            Email = email;
            CustomerFullName = customerFullName;
            PhoneNumber = phoneNumber;
        }
    }
}
