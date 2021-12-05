using FluentValidation;
using FluentValidation.Validators;
using System;
using WebStore.Data.Entities;

namespace WebStore.Validators.Identity
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IValidator<OrderHistory> orderHistoryValidator, IValidator<Cart> cartValidator,
            IValidator<FavoritesList> listFavouritesValidator, IValidator<Address> addressValidator)
        {
            RuleFor(i => i.Firstname)
                .NotNull().NotEmpty().WithMessage("Имя пользователя не может быть пустым");
            RuleFor(i => i.Surname)
                .NotNull().NotEmpty().WithMessage("Фамилия пользователя не может быть пустым");
            RuleFor(i => i.Lastname)
                .NotNull().WithMessage("Отчество пользователя не может быть пустым");
            RuleFor(i => i.DateTimeCreation)
                .NotNull().NotEmpty().WithMessage("Время создания пользователя не может быть пустым");
            RuleFor(i => i.DateOfBirth)
                .NotNull().NotEmpty().WithMessage("Время рождения пользователя не может быть пустым")
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1)).WithMessage("Время рождения пользователя не может быть меньше 1900 года");
            RuleFor(i => i.DateOfBirth)
                .NotNull().NotEmpty().WithMessage("Время рождения пользователя не может быть пустым");
            RuleFor(i => i.Address)
                .NotNull().NotEmpty().WithMessage("Время рождения пользователя не может быть пустым")
                .SetValidator(addressValidator);
            RuleFor(i => i.OrderHistory)
                .NotNull().WithMessage("Заказы не могут быть неопределен")
                .SetValidator(orderHistoryValidator);
            RuleFor(i => i.ListFavourites)
                .NotNull().WithMessage("Избранные товары не могут быть неопределен")
                .SetValidator(listFavouritesValidator);
            RuleFor(i => i.Cart)
                .NotNull().WithMessage("Корзина товаров не может быть неопределен")
                .SetValidator(cartValidator);
            RuleFor(i => i.PhoneNumber)
                .NotNull().WithMessage("Номер телефона не может быть неопределен")
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .ExclusiveBetween(7, 15).WithMessage("Номер телефона должен быть в диапазоне от 7 до 15"));
            RuleFor(i => i.Email)
                .NotNull().NotEmpty().WithMessage("Электронная почта не может быть пуста")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Электронная почта неверного формата");
            RuleFor(i => i.UserName)
                .NotNull().NotEmpty().WithMessage("Логин не может быть пуст");
        }
    }
}
