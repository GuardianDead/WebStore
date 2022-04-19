using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class CartProductValidator : AbstractValidator<CartProduct>
    {
        public CartProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Article)
                .NotNull().WithMessage("Продукт не может быть пустым")
                .SetValidator(productArticleValidator);
        }
    }
}
