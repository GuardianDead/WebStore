using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace WebStore.Configurations
{
    public static class ValidationConfiguration
    {

        public static IServiceCollection AddAppValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(option =>
            {
                option.ValidatorOptions.LanguageManager.Culture = CultureInfo.GetCultureInfo("ru-RU", true);
                option.ValidatorOptions.LanguageManager.Enabled = true;
                option.ImplicitlyValidateChildProperties = true;
                option.ImplicitlyValidateRootCollectionElements = true;
                option.LocalizationEnabled = true;
                option.ValidatorOptions.Severity = Severity.Warning;
                //option.DisableDataAnnotationsValidation = true;
                option.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            return services;
        }
    }
}
