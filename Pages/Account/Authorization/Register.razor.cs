using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain.Consts;
using WebStore.Services;
using WebStore.ViewModels;

namespace WebStore.Pages.Account.Authorization
{
    public class RegisterBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Parameter] public string ReturnUrl { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public IValidator<RegisterViewModel> RegisterViewModelValidator { get; set; }
        [Inject] public UserManager<User> UserManager { get; set; }
        [Inject] public SignInManager<User> SignInManager { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public TokenAuthenticationStateService TokenAuthenticationStateService { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();
        public ClaimsPrincipal currentUserState;
        public string confirmPassword;
        public string passwordMeterScoreClassName;
        public string passwordLabelScoreWord;

        public bool IsEmailLabelShift
        { get => !string.IsNullOrEmpty(RegisterViewModel.Email); }
        public bool IsPasswordLabelShift { get => !string.IsNullOrEmpty(RegisterViewModel.Password); }
        public bool IsConfirmPasswordLabelShift { get => !string.IsNullOrEmpty(confirmPassword); }
        public bool IsPasswordShow { get; set; }
        public bool IsConfirmPasswordShow { get; set; }
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
        public bool IsEmailInputValid { get; set; } = true;
        public bool IsPasswordInputValid { get; set; } = true;
        public bool IsConfirmPasswordInputValid { get; set; } = true;
        public bool PasswordMeterIsShow { get; set; }
        public bool PasswordLabelIsShow { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RegisterViewModel.ReturnUrl = string.IsNullOrEmpty(ReturnUrl) ? NavigationManager.BaseUri : ReturnUrl.Replace('$', '/'); // Костыль, требуется заменить символы '/' на '$' в возвращаемом URL адресе при его отправке и наоборот
            currentUserState = (await AuthenticationStateTask).User;

            if (currentUserState.Identity.IsAuthenticated)
                NavigationManager.NavigateTo(RegisterViewModel.ReturnUrl);

            //TODO : Сделать регистрацию через ExternalLoging
            //TODO : Сделать LockOut
        }

        public void NavigateToLogin() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/login/{RegisterViewModel.ReturnUrl.Replace('/', '$')}", true);

        public void EmailInputChangeValue(ChangeEventArgs e)
        {
            RegisterViewModel.Email = e.Value.ToString();
            IsEmailInputValid = true;
            Errors.RemoveAll(error => error.PropertyName == nameof(RegisterViewModel.Email));
        }
        public async Task PasswordInputChangeValueAsync(ChangeEventArgs e)
        {
            RegisterViewModel.Password = e.Value.ToString();
            IsPasswordInputValid = true;
            Errors.RemoveAll(error => error.PropertyName == nameof(RegisterViewModel.Password));
            if (string.IsNullOrEmpty(RegisterViewModel.Password))
            {
                PasswordMeterIsShow = false;
                PasswordLabelIsShow = false;
            }
            else
            {
                PasswordMeterIsShow = true;
                PasswordLabelIsShow = true;
                var scorePassword = await JSRuntime.InvokeAsync<int>("getScorePassword", RegisterViewModel.Password);
                if (scorePassword <= 100)
                {
                    passwordMeterScoreClassName = "strong";
                    passwordLabelScoreWord = "Сильный";
                }
                if (scorePassword <= 75)
                {
                    passwordMeterScoreClassName = "high-medium";
                    passwordLabelScoreWord = "Выше среднего";
                }
                if (scorePassword <= 50)
                {
                    passwordMeterScoreClassName = "medium";
                    passwordLabelScoreWord = "Средний";
                }
                if (scorePassword <= 25)
                {
                    passwordMeterScoreClassName = "simple";
                    passwordLabelScoreWord = "Слабый";
                }
            }
            StateHasChanged();
        }
        public void ConfirmPasswordInputChangeValue(ChangeEventArgs e)
        {
            confirmPassword = e.Value.ToString();
            IsConfirmPasswordInputValid = true;
            Errors.RemoveAll(error => error.PropertyName == nameof(confirmPassword));
        }

        public async Task SubmitAsync(EditContext editContext)
        {
            Errors.Clear();

            if (string.IsNullOrEmpty(confirmPassword))
            {
                Errors.Add(new ValidationFailure(nameof(confirmPassword), "Подтвердите пароль"));
                IsConfirmPasswordInputValid = false;
                return;
            }

            bool editContextValidateResult = editContext.Validate();
            if (editContextValidateResult)
                Errors.AddRange(editContext.GetValidationMessages().Select(error => new ValidationFailure("Form", error)));

            ValidationResult validateResult = RegisterViewModelValidator.Validate(RegisterViewModel);
            if (!validateResult.IsValid)
            {
                IEnumerable<ValidationFailure> emailErrors = validateResult.Errors.Where(error => error.PropertyName == "Email");
                IEnumerable<ValidationFailure> passwordErrors = validateResult.Errors.Where(error => error.PropertyName == "Password" || error.PropertyName == "Password.Length");

                if (emailErrors.Any())
                {
                    Errors.AddRange(emailErrors);
                    IsEmailInputValid = false;
                }
                if (passwordErrors.Any())
                {
                    Errors.AddRange(passwordErrors);
                    IsPasswordInputValid = false;
                }
            }

            if (validateResult.IsValid && editContextValidateResult)
                await RegisterAsync();
        }
        public async ValueTask RegisterAsync()
        {
            if (!RegisterViewModel.Password.SequenceEqual(confirmPassword))
            {
                Errors.Add(new ValidationFailure("Identity", "Пароли не совпадают"));
                IsConfirmPasswordInputValid = false;
                return;
            }

            User findedUser = await UserManager.FindByEmailAsync(RegisterViewModel.Email);
            if (findedUser is not null)
            {
                Errors.Add(new ValidationFailure("Identity", "Такой пользователь уже зарегистрирован"));
                IsEmailInputValid = false;
                return;
            }

            var сlaims = new Claim[]
            {
                new Claim("DateTimeCreation", DateTime.Now.ToString("dd.MM.yyyy"), ClaimValueTypes.DateTime)
            };
            var createdUser = new User(
                    orderHistory: new OrderHistory(new List<Order>()),
                    listFavourites: new FavoritesList(new List<FavoritesListProduct>()),
                    cart: new Data.Entities.Cart(new List<CartProduct>()),
                    email: RegisterViewModel.Email,
                    dateTimeCreation: DateTime.Now,
                    userName: RegisterViewModel.Email[..RegisterViewModel.Email.IndexOf('@')]
                );
            await UserManager.CreateAsync(createdUser, RegisterViewModel.Password);
            await UserManager.AddToRoleAsync(createdUser, RoleConst.User);
            await UserManager.AddClaimsAsync(createdUser, сlaims);
            var claimsPrincipal = await SignInManager.CreateUserPrincipalAsync(createdUser);

            await TokenAuthenticationStateService.SetAuthenticationStateAsync(new AuthenticationState(claimsPrincipal), RegisterViewModel.Remember);
            NavigationManager.NavigateTo(RegisterViewModel.ReturnUrl, true);
        }
    }
}
