using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Repositories;

namespace WebStore.Data.Mocks.SubcategoryMock
{
    public class SubcategoryMock : ISubcategoryMock
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubcategoryRepository subcategoryRepository;
        private readonly IValidator<Subcategory> subcategoryValidator;

        public SubcategoryMock(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository,
            IValidator<Subcategory> subcategoryValidator)
        {
            this.categoryRepository = categoryRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.subcategoryValidator = subcategoryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await subcategoryRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var categories = new Category[]
            {
                await categoryRepository.GetAsync(i => i.Name == "Обувь",false,cancellationToken),
                await categoryRepository.GetAsync(i => i.Name == "Нижняя одежда",false,cancellationToken),
                await categoryRepository.GetAsync(i => i.Name == "Верхняя одежда",false,cancellationToken),
                await categoryRepository.GetAsync(i => i.Name == "Головные уборы",false,cancellationToken)
            };

            var subcategories = categories.SelectMany(category => category switch
            {
                Category { Name: "Обувь" } => new string[] { "Кроссовки", "Ботинки", "Туфли", "Каблуки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Нижняя одежда" } => new string[] { "Брюки", "Юбки", "Джинсы" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Верхняя одежда" } => new string[] { "Куртки","Пальто","Футболки","Свитера",
                    "Пиджаки","Худи","Толстовки","Ветровки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Головные уборы" } => new string[] { "Кепки", "Шляпы", "Панамы", "Шапки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                _ => throw new System.NotImplementedException("Данная категория не найдена")
            });

            subcategories.Select(async subcategory => await subcategoryValidator.ValidateAndThrowAsync(subcategory, cancellationToken));

            await subcategoryRepository.AddRangeAsync(subcategories, cancellationToken);
            return await subcategoryRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
