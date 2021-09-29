﻿using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator(IValidator<ProductSubcategory> productSubcategoryValidator)
        {
            RuleFor(i => i.Id)
                .NotNull().NotEmpty().WithMessage("Номер модели товара не может быть пустым");
            RuleFor(i => i.Name)
                .NotNull().NotEmpty().WithMessage("Название модели не может быть пустым");
            RuleFor(i => i.Guarantee)
                .NotNull().NotEmpty().WithMessage("Гарантия модели товара не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Гарантия модели товара должна быть больше 0");
            RuleFor(i => i.Brand)
                .NotNull().NotEmpty().WithMessage("Бренд модели товара не может быть пустым");
            RuleFor(i => i.Price)
                .NotNull().WithMessage("Цена модели товара не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Цена модели товара должна быть больше 0");
            RuleFor(i => i.Subcategory)
                .NotNull().WithMessage("Подкатегория модели товара не может быть пустым")
                .SetValidator(productSubcategoryValidator);
            RuleFor(i => i.Materials)
                .NotNull().WithMessage("Материалы модели товара не могут быть пустым")
                .ForEach(j => j.NotNull().NotEmpty().WithMessage("Материал модели товара не может быть пустым"));
            RuleFor(i => i.Features)
                .NotNull().WithMessage("Характеристики модели товара не могут быть пустым")
                .ForEach(i =>
                    i.Must(i => i.Key != null && i.Key != "").WithMessage("Ключ характеристики модели товара не может быть пустым")
                    .Must(i => i.Value != null && i.Value != "").WithMessage("Значение характеристики модели товара не может быть пустым"));
            RuleFor(i => i.MainPhoto)
                .NotNull().WithMessage("Главное фото модели товара не может быть пустым");
            RuleFor(i => i.Photos)
                .NotNull().WithMessage("Фотографии модели товара не могут быть пустым")
                .ForEach(i => i.Must(i => i != null).WithMessage("Картинка модели товара не может быть пустая"));
            RuleFor(i => i.СountryManufacturer)
                .NotNull().NotEmpty().WithMessage("Id модели товара не может быть пустая");
        }
    }
}