using FluentValidation;
using FluentValidation.Validators;

namespace WebStore.Pages.ViewModels.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(i => i.Password)
                .NotNull().NotEmpty().WithMessage("Пароль не введен")
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .GreaterThanOrEqualTo(6).WithMessage("Пароль должен быть больше 6 символов"));
            RuleFor(i => i.Email)
                .NotNull().NotEmpty().WithMessage("Электронная почта не введена")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Электронная почта почта неверного формата");
        }
    }
}
