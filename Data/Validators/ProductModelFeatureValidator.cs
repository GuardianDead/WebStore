using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class ProductModelFeatureValidator : AbstractValidator<ProductModelFeature>
    {
        public ProductModelFeatureValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название характеристики модели товара не может быть пустым или null");
            RuleFor(i => i.Value)
                .NotNull().NotEmpty().WithMessage("Значение характеристики модели товара не может быть пустым или null");
        }
    }
}
