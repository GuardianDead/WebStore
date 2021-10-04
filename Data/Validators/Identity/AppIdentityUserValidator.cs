using FluentValidation;
using WebStore.Data.Entities;
using WebStore.Data.Identity;

namespace WebStore.Validators.Identity
{
    public class AppIdentityUserValidator :  AbstractValidator<AppIdentityUser>
    {
        public AppIdentityUserValidator(IValidator<Order> orderValidator, IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Id пользователя не может быть пустым");
            RuleFor(i => i.Firstname)
                .NotNull().NotEmpty().WithMessage("Имя пользователя не может быть пустым");
            RuleFor(i => i.Surname)
                .NotNull().NotEmpty().WithMessage("Фамилия пользователя не может быть пустым");
            RuleFor(i => i.Lastname)
                .NotNull().WithMessage("Отчество пользователя не может быть пустым");
            RuleFor(i => i.Orders)
                .NotNull().WithMessage("Заказы не могут быть пустыми")
                .ForEach(i => i.SetValidator(orderValidator));
            RuleFor(i => i.Favorites)
                .NotNull().WithMessage("Избранные товары не могут быть пустыми")
                .ForEach(i => i.SetValidator(productArticleValidator));
            RuleFor(i => i.Cart)
                .NotNull().WithMessage("Корзина товаров не может быть пустым")
                .ForEach(i => i.SetValidator(productArticleValidator));
            RuleFor(i => i.Password.Length)
                .NotNull().NotEmpty().WithMessage("Пароль не может быть пустым")
                .GreaterThanOrEqualTo(6).WithMessage("Пароль должен быть больше 6 символов");
        }
    }
}
