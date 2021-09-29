using FluentValidation;
using System;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator(IValidator<Delivery> deliveryValidator, IValidator<ProductArticle> productArticleValidator)
        {
            RuleFor(i => i.Address)
                .NotNull().NotEmpty().WithMessage("Адрес заказа не может быть пустым");
            RuleFor(i => i.Delivery)
                .NotNull().WithMessage("Доставка заказа не может быть пустым")
                .SetValidator(deliveryValidator);
            RuleFor(i => i.OrderDate)
                .NotNull().NotEmpty().WithMessage("Дата заказа не может быть пустым")
                .LessThan(DateTime.Now).WithMessage("Дата заказа не может быть в прошедшем времени");
            RuleFor(i => i.OrderPaymentMethodType)
                .NotNull().NotEmpty().WithMessage("Тип оплаты заказа не может быть пустым")
                .IsInEnum().WithMessage("Тип оплаты заказа обязан быть типа 'OrderPaymentMethodType'");
            RuleFor(i => i.OrderStatusType)
                .NotNull().NotEmpty().WithMessage("Статус заказа не может быть пустым")
                .IsInEnum().WithMessage("Статус заказа обязан быть типа 'OrderStatusType'");
            RuleFor(i => i.ProductArticles)
                .NotNull().WithMessage("Список артикулов товаров не может быть пустым")
                .ForEach(i => i.SetValidator(productArticleValidator));
            RuleFor(i => i.SummaryCost)
                .NotNull().WithMessage("Общая стоимость заказа не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Общая стоимость заказа должна быть больше 0");
        }
    }
}
