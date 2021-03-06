using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Services;

namespace WebStore.Data.Mocks.ProductModelMock
{
    public class ProductModelMock : IProductModelMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<ProductModel> productModelValidator;

        public ProductModelMock(AppDbContext db, IValidator<ProductModel> productModelValidator)
        {
            this.db = db;
            this.productModelValidator = productModelValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.ProductModels.AnyAsync(cancellationToken))
                return true;

            var subcategoriesNames = new Subcategory[]
            {
                await db.Subcategories.SingleAsync(i => i.Name == "Кроссовки",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Ботинки",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Джинсы",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Брюки",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Куртки",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Пальто",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Кепки",cancellationToken),
                await db.Subcategories.SingleAsync(i => i.Name == "Шляпы",cancellationToken),
            };

            #region Load Main Product Model Photos
            var productModelsMainPhotos = new List<byte[]>()
            {
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\NIKE Air Zoom Pegasus.jfif",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Брюки KORPO COLLEZIONI.png",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Пальто Tom Farr.jfif",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif",cancellationToken)),
                await PhotoEditorService.MakeBackgroundTrancparentAsync(await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\productPhotos\Шляпа ARMANI.jfif",cancellationToken)),
            };
            #endregion
            #region Load Product Model Photos
            var productModelsPhotos = new List<List<byte[]>>()
            {
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus1.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus2.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus3.jfif", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K1.jpg", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K2.jpg", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K3.jpg", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)1.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)2.jfif", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI1.png", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI2.png", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI3.png", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210131.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210132.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210133.jfif", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr1.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr2.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr3.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr4.jfif", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка1.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка2.jfif", cancellationToken),
                },
                new List<byte[]>()
                {
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif", cancellationToken),
                    await File.ReadAllBytesAsync(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI1.jfif", cancellationToken),
                },
            };
            #endregion
            IEnumerable<ProductModel> productModels = subcategoriesNames.Select(subcategory => subcategory switch
            {
                #region Товар1 Кроссовки Nike
                Subcategory { Name: "Кроссовки" } => new ProductModel(
                     name: "Кроссовки Nike Air Zoom Pegasus",
                     price: 9990,
                     daysGuarantee: 365,
                     countryManufacturer: "США",
                     brand: "Nike",
                     subcategory: subcategory,
                     mainPhoto: productModelsMainPhotos[0],
                     features: new List<ProductModelFeature>
                     {
                         new ProductModelFeature("Назначение","Бег"),
                         new ProductModelFeature("Застежка","Шнуровка"),
                     },
                     materials: new List<ProductModelMaterial>
                     {
                         new ProductModelMaterial("Текстиль")
                     },
                     photos: productModelsPhotos[0].Select(photo => new ProductModelPhoto(photo)).ToList(),
                     dateTimeCreation: new DateTime(2021, 11, 5)),
                #endregion
                #region Товар2 Ботинки Adidas
                Subcategory { Name: "Ботинки" } => new ProductModel(
                      name: "Ботинки Adidas Terrex Trailmaker Mid R.RDY K",
                      price: 7399,
                      daysGuarantee: 180,
                      countryManufacturer: "Вьетнам",
                      brand: "Adidas",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[1],
                      features: new List<ProductModelFeature>
                      {
                          new ProductModelFeature("Материал подкладки", "Текстиль"),
                          new ProductModelFeature("Материал подошвы", "Резина")
                      },
                      materials: new List<ProductModelMaterial>
                      {
                         new ProductModelMaterial("Синтетика 60%"),
                         new ProductModelMaterial("Текстиль 40%")
                      },
                      photos: productModelsPhotos[1].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 11, 20)),
                #endregion
                #region Товар3 Джинсы Levi's
                Subcategory { Name: "Джинсы" } => new ProductModel(
                      name: "Джинсы Levi's 514™ Straight (Big & Tall)",
                      price: 5673,
                      daysGuarantee: 300,
                      countryManufacturer: "США",
                      brand: "Levi's",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[2],
                      features: new List<ProductModelFeature>
                      {
                          new ProductModelFeature("Модель","Прямые"),
                          new ProductModelFeature("Посадка","Низкая"),
                          new ProductModelFeature("Застежка","Молния"),
                          new ProductModelFeature("Передние карманы","Втачные"),
                          new ProductModelFeature("Задние карманы","Накладные")
                      },
                      materials: new List<ProductModelMaterial>
                      {
                          new ProductModelMaterial("Хлопок 99%"),
                          new ProductModelMaterial("Эластан 1%")
                      },
                      photos: productModelsPhotos[2].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 10, 13)),
                #endregion
                #region Товар4 Брюки KORPO
                Subcategory { Name: "Брюки" } => new ProductModel(
                      name: "Брюки KORPO COLLEZIONI",
                      price: 6560,
                      daysGuarantee: 600,
                      countryManufacturer: "Италия",
                      brand: "KORPO",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[3],
                      features: new List<ProductModelFeature>
                      {
                          new ProductModelFeature("Стиль","Casual, Классический"),
                          new ProductModelFeature("Расцветка","Однотонная"),
                          new ProductModelFeature("Форма","Прямая")
                      },
                      materials: new List<ProductModelMaterial>
                      {
                          new ProductModelMaterial("Шерсть 60%"),
                          new ProductModelMaterial("Полиэстер 38%"),
                          new ProductModelMaterial("Эластан 2%")
                      },
                      photos: productModelsPhotos[3].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 11, 3)),
                #endregion
                #region Товар5 Куртка SHARK FORCE
                Subcategory { Name: "Куртки" } => new ProductModel(
                      name: "Зимняя куртка SHARK FORCE",
                      price: 19080,
                      daysGuarantee: 365 * 3,
                      countryManufacturer: "Пекин",
                      brand: "SHARK FORCE",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[4],
                      features: new List<ProductModelFeature>(),
                      materials: new List<ProductModelMaterial>(),
                      photos: productModelsPhotos[4].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 11, 8)),
                #endregion
                #region Товар6 Пальто Tom Farr
                Subcategory { Name: "Пальто" } => new ProductModel(
                      name: "Пальто Tom Farr",
                      price: 10890,
                      daysGuarantee: 90,
                      countryManufacturer: "Россия",
                      brand: "Tom Farr",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[5],
                      features: new List<ProductModelFeature>
                      {
                          new ProductModelFeature("Сезон","Весна, Осень"),
                          new ProductModelFeature("Застежка","Отсутствует")
                      },
                      materials: new List<ProductModelMaterial>
                      {
                          new ProductModelMaterial("Полиэстер 70%")
                      },
                      photos: productModelsPhotos[5].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 9, 26)),
                #endregion
                #region Товар7 Кепка Denkor
                Subcategory { Name: "Кепки" } => new ProductModel(
                      name: "Кепка Denkor Восьмиклинка-Хулиганка",
                      price: 1190,
                      daysGuarantee: 12,
                      countryManufacturer: "Россия",
                      brand: "Denkor",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[6],
                      features: new List<ProductModelFeature>
                      {
                          new ProductModelFeature("Сезон","Лето")
                      },
                      materials: new List<ProductModelMaterial>
                      {
                          new ProductModelMaterial("Ткань")
                      },
                      photos: productModelsPhotos[6].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 11, 11)),
                #endregion
                #region Товар8 Шляпа ARMANI
                Subcategory { Name: "Шляпы" } => new ProductModel(
                      name: "Шляпа ARMANI",
                      price: 16730,
                      daysGuarantee: 300,
                      countryManufacturer: "Италия",
                      brand: "ARMANI",
                      subcategory: subcategory,
                      mainPhoto: productModelsMainPhotos[7],
                      features: new List<ProductModelFeature>(),
                      materials: new List<ProductModelMaterial>(),
                      photos: productModelsPhotos[7].Select(photo => new ProductModelPhoto(photo)).ToList(),
                      dateTimeCreation: new DateTime(2021, 11, 11)),
                #endregion
                _ => throw new NotImplementedException("Данная подкатегория не найдена")
            });

            foreach (ProductModel productModel in productModels)
                await productModelValidator.ValidateAndThrowAsync(productModel, cancellationToken);

            await db.ProductModels.AddRangeAsync(productModels, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
