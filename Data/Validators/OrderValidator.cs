using FluentValidation;
using System;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator(IValidator<Delivery> deliveryValidator, IValidator<Address> addressValidator,
            IValidator<Product> productValidator)
        {
            RuleFor(i => i.Address)
                .NotNull().NotEmpty().WithMessage("Адрес заказа не может быть пустым")
                .SetValidator(addressValidator);
            RuleFor(i => i.TrackNumber)
                .NotNull().NotEmpty().WithMessage("Трек номер заказа не может быть пустым");
            RuleFor(i => i.Delivery)
                .NotNull().WithMessage("Доставка  не может быть пустым")
                .SetValidator(deliveryValidator);
            RuleFor(i => i.DateTimeCreation)
                .NotNull().NotEmpty().WithMessage("Дата заказа не может быть пустым")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Дата заказа не может быть в прошедшем времени");
            RuleFor(i => i.OrderPaymentMethod)
                .NotNull().NotEmpty().WithMessage("Тип оплаты заказа не может быть пустым")
                .IsInEnum().WithMessage("Тип оплаты заказа обязан быть типа 'OrderPaymentMethodType'");
            RuleFor(i => i.OrderStatus)
                .NotNull().NotEmpty().WithMessage("Статус заказа не может быть пустым")
                .IsInEnum().WithMessage("Статус заказа обязан быть типа 'OrderStatusType'");
            RuleFor(i => i.Products)
                .NotNull().WithMessage("Список артикулов товаров не может быть пустым")
                .ForEach(i => i.SetValidator(productValidator));
            RuleFor(i => i.TotalCost)
                .NotNull().WithMessage("Общая стоимость не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Общая стоимость заказа должна быть больше или равна 0");
            RuleFor(i => i.PhoneNumber)
                .NotNull().WithMessage("Номер телефона не может быть неопределен")
                .ChildRules(i => i.RuleFor(i => i.Length)
                    .ExclusiveBetween(7, 15).WithMessage("Номер телефона должен быть в диапазоне от 7 до 15"));
        }
    }
}
