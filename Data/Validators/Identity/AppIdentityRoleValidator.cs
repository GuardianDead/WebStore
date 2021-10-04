using FluentValidation;
using WebStore.Data.Identity;

namespace WebStore.Validators.Identity
{
    public class AppIdentityRoleValidator : AbstractValidator<AppIdentityRole>
    {
        public AppIdentityRoleValidator()
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер роли не может быть пустым");
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название роли не может быть пустым");
        }
    }
}
