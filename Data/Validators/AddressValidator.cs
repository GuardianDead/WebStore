using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(i => i.Country)
                .NotEmpty().NotNull().WithMessage("Страна не может быть пустым");
            RuleFor(i => i.City)
                .NotEmpty().NotNull().WithMessage("Город не может быть пустым");
            RuleFor(i => i.Street)
                .NotEmpty().NotNull().WithMessage("Улица не может быть пустым");
            RuleFor(i => i.HouseNumber)
                .NotEmpty().NotNull().WithMessage("Номер дома не может быть пустым");
            RuleFor(i => i.PostalCode)
                .NotEmpty().NotNull().WithMessage("Почтовый индекс не может быть пустым");
        }
    }
}
