using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator(IValidator<Subcategory> productSubcategoryValidator, IValidator<ProductModelPhoto> productModelPhotoValidator, IValidator<ProductModelFeature> productModelFeatureValidator, IValidator<ProductModelMaterial> productModelMaterialValidator)
        {
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название модели не может быть пустым");
            RuleFor(i => i.DaysGuarantee)
                .NotNull().NotEmpty().WithMessage("Гарантия модели товара не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Гарантия модели товара должна быть больше или равна 0");
            RuleFor(i => i.Brand)
                .NotNull().NotEmpty().WithMessage("Бренд модели товара не может быть пустым");
            RuleFor(i => i.Price)
                .NotNull().WithMessage("Цена модели товара не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Цена модели товара должна быть больше или равна 0");
            RuleFor(i => i.Subcategory)
                .NotNull().WithMessage("Подкатегория модели товара не может быть пустым")
                .SetValidator(productSubcategoryValidator);
            RuleFor(i => i.Materials)
                .NotNull().WithMessage("Материалы модели товара не могут быть пустыми")
                .ForEach(j => j.SetValidator(productModelMaterialValidator));
            RuleFor(i => i.Features)
                .NotNull().WithMessage("Характеристики модели товара не могут быть пустыми")
                .ForEach(j => j.SetValidator(productModelFeatureValidator));
            RuleFor(i => i.MainPhoto)
                .NotNull().WithMessage("Главное фото модели товара не может быть пустым");
            RuleFor(i => i.Photos)
                .NotNull().WithMessage("Фотографии модели товара не могут быть пустым")
                .ForEach(j => j.SetValidator(productModelPhotoValidator));
            RuleFor(i => i.СountryManufacturer)
                .NotNull().NotEmpty().WithMessage("Id модели товара не может быть пустая");
            RuleFor(i => i.DateTimeCreation)
                .NotNull().NotEmpty().WithMessage("Время создания модели товара не может быть пустым");
        }
    }
}
