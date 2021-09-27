using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks
{
    public class ProductsMock
    {
        public static void Init(AppDbContext db)
        {
            #region Товар1 Кроссовки Nike
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Брюки KORPO COLLEZIONI").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Зимняя куртка мужская SHARK FORCE") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Пальто Tom Farr") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Пальто Tom Farr").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Шляпа ARMANI") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Шляпа ARMANI").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        db.Products.Add(new Product(productArticle));
                    }
                }
            }
            #endregion

            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            #region Товар1 Кроссовки Nike
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Брюки KORPO COLLEZIONI").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Зимняя куртка мужская SHARK FORCE") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Пальто Tom Farr") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Пальто Tom Farr").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.Products.FirstOrDefault(i => i.Article.Model.Name == "Шляпа ARMANI") == null)
            {
                var productArticles = db.ProductArticles.Where(i => i.Model.Name == "Шляпа ARMANI").ToArray();
                foreach (var productArticle in productArticles)
                {
                    for (int i = 0; i < productArticle.Count; i++)
                    {
                        await db.Products.AddAsync(new Product(productArticle));
                    }
                }
            }
            #endregion

            await db.SaveChangesAsync();
        }
    }
}
