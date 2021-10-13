using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validations.Entities
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название категории не может быть пустым");
        }
    }
}
