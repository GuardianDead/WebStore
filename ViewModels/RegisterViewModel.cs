using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Запомнить пользователя?")]
        public bool Remember { get; set; }
        [Required]
        [DisplayName("Запомнить пользователя?")]
        public string ReturnUrl { get; set; }

        public RegisterViewModel()
        {
        }
        public RegisterViewModel(string email, string password, bool remember, string returnUrl)
        {
            Email = email;
            Password = password;
            Remember = remember;
            ReturnUrl = returnUrl;
        }
    }
}
