using FluentValidation;
using System;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class OrderProductValidator : AbstractValidator<OrderProduct>
    {
        public OrderProductValidator(IValidator<Product> productValidator)
        {
            RuleFor(i => i.ExpirationDate)
                .GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(i => i.Product)
                .NotNull().WithMessage("Проданный продукт не может быть пустым")
                .SetValidator(productValidator);
        }
    }
}
