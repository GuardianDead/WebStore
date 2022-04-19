using FluentValidation;
using System;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductSoldValidator : AbstractValidator<SoldProduct>
    {
        public ProductSoldValidator(IValidator<Order> orderValidator, IValidator<Product> productValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер проданного товара не может быть пустым");
            RuleFor(i => i.ExpirationDate)
                .GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(i => i.Order)
                .NotNull().WithMessage("Заказ не может быть пустым")
                .SetValidator(orderValidator);
            RuleFor(i => i.Product)
                .NotNull().WithMessage("Проданный продукт не может быть пустым")
                .SetValidator(productValidator);
        }
    }
}
