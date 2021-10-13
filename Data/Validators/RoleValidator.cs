using FluentValidation;
using WebStore.Data.Identity;

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
