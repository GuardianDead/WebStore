using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Data;
using WebStore.Data.Identity;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddServerSideBlazor();

            services.AddDbContext<AppDbContext>(i => i.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(option =>
            {
                option.Password.RequireNonAlphanumeric = false;
            })
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders()
              .AddSignInManager();

            services.AddFluentValidation(i =>
            {
                i.DisableDataAnnotationsValidation = true;
                i.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
