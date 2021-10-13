using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using WebStore.Data;
using WebStore.Data.Identity;
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

            services.Scan(
                options =>
                {
                    options.FromCallingAssembly()

                    .AddClasses(i => i.Where(c => c.Name.EndsWith("Repository")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
                });

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
