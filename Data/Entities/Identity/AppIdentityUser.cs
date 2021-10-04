using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Data.Entities;

namespace WebStore.Data.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        [Key]
        [Required]
        [DisplayName("Номер пользователя")]
        public override Guid Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Firstname { get; set; }
        [Required]
        [DisplayName("Фамилие")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Отчество")]
        public string Lastname { get; set; }
        [Required]
        [DisplayName("Заказы")]
        public List<Order> Orders { get; set; }
        [Required]
        [DisplayName("Избранное")]
        public virtual List<ProductArticle> Favorites { get; set; }
        [Required]
        [DisplayName("Корзина")]
        public virtual List<ProductArticle> Cart { get; set; }
        [Required]
        [NotMapped]
        public string Password { get; set; }

        public AppIdentityUser()
        {
        }
        public AppIdentityUser(string firstname, string surname, string lastname, List<Order> orders,
            List<ProductArticle> favorites, List<ProductArticle> cart, string email, string phoneNumber, string password)
        {
            Id = Guid.NewGuid();
            this.UserName = email.Substring(0, email.IndexOf('@'));
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            Orders = orders;
            Favorites = favorites;
            Cart = cart;
            Password = password;
        }
    }
}
