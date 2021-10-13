using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Data;
using WebStore.Data.Identity;
using WebStore.Data.Repositories;
using WebStore.Data.Repositories.AddressRepository;
using WebStore.Data.Repositories.AppIdentityUserRepository;
using WebStore.Data.Repositories.CartProductRepository;
using WebStore.Data.Repositories.CartRepository;
using WebStore.Data.Repositories.CategoryRepository;
using WebStore.Data.Repositories.DeliveryRepository;
using WebStore.Data.Repositories.FavoritesListProductRepository;
using WebStore.Data.Repositories.FavoritesListRepository;
using WebStore.Data.Repositories.ListFavoritesRepository;
using WebStore.Data.Repositories.OrderHistoryRepository;
using WebStore.Data.Repositories.OrderRepository;
using WebStore.Data.Repositories.ProductArticleRepository;
using WebStore.Data.Repositories.ProductModelRepository;
using WebStore.Data.Repositories.ProductRepository;
using WebStore.Data.Repositories.ProductSoldRepository;
using WebStore.Data.Repositories.RoleRepository;
using WebStore.Data.Repositories.SubcategoryRepository;
using WebStore.Data.Repositories.UserRepository;
using WebStore.Services.RoleService;
using WebStore.Services.UserService;

namespace WebStore.Domain.Configurations
{
    public static class AppDbContextConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICartProductRepository, CartProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IFavoritesListProductRepository, FavoritesListProductRepository>();
            services.AddScoped<IFavoritesListRepository, FavoritesListRepository>();
            services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductArticleRepository, ProductArticleRepository>();
            services.AddScoped<IProductModelRepository, ProductModelRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductSoldRepository, ProductSoldRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            return services;
        }
    }
}
