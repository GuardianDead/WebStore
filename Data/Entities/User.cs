using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class User : IdentityUser<int>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public override int Id { get; set; }
        [DisplayName("Имя")]
        public string Firstname { get; set; }
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [DisplayName("Отчество")]
        public string Lastname { get; set; }
        [DisplayName("Дата рождения")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Пол")]
        [Display(Name = "Пол")]
        [EnumDataType(typeof(GenderType))]
        public GenderType? Gender { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; init; }
        [DisplayName("Окончание блокировки")]
        [Display(Name = "Окончание блокировки")]
        public override DateTimeOffset? LockoutEnd { get; set; }
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

        [DisplayName("История заказов")]
        [Display(AutoGenerateField = false)]
        public int OrderHistoryId { get; set; }
        [Required]
        [DisplayName("История заказов")]
        public OrderHistory OrderHistory { get; set; }
        [DisplayName("Избранное")]
        [Display(AutoGenerateField = false)]
        public int FavoriteListId { get; set; }
        [Required]
        [DisplayName("Избранное")]
        public FavoriteList FavoriteList { get; set; }
        [DisplayName("Корзины")]
        [Display(AutoGenerateField = false)]
        public int CartId { get; set; }
        [Required]
        [DisplayName("Корзина")]
        public Cart Cart { get; set; }
        [DisplayName("Адрес")]
        [Display(AutoGenerateField = false)]
        public int? AddressId { get; set; }
        [DisplayName("Адрес")]
        public Address? Address { get; set; }

        public User()
        {

        }
        public User(OrderHistory orderHistory, FavoriteList favoriteList, Cart cart, string email, DateTime dateTimeCreation, string userName)
        {
            UserName = userName;
            Email = email;
            Cart = cart;
            OrderHistory = orderHistory;
            FavoriteList = favoriteList;
            DateTimeCreation = dateTimeCreation;
        }
    }
}
