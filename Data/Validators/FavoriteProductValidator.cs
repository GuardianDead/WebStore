using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class FavoriteProductValidator : AbstractValidator<FavoriteProduct>
    {
        public FavoriteProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Article)
                .NotNull().WithMessage("Артикул не может быть null")
                .SetValidator(productArticleValidator);
        }
    }
}
