using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace WebStore.Domain.Configurations
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
