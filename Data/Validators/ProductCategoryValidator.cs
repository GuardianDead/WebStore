using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validations.Entities
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название категории не может быть пустым");
        }
    }
}
