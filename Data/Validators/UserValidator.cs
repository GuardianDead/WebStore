using FluentValidation;
using FluentValidation.Validators;
using WebStore.Data.Entities;

namespace WebStore.Validators.Identity
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IValidator<OrderHistory> orderHistoryValidator, IValidator<Cart> cartValidator,
            IValidator<FavoritesProductsList> listFavouritesValidator, IValidator<Address> addressValidator)
        {
            RuleFor(i => i.Address)
                .SetValidator(addressValidator);
            RuleFor(i => i.OrderHistory)
                .NotNull().WithMessage("Заказы не могут быть неопределены")
                .SetValidator(orderHistoryValidator);
            RuleFor(i => i.ListFavourites)
                .NotNull().WithMessage("Избранные товары не могут быть неопределены")
                .SetValidator(listFavouritesValidator);
            RuleFor(i => i.Cart)
                .NotNull().WithMessage("Корзина товаров не может быть неопределена")
                .SetValidator(cartValidator);
            RuleFor(i => i.PhoneNumber)
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .ExclusiveBetween(7, 15).When(value => !string.IsNullOrEmpty(value)).WithMessage("Номер телефона должен быть в диапазоне от 7 до 15"));
            RuleFor(i => i.Email)
                .NotNull().NotEmpty().WithMessage("Электронная почта не может быть пуста")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Электронная почта неверного формата");
            RuleFor(i => i.UserName)
                .NotNull().NotEmpty().WithMessage("Логин не может быть пуст");
        }
    }
}
