using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class FavoritesListProductValidator : AbstractValidator<FavoritesListProduct>
    {
        public FavoritesListProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.ProductArticle)
                .NotEmpty().NotNull().WithMessage("Продукт не может быть пустым")
                .SetValidator(productArticleValidator);
        }
    }
}
