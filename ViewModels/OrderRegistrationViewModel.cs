using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Data.Entities;
using WebStore.Domain.Types;

namespace WebStore.ViewModels
{
    public class OrderRegistrationViewModel
    {
        [Required]
        [DisplayName("ФИО")]
        public string FullName { get; set; }
        [Required]
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Адрес доставки")]
        public Address Address { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        [EnumDataType(typeof(PaymentMethodType))]
        public PaymentMethodType? PaymentMethodType { get; set; }
        [Required]
        [DisplayName("Способ оплаты")]
        [EnumDataType(typeof(DeliveryMethodType))]
        public DeliveryMethodType? DeliveryMethod { get; set; }
        [Required]
        [DisplayName("Доставка")]
        public Delivery Delivery { get; set; }

        public OrderRegistrationViewModel()
        {
        }
        public OrderRegistrationViewModel(string fullName, string phoneNumber, string email,
            Address address, PaymentMethodType paymentMethodType, Delivery delivery)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            PaymentMethodType = paymentMethodType;
            Delivery = delivery;
        }
    }
}
