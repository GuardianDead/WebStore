using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class FavoritesListProductValidator : AbstractValidator<FavoriteProduct>
    {
        public FavoritesListProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Article)
                .NotEmpty().NotNull().WithMessage("Артикул продукта не может быть пустым")
                .SetValidator(productArticleValidator);
        }
    }
}
