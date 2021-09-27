using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks
{
    public class ProductArticlesMock
    {
        public static void Init(AppDbContext db)
        {
            Random random = new Random();
            int size;
            int count;

            #region Товар1 Кроссовки Nike
            if(db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                size = 38;
                count = 9;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Кроссовки Nike Air Zoom Pegasus"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                size = 35;
                count = 4;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K"),
                        size, "Бежевый", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                size = 30;
                count = 7;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Джинсы Levi's 514™ Straight (Big & Tall)"),
                        size, "Светло синий", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                size = 42;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Брюки KORPO COLLEZIONI"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE") == null)
            {
                size = 54;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Зимняя куртка мужская SHARK FORCE"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Пальто Tom Farr") == null)
            {
                size = 42;
                count = 3;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Пальто Tom Farr"),
                        size, "Серое", random.Next(50, 101)));
                    size += 2;
                    count--;
                }
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                size = 58;
                count = 2;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка"),
                        size, "Серое", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Шляпа ARMANI") == null)
            {
                size = 56;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Шляпа ARMANI"),
                        size, "Желтый", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            Random random = new Random();
            int size;
            int count;

            #region Товар1 Кроссовки Nike
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                size = 38;
                count = 9;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Кроссовки Nike Air Zoom Pegasus"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                size = 35;
                count = 4;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K"),
                        size, "Бежевый", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                size = 30;
                count = 7;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Джинсы Levi's 514™ Straight (Big & Tall)"),
                        size, "Светло синий", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                size = 42;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Брюки KORPO COLLEZIONI"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Зимняя куртка мужская SHARK FORCE") == null)
            {
                size = 54;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Зимняя куртка мужская SHARK FORCE"),
                        size, "Черный", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Пальто Tom Farr") == null)
            {
                size = 42;
                count = 3;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Пальто Tom Farr"),
                        size, "Серое", random.Next(50, 101)));
                    size += 2;
                    count--;
                }
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                size = 58;
                count = 2;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка"),
                        size, "Серое", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.ProductArticles.FirstOrDefault(i => i.Model.Name == "Шляпа ARMANI") == null)
            {
                size = 56;
                count = 1;
                while (count != 0)
                {
                    db.ProductArticles.Add(new ProductArticle(db.ProductModels.FirstOrDefault(i => i.Name == "Шляпа ARMANI"),
                        size, "Желтый", random.Next(50, 101)));
                    size += 1;
                    count--;
                }
            }
            #endregion

            await db.SaveChangesAsync();
        }
    }
}
