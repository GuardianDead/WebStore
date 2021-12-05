using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks.CategoryMock
{
    public class CategoryMock : ICategoryMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<Category> categoryValidator;

        public CategoryMock(AppDbContext db, IValidator<Category> categoryValidator)
        {
            this.db = db;
            this.categoryValidator = categoryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Categories.AnyAsync(cancellationToken))
                return true;

            Category[] categories =
            {
                new Category("Обувь"),
                new Category("Низ"),
                new Category("Верх"),
                new Category("Головные уборы"),
            };

            foreach (Category category in categories)
                await categoryValidator.ValidateAndThrowAsync(category, cancellationToken);

            await db.Categories.AddRangeAsync(categories, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
