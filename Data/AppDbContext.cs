using Innofactor.EfCoreJsonValueConverter;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebStore.Data.Entities;
using WebStore.Data.Identity;

namespace WebStore.Data
{
    public class AppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
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
            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductArticle> ProductArticles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }

        public DbSet<ProductSold> ProductSolds { get; set; }
    }
}
