using FluentValidation;
using FluentValidation.Validators;
using WebStore.Data.Entities;

namespace WebStore.ViewModels.Validators
{
    public class OrderRegistrationViewModelValidator : AbstractValidator<OrderRegistrationViewModel>
    {
        public OrderRegistrationViewModelValidator(IValidator<Address> addressValidator, IValidator<Delivery> deliveryValidator)
        {
            RuleFor(p => p.FullName)
                .NotNull().NotEmpty().WithMessage("ФИО не заполнен");
            RuleFor(p => p.Email)
                .NotNull().WithMessage("Электронная почта не введена")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Электронная почта почта неверного формата");
            RuleFor(p => p.PaymentMethodType)
                .NotNull().WithMessage("Метод оплаты не выбран")
                .IsInEnum();
            RuleFor(p => p.DeliveryMethod)
                .NotNull().WithMessage("Способ доставки не выбран")
                .IsInEnum();
            RuleFor(p => p.Delivery)
                .NotNull().WithMessage("Доставка не выбрана")
                .SetValidator(deliveryValidator);
            RuleFor(p => p.Address)
                .SetValidator(addressValidator);
        }
    }
}
