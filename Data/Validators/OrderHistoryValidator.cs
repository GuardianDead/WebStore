using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class OrderHistoryValidator : AbstractValidator<OrderHistory>
    {
        public OrderHistoryValidator(IValidator<Order> orderValidator)
        {
            RuleFor(i => i.Orders)
                .NotNull().WithMessage("История заказа не может быть неопределен")
                .ForEach(i => i.SetValidator(orderValidator));
        }
    }
}
