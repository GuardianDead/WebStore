using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class CartProductValidator : AbstractValidator<CartProduct>
    {
        public CartProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Count)
                .GreaterThanOrEqualTo(1).WithMessage("Количество товара не может быть меньше 1")
                .Must((cartProduct, productCount) => cartProduct.ProductArticle.Count >= productCount).WithMessage("Такого количества товара нет на складе");
            RuleFor(i => i.ProductArticle)
                .NotNull().WithMessage("Продукт не может быть пустым")
                .SetValidator(productArticleValidator);
        }
    }
}
