using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Pages.ViewModels;
using WebStore.Services;

namespace WebStore.Pages.Account.Authorization
{
    public class LoginBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Parameter] public string ReturnUrl { get; set; }

        [Inject] public IValidator<LoginViewModel> LoginViewModelValidator { get; set; }
        [Inject] public UserManager<User> UserManager { get; set; }
        [Inject] public SignInManager<User> SignInManager { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public TokenAuthenticationStateService TokenAuthenticationStateService { get; set; }

        public bool IsEmailLabelShift { get => !string.IsNullOrWhiteSpace(LoginViewModel.Email); }
        public bool IsPasswordLabelShift { get => !string.IsNullOrWhiteSpace(LoginViewModel.Password); }
        public bool IsPasswordShow { get; set; } = false;
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
        public bool IsEmailInputValid { get; set; } = true;
        public bool IsPasswordInputValid { get; set; } = true;

        public ClaimsPrincipal currentUserState;
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        protected override async Task OnInitializedAsync()
        {
            LoginViewModel.ReturnUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? NavigationManager.BaseUri : ReturnUrl;
            currentUserState = (await AuthenticationStateTask).User;

            if (currentUserState.Identity.IsAuthenticated)
                NavigationManager.NavigateTo(LoginViewModel.ReturnUrl, true);

            //TODO : Сделать авторизацию через ExternalLoging
            //TODO : Сделать LockOut
        }

        public void EmailInputChangeValue(ChangeEventArgs e)
        {
            LoginViewModel.Email = e.Value.ToString();
            IsEmailInputValid = true;
            Errors.RemoveAll(error => error.PropertyName == nameof(LoginViewModel.Email));
        }
        public void PasswordInputChangeValue(ChangeEventArgs e)
        {
            LoginViewModel.Password = e.Value.ToString();
            IsPasswordInputValid = true;
            Errors.RemoveAll(error => error.PropertyName == nameof(LoginViewModel.Password));
        }

        public async Task SubmitAsync(EditContext editContext)
        {
            Errors.Clear();

            bool editContextValidateResult = editContext.Validate();
            if (editContextValidateResult)
                Errors.AddRange(editContext.GetValidationMessages().Select(error => new ValidationFailure("Form", error)));

            ValidationResult validateResult = LoginViewModelValidator.Validate(LoginViewModel);
            if (!validateResult.IsValid)
            {
                IEnumerable<ValidationFailure> emailErrors = validateResult.Errors.Where(error => error.PropertyName == "Email");
                IEnumerable<ValidationFailure> passwordErrors = validateResult.Errors.Where(error => error.PropertyName == "Password");

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
                await LoginAsync();
        }
        public async ValueTask LoginAsync()
        {
            User findedUser = await UserManager.FindByEmailAsync(LoginViewModel.Email);
            if (findedUser is null)
            {
                Errors.Add(new ValidationFailure("Identity", "Данного пользователя не существует"));
                return;
            }
            var checkPassworkResult = await SignInManager.CheckPasswordSignInAsync(findedUser, LoginViewModel.Password, false);
            if (!checkPassworkResult.Succeeded)
            {
                Errors.Add(new ValidationFailure("Identity", "Неверный пароль"));
                return;
            }

            var claimsPrincipal = await SignInManager.CreateUserPrincipalAsync(findedUser);
            if (currentUserState.Identity.IsAuthenticated)
            {
                Errors.Add(new ValidationFailure("Identity", "Данный пользователь уже авторизован в системе"));
                return;
            }

            await TokenAuthenticationStateService.SetAuthenticationStateAsync(new AuthenticationState(claimsPrincipal), LoginViewModel.Remember);
            NavigationManager.NavigateTo(LoginViewModel.ReturnUrl, true);
        }
    }
}
