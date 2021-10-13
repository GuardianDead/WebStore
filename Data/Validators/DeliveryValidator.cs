using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validations.Entities
{
    public class DeliveryValidator : AbstractValidator<Delivery>
    {
        public DeliveryValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название доставки не может быть пустым");
            RuleFor(i => i.DeliveryMethod)
                .NotNull().NotEmpty().WithMessage("Метод доставки не может быть пустым")
                .IsInEnum().WithMessage("Способ доставки обязан быть типа 'DeliveryMethodType'");
            RuleFor(i => i.DeliveryCost)
                .GreaterThanOrEqualTo(0).WithMessage("Цена доставки должна быть больше или равно 0");
            RuleFor(i => i.ApproximateDeliveryTime)
                .GreaterThanOrEqualTo(0).WithMessage("Приблизительное время доставки должна быть больше или равно 0");
        }
    }
}
