using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories;

namespace WebStore.Data.Mocks.CategoryMock
{
    public class CategoryMock : ICategoryMock
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IValidator<Category> categoryValidator;

        public CategoryMock(ICategoryRepository categoryRepository, IValidator<Category> categoryValidator)
        {
            this.categoryRepository = categoryRepository;
            this.categoryValidator = categoryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await categoryRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            Category[] categories =
            {
                new Category("Обувь"),
                new Category("Нижняя одежда"),
                new Category("Верхняя одежда"),
                new Category("Головные уборы"),
            };

            categories.Select(async category => await categoryValidator.ValidateAndThrowAsync(category, cancellationToken));

            await categoryRepository.AddRangeAsync(categories, cancellationToken);
            return await categoryRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
