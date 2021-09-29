using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductSoldValidator : AbstractValidator<ProductSold>
    {
        public ProductSoldValidator(IValidator<Order> orderValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер проданного товара не может быть пустым");
            RuleFor(i => i.LifeTime)
                .NotNull().WithMessage("Количество дней хранения проданного товара в бд не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Количество дней хранения проданного товара в бд должно быть больше 1");
            RuleFor(i => i.Order)
                .NotNull().WithMessage("Заказ не может быть пустым")
                .SetValidator(orderValidator);
        }
    }
}
