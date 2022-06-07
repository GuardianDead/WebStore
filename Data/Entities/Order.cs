using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; private init; }
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        [Display(Name = "Способ оплаты")]
        [EnumDataType(typeof(PaymentMethodType))]
        public PaymentMethodType PaymentMethod { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; set; }
        [Required]
        [DisplayName("Статус")]
        [Display(Name = "Статус")]
        [EnumDataType(typeof(OrderStatusType))]
        public OrderStatusType Status { get; set; }
        [Required]
        [DisplayName("Сумма")]
        public int TotalCost { get; set; }
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

        [Required]
        [Display(AutoGenerateField = false)]
        [DisplayName("Товары")]
        public List<OrderProduct> Products { get; set; }
        [DisplayName("Адрес")]
        [Display(AutoGenerateField = false)]
        public int AddressId { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public Address Address { get; set; }
        [DisplayName("Доставка")]
        [Display(AutoGenerateField = false)]
        public int DeliveryId { get; set; }
        [Required]
        [DisplayName("Доставка")]
        public Delivery Delivery { get; set; }

        public Order()
        {
        }
        public Order(List<OrderProduct> products, Delivery delivery, PaymentMethodType orderPaymentMethodType, DateTime dateTimeCreation, OrderStatusType orderStatusType, Address address, int totalCost, string trackNumber, string email, string customerFullName, string phoneNumber)
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

        public override string ToString() => Id.ToString();
    }
}
