using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebStore.Data;

namespace WebStore.Configurations
{
    public static class MockConfiguration
    {
        public static IApplicationBuilder AddAppMock(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbSeed = scope.ServiceProvider.GetRequiredService<AppDbContextSeed>();
                if (!dbSeed.SeedAsync().GetAwaiter().GetResult())
                {
                    throw new Exception("Ошибка при инициализации базы данных");
                }
                return app;
            }
        }
    }
}
