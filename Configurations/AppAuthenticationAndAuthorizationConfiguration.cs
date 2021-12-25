using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebStore.Domain.Consts;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore.Configurations
{
    public static class AppAuthenticationAndAuthorizationConfiguration
    {
        public static IServiceCollection AddAppAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<TokenAuthenticationStateService>();
            services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateService>();
            services.AddScoped<ServerAuthenticationStateProvider, TokenAuthenticationStateService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = configuration["JwtTokenParametrs:Audience"];
                    options.ClaimsIssuer = configuration["JwtTokenParametrs:Issuer"];
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtTokenParametrs:Secret"])),
                        SaveSigninToken = true,
                        RequireAudience = true,
                        RequireExpirationTime = true,
                        AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JwtTokenParametrs:Issuer"],
                        ValidAudience = configuration["JwtTokenParametrs:Audience"]
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

            return services;
        }
    }
}
