using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks.ProductMock
{
    public class ProductMock : IProductMock
    {
        private readonly Random random = new Random();
        private readonly AppDbContext db;
        private readonly IValidator<Product> productValidator;

        public ProductMock(AppDbContext db, IValidator<Product> productValidator)
        {
            this.db = db;
            this.productValidator = productValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Products.AnyAsync(cancellationToken))
                return true;

            var productArticlesLists = new IEnumerable<ProductArticle>[]
            {
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall), true)"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Брюки KORPO COLLEZIONI"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Пальто Tom Farr"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка"),
                db.ProductArticles
                    .Include(productArticle => productArticle.Model.Subcategory.Category)
                    .Where(i => i.Model.Name == "Шляпа ARMANI"),
            };

            IEnumerable<Product> products = productArticlesLists
                .SelectMany(productArticles => productArticles
                .SelectMany(article => Enumerable.Repeat(new Product(article), random.Next(50, 101))));

            foreach (Product product in products)
                await productValidator.ValidateAndThrowAsync(product, cancellationToken);

            await db.Products.AddRangeAsync(products, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
