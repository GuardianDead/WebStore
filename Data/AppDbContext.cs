using Innofactor.EfCoreJsonValueConverter;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;
using WebStore.Data.Entities;
using WebStore.Data.Identity;

namespace WebStore.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddJsonFields();
            builder.Entity<ProductModel>(i =>
            {
                i.Property(o => o.Photos).HasJsonValueConversion();
                i.Property(o => o.Features).HasJsonValueConversion();
                i.Property(o => o.Materials).HasJsonValueConversion();
                i.Property(o => o.Photos).HasJsonValueConversion();
            });
            builder.Entity<ProductSold>(i =>
            {
                i.Property(o => o.Order).HasJsonValueConversion();
                i.Property(o => o.Product).HasJsonValueConversion();
            });
            builder.Entity<Delivery>().Property(p => p.DeliveryCost).HasColumnType("decimal(18,4)");
            builder.Entity<Order>().Property(p => p.SummaryCost).HasColumnType("decimal(18,4)");
            builder.Entity<ProductModel>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductArticle> ProductArticles { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<ProductSold> ProductsSold { get; set; }

        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<FavoritesList> FavoritesLists { get; set; }
        public DbSet<FavoritesListProduct> FavoritesListProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
    }
}
