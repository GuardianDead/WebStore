using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validations.Entities
{
    public class ProductSubcategoryValidator : AbstractValidator<ProductSubcategory>
    {
        public ProductSubcategoryValidator(IValidator<ProductCategory> productCategoryValidator)
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название подкатегории не может быть пустым");
            RuleFor(i => i.Category)
                .SetValidator(productCategoryValidator);
        }
    }
}
