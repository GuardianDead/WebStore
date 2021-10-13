using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories.ProductArticleRepository;
using WebStore.Data.Repositories.ProductModelRepository;

namespace WebStore.Data.Mocks.ProductArticleMock
{
    public class ProductArticleMock : IProductArticleMock
    {
        private readonly IProductArticleRepository productArticleRepository;
        private readonly IValidator<ProductArticle> productArticleValidator;
        private readonly IProductModelRepository productModelRepository;

        public ProductArticleMock(IProductArticleRepository productArticleRepository, IValidator<ProductArticle> productArticleValidator,
            IProductModelRepository productModelRepository)
        {
            this.productArticleRepository = productArticleRepository;
            this.productArticleValidator = productArticleValidator;
            this.productModelRepository = productModelRepository;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await productArticleRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var productModels = new ProductModel[]
            {
                await productModelRepository.GetAsync(productModel => productModel.Name == "Кроссовки Nike Air Zoom Pegasus", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Джинсы Levi's 514™ Straight (Big & Tall)", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Брюки KORPO COLLEZIONI", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Зимняя куртка мужская SHARK FORCE", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Пальто Tom Farr", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка", false, cancellationToken),
                await productModelRepository.GetAsync(productModel => productModel.Name == "Шляпа ARMANI", false, cancellationToken)
            };

            var random = new Random();
            var productArticles = productModels.SelectMany(productModel => productModel switch
            {
                #region Товар1 Кроссовки Nike
                ProductModel { Name: "Кроссовки Nike Air Zoom Pegasus" } => Enumerable.Range(38, 9)
                .Select(size => new ProductArticle(productModel, size, "Черный", random.Next(50, 101))),
                #endregion
                #region Товар2 Ботинки Adidas
                ProductModel { Name: "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K" } => Enumerable.Range(35, 4)
                .Select(size => new ProductArticle(productModel, size, "Бежевый", random.Next(50, 101))),
                #endregion
                #region Товар3 Джинсы Levi's
                ProductModel { Name: "Джинсы Levi's 514™ Straight (Big & Tall)" } => Enumerable.Range(30, 7)
                .Select(size => new ProductArticle(productModel, size, "Светло-синий", random.Next(50, 101))),
                #endregion
                #region Товар4 Брюки KORPO
                ProductModel { Name: "Брюки KORPO COLLEZIONI" } => Enumerable.Range(42, 1)
                .Select(size => new ProductArticle(productModel, size, "Черный", random.Next(50, 101))),
                #endregion
                #region Товар5 Куртка SHARK FORCE
                ProductModel { Name: "Зимняя куртка мужская SHARK FORCE" } => Enumerable.Range(54, 1)
                .Select(size => new ProductArticle(productModel, size, "Черный", random.Next(50, 101))),
                #endregion
                #region Товар6 Пальто Tom Farr
                ProductModel { Name: "Пальто Tom Farr" } => Enumerable.Range(42, 3)
                .Select(size => new ProductArticle(productModel, size, "Серое", random.Next(50, 101))),
                #endregion
                #region Товар7 Кепка Denkor
                ProductModel { Name: "Кепка мужская Denkor Восьмиклинка-Хулиганка" } => Enumerable.Range(58, 2)
                .Select(size => new ProductArticle(productModel, size, "Серое", random.Next(50, 101))),
                #endregion
                #region Товар8 Шляпа ARMANI
                ProductModel { Name: "Шляпа ARMANI" } => Enumerable.Range(56, 1)
                .Select(size => new ProductArticle(productModel, size, "Желтый", random.Next(50, 101))),
                #endregion
                _ => throw new NotImplementedException("Данной модели товара не существует")
            });

            productArticles.Select(async productArticle => await productArticleValidator.ValidateAndThrowAsync(productArticle, cancellationToken));

            await productArticleRepository.AddRangeAsync(productArticles, cancellationToken);
            return await productArticleRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
