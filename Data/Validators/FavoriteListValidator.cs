using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class FavoriteListValidator : AbstractValidator<FavoriteList>
    {
        public FavoriteListValidator(IValidator<FavoriteProduct> favoriteProductValidator)
        {
            When(i => i.Products is not null, () =>
                RuleFor(i => i.Products)
                    .ForEach(i => i.SetValidator(favoriteProductValidator)));
        }
    }
}
