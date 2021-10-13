using FluentValidation;
using System;
using WebStore.Data.Entities;
using WebStore.Data.Identity;

namespace WebStore.Validators.Identity
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IValidator<OrderHistory> orderHistoryValidator, IValidator<Cart> cartValidator,
            IValidator<FavoritesList> listFavouritesValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Id пользователя не может быть пустым");
            RuleFor(i => i.Firstname)
                .NotNull().NotEmpty().WithMessage("Имя пользователя не может быть пустым");
            RuleFor(i => i.Surname)
                .NotNull().NotEmpty().WithMessage("Фамилия пользователя не может быть пустым");
            RuleFor(i => i.Lastname)
                .NotNull().WithMessage("Отчество пользователя не может быть пустым");
            RuleFor(i => i.DateTimeCreation)
                .NotNull().NotEmpty().WithMessage("Время создания пользователя не может быть пустым")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Время создания пользователя не может быть в прошедшем времени");
            RuleFor(i => i.OrderHistory)
                .NotNull().WithMessage("Заказы не могут быть неопределен")
                .SetValidator(orderHistoryValidator);
            RuleFor(i => i.ListFavourites)
                .NotNull().WithMessage("Избранные товары не могут быть неопределен")
                .SetValidator(listFavouritesValidator);
            RuleFor(i => i.Cart)
                .NotNull().WithMessage("Корзина товаров не может быть неопределен")
                .SetValidator(cartValidator);
            RuleFor(i => i.Password)
                .NotNull().NotEmpty().WithMessage("Пароль не может быть пустым")
                .ChildRules(i => i.RuleFor(i => i.Length)
                                    .GreaterThanOrEqualTo(6).WithMessage("Пароль должен быть больше 6 символов"));
            RuleFor(i => i.PhoneNumber)
                .NotNull().WithMessage("Номер телефона не может быть неопределен")
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .ExclusiveBetween(7, 15).WithMessage("Номер телефона должен быть в диапазоне от 7 до 15"));
            RuleFor(i => i.Email)
                .NotNull().NotEmpty().WithMessage("Электронная почта не может быть пуста")
                .EmailAddress()
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .GreaterThanOrEqualTo(6).WithMessage("Длина электронный почты должна быть минимум 6"));
            RuleFor(i => i.UserName)
                .NotNull().NotEmpty().WithMessage("Логин не может быть пуст")
                .Must((user, userName) => userName == user.Email.Substring(user.Email.IndexOf('@')))
                .WithMessage("Логин и название почты не совпадают");
        }
    }
}
