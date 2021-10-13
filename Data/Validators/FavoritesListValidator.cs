using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class FavoritesListValidator : AbstractValidator<FavoritesList>
    {
        public FavoritesListValidator(IValidator<FavoritesListProduct> favoritesListProductValidator)
        {
            RuleFor(i => i.Products)
                .NotNull().WithMessage("Список продуктов не может быть неопределен")
                .ForEach(i => i.SetValidator(favoritesListProductValidator));
        }
    }
}
