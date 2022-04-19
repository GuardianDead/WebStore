using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Configurations;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(optins => optins.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddServerSideBlazor();
            services.AddAdvancedDependencyInjection();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();

            services.AddSingleton<ITrackNumberService, TrackNumberService>();
            services.AddAppAuthenticationAndAuthorization(configuration: Configuration);
            services.AddAppDbContext(configuration: Configuration);
            services.AddAppValidation();
            if (Env.IsDevelopment())
                services.AddAppMockDependencies();

            services.AddWMBSC(false);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.AddAppMock();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAdvancedDependencyInjection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
