using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks.ProductArticleMock
{
    public class ProductArticleMock : IProductArticleMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<ProductArticle> productArticleValidator;

        public ProductArticleMock(AppDbContext db, IValidator<ProductArticle> productArticleValidator)
        {
            this.db = db;
            this.productArticleValidator = productArticleValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.ProductArticles.AnyAsync(cancellationToken))
                return true;

            var productModels = new ProductModel[]
            {
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Кроссовки Nike Air Zoom Pegasus", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Ботинки Adidas Terrex Trailmaker Mid R.RDY K", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Джинсы Levi's 514™ Straight (Big & Tall)", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Брюки KORPO COLLEZIONI", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Зимняя куртка SHARK FORCE", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Пальто Tom Farr", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Кепка Denkor Восьмиклинка-Хулиганка", cancellationToken),
                await db.ProductModels.Include(productModel => productModel.Subcategory.Category)
                    .SingleAsync(productModel => productModel.Name == "Шляпа ARMANI", cancellationToken)
            };

            var random = new Random();
            IEnumerable<ProductArticle> productArticles = productModels.SelectMany(productModel => productModel switch
            {
                #region Товар1 Кроссовки Nike
                ProductModel { Name: "Кроссовки Nike Air Zoom Pegasus" } => Enumerable.Range(38, 9)
                    .Select(size => new ProductArticle(productModel, size, "Черный")),
                #endregion
                #region Товар2 Ботинки Adidas
                ProductModel { Name: "Ботинки Adidas Terrex Trailmaker Mid R.RDY K" } => Enumerable.Range(35, 4)
                    .Select(size => new ProductArticle(productModel, size, "Бежевый")),
                #endregion
                #region Товар3 Джинсы Levi's
                ProductModel { Name: "Джинсы Levi's 514™ Straight (Big & Tall)" } => Enumerable.Range(30, 7)
                    .Select(size => new ProductArticle(productModel, size, "Светло-синий")),
                #endregion
                #region Товар4 Брюки KORPO
                ProductModel { Name: "Брюки KORPO COLLEZIONI" } => Enumerable.Range(42, 1)
                    .Select(size => new ProductArticle(productModel, size, "Черный")),
                #endregion
                #region Товар5 Куртка SHARK FORCE
                ProductModel { Name: "Зимняя куртка SHARK FORCE" } => Enumerable.Range(54, 1)
                    .Select(size => new ProductArticle(productModel, size, "Черный")),
                #endregion
                #region Товар6 Пальто Tom Farr
                ProductModel { Name: "Пальто Tom Farr" } => Enumerable.Range(42, 3)
                    .Select(size => new ProductArticle(productModel, size, "Серое")),
                #endregion
                #region Товар7 Кепка Denkor
                ProductModel { Name: "Кепка Denkor Восьмиклинка-Хулиганка" } => Enumerable.Range(58, 2)
                    .Select(size => new ProductArticle(productModel, size, "Серое")),
                #endregion
                #region Товар8 Шляпа ARMANI
                ProductModel { Name: "Шляпа ARMANI" } => Enumerable.Range(56, 1)
                    .Select(size => new ProductArticle(productModel, size, "Желтый")),
                #endregion
                _ => throw new NotImplementedException("Данной модели товара не существует")
            });

            foreach (ProductArticle productArticle in productArticles)
                await productArticleValidator.ValidateAndThrowAsync(productArticle, cancellationToken);

            await db.ProductArticles.AddRangeAsync(productArticles, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
