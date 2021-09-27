using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks
{
    public class ProductSubcategoriesMock
    {
        public static void Init(AppDbContext db)
        {
            List<string> productSubcategoriesNames;
            ProductCategory productCategory;

            if(!db.ProductSubcategories.Any(i => i.Category.Name == "Обувь"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Обувь");
                productSubcategoriesNames = new List<string>() { "Кроссовки", "Ботинки", "Туфли", "Каблуки" };
                db.ProductSubcategories.AddRange(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Нижняя одежда"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Нижняя одежда");
                productSubcategoriesNames = new List<string>() { "Брюки", "Юбки", "Джинсы" };
                db.ProductSubcategories.AddRange(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Верхняя одежда"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Верхняя одежда");
                productSubcategoriesNames = new List<string>() { "Куртки","Пальто","Футболки","Свитера","Пиджаки"
                ,"Худи","Толстовки","Ветровки" };
                db.ProductSubcategories.AddRange(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Головные уборы"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Головные уборы");
                productSubcategoriesNames = new List<string>() { "Кепки", "Шляпа", "Панама", "Шапка" };
                db.ProductSubcategories.AddRange(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            List<string> productSubcategoriesNames;
            ProductCategory productCategory;

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Обувь"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Обувь");
                productSubcategoriesNames = new List<string>() { "Кроссовки", "Ботинки", "Туфли", "Каблуки" };
                await db.ProductSubcategories.AddRangeAsync(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Нижняя одежда"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Нижняя одежда");
                productSubcategoriesNames = new List<string>() { "Брюки", "Юбки", "Джинсы" };
                await db.ProductSubcategories.AddRangeAsync(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Верхняя одежда"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Верхняя одежда");
                productSubcategoriesNames = new List<string>() { "Куртки","Пальто","Футболки","Свитера","Пиджаки"
                ,"Худи","Толстовки","Ветровки" };
                await db.ProductSubcategories.AddRangeAsync(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            if (!db.ProductSubcategories.Any(i => i.Category.Name == "Головные уборы"))
            {
                productCategory = db.ProductCategories.SingleOrDefault(i => i.Name == "Головные уборы");
                productSubcategoriesNames = new List<string>() { "Кепки", "Шляпа", "Панама", "Шапка" };
                await db.ProductSubcategories.AddRangeAsync(GenerateProductSubcategories(productSubcategoriesNames, productCategory).ToList());
            }

            await db.SaveChangesAsync();
        }

        private static IEnumerable<ProductSubcategory> GenerateProductSubcategories(List<string> productSubcategoriesNames, ProductCategory productCategory)
        {
            foreach (var productSubcategoriesName in productSubcategoriesNames)
            {
                yield return new ProductSubcategory(productSubcategoriesName, productCategory);
            }
        }
    }
}
