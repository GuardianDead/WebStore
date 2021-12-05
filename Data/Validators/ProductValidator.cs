using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator(IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Article)
                .NotNull().WithMessage("Артикул товара не может быть пустым")
                .SetValidator(productArticleValidator);
        }
    }
}
