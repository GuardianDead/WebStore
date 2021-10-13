using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator(IValidator<CartProduct> CartProductValidator)
        {
            RuleFor(i => i.Products)
                .NotNull().WithMessage("Список продуктов не может быть неопределен")
                .ForEach(i => i.SetValidator(CartProductValidator));
        }
    }
}
