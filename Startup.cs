using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebStore.Configurations;
using WebStore.Domain.Consts;
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
            services.AddMvc();
            services.AddServerSideBlazor();
            services.AddAdvancedDependencyInjection();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<TokenAuthenticationStateService>();
            services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateService>();
            services.AddScoped<ServerAuthenticationStateProvider, TokenAuthenticationStateService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = Configuration["JwtTokenParametrs:Audience"];
                    options.ClaimsIssuer = Configuration["JwtTokenParametrs:Issuer"];
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtTokenParametrs:Secret"])),
                        SaveSigninToken = true,
                        RequireAudience = true,
                        RequireExpirationTime = true,
                        AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JwtTokenParametrs:Issuer"],
                        ValidAudience = Configuration["JwtTokenParametrs:Audience"]
                    };
                    options.SaveToken = true;
                    options.Validate(JwtBearerDefaults.AuthenticationScheme);
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConst.Guest, builder => builder.RequireAssertion(x => !x.User.Identity.IsAuthenticated));
                options.AddPolicy(PolicyConst.Admin, bulder => bulder.RequireClaim(ClaimTypes.Role, RoleConst.Admin));
                options.AddPolicy(PolicyConst.Moderator, bulder => bulder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RoleConst.Moderator) || x.User.HasClaim(ClaimTypes.Role, RoleConst.Admin)));
                options.AddPolicy(PolicyConst.User, bulder => bulder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RoleConst.Moderator) || x.User.HasClaim(ClaimTypes.Role, RoleConst.Admin) || x.User.HasClaim(ClaimTypes.Role, RoleConst.User)));
            });

            services.AddAppDbContext(Configuration);
            services.AddAppValidation();
            if (Env.IsDevelopment())
                services.AddAppMockDependencies();

            services.AddWMBSC();
            services.AddHttpClient();
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
