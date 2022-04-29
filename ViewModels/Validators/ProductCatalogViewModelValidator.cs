using FluentValidation;

namespace WebStore.ViewModels.Validators
{
    public class ProductCatalogViewModelValidator : AbstractValidator<ProductCatalogViewModel>
    {
        public ProductCatalogViewModelValidator()
        {
            RuleFor(o => o.SelectedBrands)
                .NotNull().WithMessage("Выбранные бренды не могу быть null");
            RuleFor(o => o.SelectedColors)
                .NotNull().WithMessage("Выбранные бренды не могу быть null");
            RuleFor(o => o.SelectedSizes)
                .NotNull().WithMessage("Выбранные бренды не могу быть null");
            RuleFor(o => o.SortProductsType)
                .IsInEnum().WithMessage("Тип по которому должна происходить отсортировка товаров должна быть типа 'SortProductsType'");
        }
    }
}
