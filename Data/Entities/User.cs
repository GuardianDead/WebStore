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
        [DisplayName("Номер пользователя")]
        public override int Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Firstname { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Отчество")]
        public string Lastname { get; set; }
        [Required]
        [DisplayName("Дата рождения")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DisplayName("Гендер")]
        public UserGenderType Gender { get; set; }
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
        public DateTime DateTimeCreation { get; set; }
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public override string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
        [Required]
        [DisplayName("Логин")]
        public override string UserName { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public Address Address { get; set; }

        public User()
        {
        }
        public User(string firstname, string surname, string lastname,
            OrderHistory orderHistory, FavoritesList listFavourites,
            Cart cart, string email, DateTime dateTimeCreation, string phoneNumber,
            UserGenderType gender, DateTime dateOfBirth, string userName, Address address)
        {
            this.UserName = userName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            Address = address;
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            OrderHistory = orderHistory;
            ListFavourites = listFavourites;
            Cart = cart;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            DateTimeCreation = dateTimeCreation;
        }
    }
}
