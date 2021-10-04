using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Identity;
using WebStore.Data.Mocks;
using WebStore.Data.Repositories.DeliveryRepository;

namespace WebStore.Data
{
    public class AppDbContextSeed
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppIdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppIdentityRole>>();

                ProductCategoriesMock.Init(db);
                ProductSubcategoriesMock.Init(db);

                DeliveriesMock.Init(db);

                ProductModelsMock.Init(db);
                ProductArticlesMock.Init(db);
                ProductsMock.Init(db);

                OrdersMock.Init(db);
                RolesMock.Init(roleManager);
                UsersMock.Init(db, userManager);
            }            
        }
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppIdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppIdentityRole>>();

                await ProductCategoriesMock.InitAsync(db);
                await ProductSubcategoriesMock.InitAsync(db);

                await DeliveriesMock.InitAsync(db);

                await ProductModelsMock.InitAsync(db);
                await ProductArticlesMock.InitAsync(db);
                await ProductsMock.InitAsync(db);

                await OrdersMock.InitAsync(db);
                await RolesMock.InitAsync(roleManager);
                await UsersMock.InitAsync(db, userManager);
            }
        }
    }
}
