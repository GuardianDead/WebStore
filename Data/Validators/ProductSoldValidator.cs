using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductSoldValidator : AbstractValidator<ProductSold>
    {
        public ProductSoldValidator(IValidator<Order> orderValidator, IValidator<Product> productValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер проданного товара не может быть пустым");
            RuleFor(i => i.DaysLifeTime)
                .NotNull().WithMessage("Количество дней хранения проданного товара в бд не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Значение хранения проданного товара в бд должно быть больше или равно 0");
            RuleFor(i => i.Order)
                .NotNull().WithMessage("Заказ не может быть пустым")
                .SetValidator(orderValidator);
            RuleFor(i => i.Product)
                .NotNull().WithMessage("Проданный продукт не может быть пустым")
                .SetValidator(productValidator);
        }
    }
}
