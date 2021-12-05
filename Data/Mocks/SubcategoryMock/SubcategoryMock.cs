using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks.SubcategoryMock
{
    public class SubcategoryMock : ISubcategoryMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<Subcategory> subcategoryValidator;

        public SubcategoryMock(AppDbContext db, IValidator<Subcategory> subcategoryValidator)
        {
            this.db = db;
            this.subcategoryValidator = subcategoryValidator;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Subcategories.AnyAsync(cancellationToken))
                return true;

            var categories = new Category[]
            {
                await db.Categories.SingleAsync(i => i.Name == "Обувь",cancellationToken),
                await db.Categories.SingleAsync(i => i.Name == "Низ",cancellationToken),
                await db.Categories.SingleAsync(i => i.Name == "Верх",cancellationToken),
                await db.Categories.SingleAsync(i => i.Name == "Головные уборы",cancellationToken)
            };

            IEnumerable<Subcategory> subcategories = categories.SelectMany(category => category switch
            {
                Category { Name: "Обувь" } => new string[] { "Кроссовки", "Ботинки", "Туфли", "Каблуки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Низ" } => new string[] { "Брюки", "Юбки", "Джинсы" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Верх" } => new string[] { "Куртки","Пальто","Футболки","Свитера",
                    "Пиджаки","Худи","Толстовки","Ветровки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                Category { Name: "Головные уборы" } => new string[] { "Кепки", "Шляпы", "Панамы", "Шапки" }
                    .Select(subcaregoryName => new Subcategory(subcaregoryName, category)),
                _ => throw new System.NotImplementedException("Данная категория не найдена")
            });

            foreach (Subcategory subcategory in subcategories)
                await subcategoryValidator.ValidateAndThrowAsync(subcategory, cancellationToken);

            await db.Subcategories.AddRangeAsync(subcategories, cancellationToken);
            return await db.SaveChangesAsync(cancellationToken) != -1;
        }
    }
}
