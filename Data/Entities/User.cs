using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Data.Entities;

namespace WebStore.Data.Identity
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
        [DisplayName("История заказов")]
        public OrderHistory OrderHistory { get; set; }
        [Required]
        [DisplayName("Избранное")]
        public FavoritesList ListFavourites { get; set; }
        [Required]
        [DisplayName("Корзина")]
        public Cart Cart { get; set; }
        [Required]
        [NotMapped]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
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
        public User()
        {
        }
        public User(string firstname, string surname, string lastname,
            OrderHistory orderHistory, FavoritesList listFavourites,
            Cart cart, string email, string password,
            DateTime dateTimeCreation, long phoneNumber)
        {
            this.UserName = email.Substring(0, email.IndexOf('@'));
            this.PhoneNumber = phoneNumber.ToString();
            this.Email = email;
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            OrderHistory = orderHistory;
            ListFavourites = listFavourites;
            Cart = cart;
            Password = password;
            DateTimeCreation = dateTimeCreation;
        }
    }
}
