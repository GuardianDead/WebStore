using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebStore.Data.Entities;
using WebStore.Domain.Types;

namespace WebStore.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().Property(o => o.Gender).HasConversion(new EnumToStringConverter<GenderType>());
            builder.Entity<Order>().Property(o => o.PaymentMethod).HasConversion(new EnumToStringConverter<PaymentMethodType>());
            builder.Entity<Order>().Property(o => o.Status).HasConversion(new EnumToStringConverter<OrderStatusType>());
            builder.Entity<Delivery>().Property(o => o.DeliveryMethod).HasConversion(new EnumToStringConverter<DeliveryMethodType>());

            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelFeature> ProductModelFeatures { get; set; }
        public DbSet<ProductModelMaterial> ProductModelMaterials { get; set; }
        public DbSet<ProductModelPhoto> ProductModelPhotos { get; set; }
        public DbSet<ProductArticle> ProductArticles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<FavoriteList> FavoriteLists { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
    }
}
