using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class User : IdentityUser<int>
    {
        [Key]
        [Required]
        [DisplayName("Индификатор пользователя")]
        public override int Id { get; set; }
        [DisplayName("Имя")]
        public string Firstname { get; set; }
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [DisplayName("Отчество")]
        public string Lastname { get; set; }
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Гендер")]
        [EnumDataType(typeof(GenderType))]
        public GenderType? Gender { get; set; }
        [Required]
        [DisplayName("История заказов")]
        public OrderHistory OrderHistory { get; set; }
        [Required]
        [DisplayName("Избранное")]
        public FavoritesList ListFavourites { get; set; }
        [Required]
        [DisplayName("Корзина")]
        public Cart Cart { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; private init; }
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public override string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public override string Email { get; set; }
        [Required]
        [DisplayName("Логин")]
        public override string UserName { get; set; }
        [DisplayName("Адрес")]
        public Address Address { get; set; }

        public User()
        {

        }
        public User(OrderHistory orderHistory, FavoritesList listFavourites,
            Cart cart, string email, DateTime dateTimeCreation, string userName)
        {
            this.UserName = userName;
            this.Email = email;
            Cart = cart;
            OrderHistory = orderHistory;
            ListFavourites = listFavourites;
            DateTimeCreation = dateTimeCreation;
        }
    }
}
