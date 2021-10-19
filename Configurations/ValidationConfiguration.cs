using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebStore.Configurations
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddAppValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(i =>
            {
                i.DisableDataAnnotationsValidation = true;
                i.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            return services;
        }
    }
}
