using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories;
using WebStore.Data.Repositories.AppIdentityUserRepository;
using WebStore.Data.Repositories.ProductModelRepository;

namespace WebStore.Data.Mocks.ProductModelMock
{
    public class ProductModelMock : IProductModelMock
    {
        private readonly IProductModelRepository productModelRepository;
        private readonly ISubcategoryRepository subcategoryRepository;
        private readonly IValidator<ProductModel> productModelValidator;
        private readonly IUserRepository userRepository;

        public ProductModelMock(IProductModelRepository productModelRepository, ISubcategoryRepository subcategoryRepository,
            IValidator<ProductModel> productModelValidator, IUserRepository userRepository)
        {
            this.productModelRepository = productModelRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.productModelValidator = productModelValidator;
            this.userRepository = userRepository;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await productModelRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var subcategoriesNames = new Subcategory[]
            {
                await subcategoryRepository.GetAsync(i => i.Name == "Кроссовки",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Ботинки",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Джинсы",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Брюки",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Куртки",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Пальто",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Кепки",false,cancellationToken),
                await subcategoryRepository.GetAsync(i => i.Name == "Шляпы",false,cancellationToken),
            };

            var productModels = subcategoriesNames.Select(subcategory => subcategory switch
            {
                #region Товар1 Кроссовки Nike
                Subcategory { Name: "Кроссовки" } => new ProductModel("Кроссовки Nike Air Zoom Pegasus", 9990, 365,
                  "США", true, "Nike", subcategory,
                  File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                  new Dictionary<string, string>()
                  {
                       { "Назначение","бег" },
                       { "Застежка","шнуровка" },
                  }
                  , new List<string>()
                  {
                       "Текстиль",
                  }
                  , new List<byte[]>()
                  {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus3.jfif"),
                  }, DateTime.Now),
                #endregion
                #region Товар2 Ботинки Adidas
                Subcategory { Name: "Ботинки" } => new ProductModel("Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K", 7399, 180,
                    "Вьетнам", false, "Adidas", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                   new Dictionary<string, string>()
                   {
                       { "Материал подкладки","текстиль" },
                       { "Материал подошвы","резина" },
                   }
                   , new List<string>()
                   {
                       "Синтетика 60%",
                       "Текстиль 40%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K1.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K2.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K3.jpg"),
                   }, DateTime.Now),
                #endregion
                #region Товар3 Джинсы Levi's
                Subcategory { Name: "Джинсы" } => new ProductModel("Джинсы Levi's 514™ Straight (Big & Tall)", 5673, 300,
                   "США", true, "Levi's", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Модель","прямые" },
                       { "Посадка","низкая" },
                       { "Застежка","молния" },
                       { "Передние карманы","втачные" },
                       { "Задние карманы","накладные" },
                   }
                   , new List<string>()
                   {
                       "Хлопок 99%",
                       "Эластан 1%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)2.jfif"),
                   }, DateTime.Now),
                #endregion
                #region Товар4 Брюки KORPO
                Subcategory { Name: "Брюки" } => new ProductModel("Брюки KORPO COLLEZIONI", 6560, 600,
                   "Италия", false, "KORPO", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                   new Dictionary<string, string>()
                   {
                       { "Стиль","Casual, Классический" },
                       { "Расцветка","Однотонная" },
                       { "Форма","Прямая" },
                   }
                   , new List<string>()
                   {
                       "Шерсть 60%",
                       "Полиэстер 38%",
                       "Эластан 2%",
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI1.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI2.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI3.png"),
                   }, DateTime.Now),
                #endregion
                #region Товар5 Куртка SHARK FORCE
                Subcategory { Name: "Куртки" } => new ProductModel("Зимняя куртка мужская SHARK FORCE", 19080, 365 * 3,
                   "Пекин", true, "SHARK FORCE", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210131.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210132.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210133.jfif"),
                   }, DateTime.Now),
                #endregion
                #region Товар6 Пальто Tom Farr
                Subcategory { Name: "Пальто" } => new ProductModel("Пальто Tom Farr", 10890, 90,
                   "Россия", false, "Tom Farr", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","весна, осень" },
                       { "Застежка","отсутствует" },
                   }
                   , new List<string>()
                   {
                       "Полиэстер 70%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr3.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr4.jfif"),
                   }, DateTime.Now),
                #endregion
                #region Товар7 Кепка Denkor
                Subcategory { Name: "Кепки" } => new ProductModel("Кепка мужская Denkor Восьмиклинка-Хулиганка", 1190, 12,
                   "Россия", true, "Denkor", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","лето" },
                   }
                   , new List<string>()
                   {
                       "Ткань"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка2.jfif"),
                   }, DateTime.Now),
                #endregion
                #region Товар8 Шляпа ARMANI
                Subcategory { Name: "Шляпы" } => new ProductModel("Шляпа ARMANI", 16730, 300,
                "Италия", true, "ARMANI", subcategory,
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI1.jfif"),
                   }, DateTime.Now),
                #endregion
                _ => throw new NotImplementedException("Данная подкатегория не найдена")
            });

            productModels.Select(async productModel => await productModelValidator.ValidateAndThrowAsync(productModel, cancellationToken));

            await productModelRepository.AddRangeAsync(productModels, cancellationToken);
            return await productModelRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
