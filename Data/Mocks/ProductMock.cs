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

            List<ProductArticle> productArticles = await db.ProductArticles
                .Include(productArticle => productArticle.Model.Subcategory.Category)
                .ToListAsync();
            IEnumerable<Product> products = productArticles
                .SelectMany(productArticle => Enumerable.Range(0, random.Next(11, 22))
                    .Select(i => new Product(productArticle)));

            foreach (Product product in products)
                await productValidator.ValidateAndThrowAsync(product, cancellationToken);

            await db.Products.AddRangeAsync(products, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
