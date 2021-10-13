using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validations.Entities
{
    public class SubcategoryValidator : AbstractValidator<Subcategory>
    {
        public SubcategoryValidator(IValidator<Category> productCategoryValidator)
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название подкатегории не может быть пустым");
            RuleFor(i => i.Category)
                .SetValidator(productCategoryValidator);
        }
    }
}
