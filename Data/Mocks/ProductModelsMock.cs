using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Entities;

namespace WebStore.Data.Mocks
{
    public class ProductModelsMock
    {
        public static void Init(AppDbContext db)
        {
            #region Товар1 Кроссовки Nike
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                db.ProductModels.Add(
                new ProductModel("Кроссовки Nike Air Zoom Pegasus", 9990, 365
                   , "США", true, "Nike", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Кроссовки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Назначение","бег" },
                       { "Застежка","шнуровка" },
                   }
                   , new List<string>()
                   {
                       "Текстиль",
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus3.jfif"),
                   }));
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                db.ProductModels.Add(
                    new ProductModel("Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K", 7399, 180
                   , "Вьетнам", false, "Adidas", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Ботинки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                   new Dictionary<string, string>()
                   {
                       { "Материал подкладки","текстиль" },
                       { "Материал подошвы","резина" },
                   }
                   , new List<string>()
                   {
                       "Синтетика 60%",
                       "Текстиль 40%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K1.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K2.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K3.jpg"),
                   }));
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                db.ProductModels.Add(
                    new ProductModel("Джинсы Levi's 514™ Straight (Big & Tall)", 5673, 300
                   , "США", true, "Levi's", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Джинсы"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Модель","прямые" },
                       { "Посадка","низкая" },
                       { "Застежка","молния" },
                       { "Передние карманы","втачные" },
                       { "Задние карманы","накладные" },
                   }
                   , new List<string>()
                   {
                       "Хлопок 99%",
                       "Эластан 1%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)2.jfif"),
                   }));
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                db.ProductModels.Add(new ProductModel("Брюки KORPO COLLEZIONI", 6560, 600
                   , "Италия", false, "KORPO", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Брюки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                   new Dictionary<string, string>()
                   {
                       { "Стиль","Casual, Классический" },
                       { "Расцветка","Однотонная" },
                       { "Форма","Прямая" },
                   }
                   , new List<string>()
                   {
                       "Шерсть 60%",
                       "Полиэстер 38%",
                       "Эластан 2%",
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI1.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI2.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI3.png"),
                   }));
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Зимняя куртка мужская SHARK FORCE") == null)
            {
                db.ProductModels.Add(new ProductModel("Зимняя куртка мужская SHARK FORCE", 19080, 365 * 3
                   , "Пекин", true, "SHARK FORCE", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Куртки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210131.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210132.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210133.jfif"),
                   }));
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Пальто Tom Farr") == null)
            {
                db.ProductModels.Add(new ProductModel("Пальто Tom Farr", 10890, 90
                   , "Россия", false, "Tom Farr", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Пальто"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","весна, осень" },
                       { "Застежка","отсутствует" },
                   }
                   , new List<string>()
                   {
                       "Полиэстер 70%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr3.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr4.jfif"),
                   }));
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                db.ProductModels.Add(new ProductModel("Кепка мужская Denkor Восьмиклинка-Хулиганка", 1190, 12
                   , "Россия", true, "Denkor", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Кепки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","лето" },
                   }
                   , new List<string>()
                   {
                       "Ткань"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка2.jfif"),
                   }));
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Шляпа ARMANI") == null)
            {
                db.ProductModels.Add(new ProductModel("Шляпа ARMANI", 16730, 300
                   , "Италия", true, "ARMANI", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Шляпа"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI1.jfif"),
                   }));
            }
            #endregion

            db.SaveChanges();
        }
        public static async Task InitAsync(AppDbContext db)
        {
            #region Товар1 Кроссовки Nike
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Кроссовки Nike Air Zoom Pegasus") == null)
            {
                await db.ProductModels.AddAsync(
                new ProductModel("Кроссовки Nike Air Zoom Pegasus", 9990, 365
                   , "США", true, "Nike", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Кроссовки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Назначение","бег" },
                       { "Застежка","шнуровка" },
                   }
                   , new List<string>()
                   {
                       "Текстиль",
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\NIKE Air Zoom Pegasus3.jfif"),
                   }));
            }
            #endregion

            #region Товар2 Ботинки Adidas
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K") == null)
            {
                await db.ProductModels.AddAsync(
                    new ProductModel("Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K", 7399, 180
                   , "Вьетнам", false, "Adidas", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Ботинки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                   new Dictionary<string, string>()
                   {
                       { "Материал подкладки","текстиль" },
                       { "Материал подошвы","резина" },
                   }
                   , new List<string>()
                   {
                       "Синтетика 60%",
                       "Текстиль 40%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K1.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K2.jpg"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Ботинки для девочек adidas Terrex Trailmaker Mid R.RDY K3.jpg"),
                   }));
            }
            #endregion

            #region Товар3 Джинсы Levi's
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Джинсы Levi's 514™ Straight (Big & Tall)") == null)
            {
                await db.ProductModels.AddAsync(
                 new ProductModel("Джинсы Levi's 514™ Straight (Big & Tall)", 5673, 300
                   , "США", true, "Levi's", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Джинсы"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Модель","прямые" },
                       { "Посадка","низкая" },
                       { "Застежка","молния" },
                       { "Передние карманы","втачные" },
                       { "Задние карманы","накладные" },
                   }
                   , new List<string>()
                   {
                       "Хлопок 99%",
                       "Эластан 1%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall).jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Джинсы Levi's 514™ Straight (Big & Tall)2.jfif"),
                   }));
            }
            #endregion

            #region Товар4 Брюки KORPO
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Брюки KORPO COLLEZIONI") == null)
            {
                await db.ProductModels.AddAsync(new ProductModel("Брюки KORPO COLLEZIONI", 6560, 600
                   , "Италия", false, "KORPO", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Брюки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                   new Dictionary<string, string>()
                   {
                       { "Стиль","Casual, Классический" },
                       { "Расцветка","Однотонная" },
                       { "Форма","Прямая" },
                   }
                   , new List<string>()
                   {
                       "Шерсть 60%",
                       "Полиэстер 38%",
                       "Эластан 2%",
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI1.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI2.png"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Брюки KORPO COLLEZIONI3.png"),
                   }));
            }
            #endregion

            #region Товар5 Куртка SHARK FORCE
            if (db.ProductModels.FirstOrDefault(i => i.Name == "") == null)
            {
                await db.ProductModels.AddAsync(new ProductModel("Зимняя куртка мужская SHARK FORCE", 19080, 365 * 3
                   , "Пекин", true, "SHARK FORCE", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Куртки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 21013.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210131.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210132.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Зимняя куртка мужская SHARK FORCE 210133.jfif"),
                   }));
            }
            #endregion

            #region Товар6 Пальто Tom Farr
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Пальто Tom Farr") == null)
            {
                await db.ProductModels.AddAsync(new ProductModel("Пальто Tom Farr", 10890, 90
                   , "Россия", false, "Tom Farr", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Пальто"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","весна, осень" },
                       { "Застежка","отсутствует" },
                   }
                   , new List<string>()
                   {
                       "Полиэстер 70%"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr2.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr3.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Пальто Tom Farr4.jfif"),
                   }));
            }
            #endregion

            #region Товар7 Кепка Denkor
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Кепка мужская Denkor Восьмиклинка-Хулиганка") == null)
            {
                await db.ProductModels.AddAsync(new ProductModel("Кепка мужская Denkor Восьмиклинка-Хулиганка", 1190, 12
                   , "Россия", true, "Denkor", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Кепки"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                   new Dictionary<string, string>()
                   {
                       { "Сезон","лето" },
                   }
                   , new List<string>()
                   {
                       "Ткань"
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка1.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Кепка мужская Denkor Восьмиклинка-Хулиганка2.jfif"),
                   }));
            }
            #endregion

            #region Товар8 Шляпа ARMANI
            if (db.ProductModels.FirstOrDefault(i => i.Name == "Шляпа ARMANI") == null)
            {
                await db.ProductModels.AddAsync(new ProductModel("Шляпа ARMANI", 16730, 300
                   , "Италия", true, "ARMANI", db.ProductSubcategories.SingleOrDefault(i => i.Name == "Шляпа"),
                   File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                   new Dictionary<string, string>()
                   {
                   }
                   , new List<string>()
                   {
                   }
                   , new List<byte[]>()
                   {
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI.jfif"),
                       File.ReadAllBytes(Environment.CurrentDirectory + @"\wwwroot\ProductPhotos\Шляпа ARMANI1.jfif"),
                   }));
            }
            #endregion

            await db.SaveChangesAsync();
        }
    }
}
