﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Pages.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Запомнить пользователя?")]
        public bool Remember { get; set; }
        [DisplayName("Запомнить пользователя?")]
        public string ReturnUrl { get; set; }

        public LoginViewModel()
        {
        }
        public LoginViewModel(string email, string password, bool remember, string returnUrl)
        {
            Email = email;
            Password = password;
            Remember = remember;
            ReturnUrl = returnUrl;
        }
    }
}
