using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories.ProductArticleRepository;
using WebStore.Data.Repositories.ProductRepository;

namespace WebStore.Data.Mocks.ProductMock
{
    public class ProductMock : IProductMock
    {
        private readonly IProductArticleRepository productArticleRepository;
        private readonly IProductRepository productRepository;
        private readonly IValidator<Product> productValidator;

        public ProductMock(IProductArticleRepository productArticleRepository, IProductRepository productRepository,
            IValidator<Product> productValidator)
        {
            this.productArticleRepository = productArticleRepository;
            this.productRepository = productRepository;
            this.productValidator = productValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await productRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var productArticlesLists = new IEnumerable<ProductArticle>[]
            {
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall), true)", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Брюки KORPO COLLEZIONI", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Пальто Tom Farr", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка", false, cancellationToken),
                await productArticleRepository.GetAllAsync(i => i.Model.Name == "Шляпа ARMANI", false, cancellationToken),
            };

            var products = productArticlesLists
                .SelectMany(productArticles => productArticles
                .SelectMany(article => Enumerable.Repeat(new Product(article), article.Count)));

            products.Select(async product => await productValidator.ValidateAndThrowAsync(product, cancellationToken));

            await productRepository.AddRangeAsync(products, cancellationToken);
            return await productRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
