using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using WebStore.Data;

namespace WebStore.Configurations
{
    public static class MockDependenciesConfiguration
    {
        public static IServiceCollection AddMockDependencies(this IServiceCollection services)
        {
            services.Scan(
                options =>
                {
                    options.FromCallingAssembly()

                    .AddClasses(i => i.Where(c => c.Name.EndsWith("Mock")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
                });

            services.AddScoped<AppDbContextSeed>();

            return services;
        }
    }
}
