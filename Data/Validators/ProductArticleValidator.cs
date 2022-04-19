using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductArticleValidator : AbstractValidator<ProductArticle>
    {
        public ProductArticleValidator(IValidator<ProductModel> productModelValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер артикула товара не может быть пустым");
            RuleFor(i => i.Color)
                .NotNull().NotEmpty().WithMessage("Цвет артикула товара не может быть пустым");
            RuleFor(i => i.Model)
                .NotNull().WithMessage("Модель артикула товара не может быть пустым")
                .SetValidator(productModelValidator);
            RuleFor(i => i.Size)
                .NotNull().NotEmpty().WithMessage("Размер артикула товара не может быть пустым")
                .GreaterThanOrEqualTo(1).WithMessage("Размер товара должен быть больше или равен 1");
        }
    }
}
