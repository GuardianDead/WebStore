using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks
{
    public class ProductCategoriesMock
    {
        private static readonly ProductCategory[] productCategoriesNames =
        {
            new ProductCategory("Обувь"),
            new ProductCategory("Нижняя одежда"),
            new ProductCategory("Верхняя одежда"),
            new ProductCategory("Головные уборы"),
        };

        public static void Init(AppDbContext db)
        {
            if (db.ProductCategories.FirstOrDefault() != null)
            {
                return;
            }
            db.ProductCategories.AddRange(productCategoriesNames);
            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            if (db.ProductCategories.FirstOrDefault() != null)
            {
                return;
            }
            await db.AddRangeAsync(productCategoriesNames);
            await db.SaveChangesAsync();
        }
    }
}
