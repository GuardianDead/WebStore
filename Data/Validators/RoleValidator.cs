using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators.Identity
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название роли не может быть пустым");
        }
    }
}
