using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class ProductModelMaterialValidator : AbstractValidator<ProductModelMaterial>
    {
        public ProductModelMaterialValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название материала модели товара не может быть пустым или null");
        }
    }
}
