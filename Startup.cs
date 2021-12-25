using WebStore.Configurations;

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
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddServerSideBlazor();
            services.AddAdvancedDependencyInjection();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();

            services.AddWMBSC();

            services.AddAppAuthenticationAndAuthorization(configuration: Configuration);
            services.AddAppDbContext(configuration: Configuration);
            services.AddAppValidation();
            if (Env.IsDevelopment())
                services.AddAppMockDependencies();
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
