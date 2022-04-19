using Innofactor.EfCoreJsonValueConverter;
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
            builder.Entity<ProductModel>().Property(o => o.Gender).HasConversion(new EnumToStringConverter<GenderType>());
            builder.Entity<Order>().Property(o => o.PaymentMethod).HasConversion(new EnumToStringConverter<PaymentMethodType>());
            builder.Entity<Order>().Property(o => o.Status).HasConversion(new EnumToStringConverter<OrderStatusType>());
            builder.Entity<Delivery>().Property(o => o.DeliveryMethod).HasConversion(new EnumToStringConverter<DeliveryMethodType>());

            builder.AddJsonFields();
            builder.Entity<ProductModel>(i =>
            {
                i.Property(o => o.Photos).HasJsonValueConversion();
                i.Property(o => o.Features).HasJsonValueConversion();
                i.Property(o => o.Materials).HasJsonValueConversion();
                i.Property(o => o.Photos).HasJsonValueConversion();
            });
            builder.Entity<SoldProduct>(i =>
            {
                i.Property(o => o.Order).HasJsonValueConversion();
                i.Property(o => o.Product).HasJsonValueConversion();
            });

            builder.Entity<Delivery>().Property(p => p.Cost).HasColumnType("decimal(18,4)");
            builder.Entity<Order>().Property(p => p.TotalCost).HasColumnType("decimal(18,4)");
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
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<FavoritesProductsList> FavoriteLists { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
    }
}
