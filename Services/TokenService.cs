using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateAccessToken(ClaimsPrincipal claimsPrincipal)
        {
            var tokenDecriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimsPrincipal.Claims, JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role),
                Audience = configuration["JwtTokenParametrs:Audience"],
                Issuer = configuration["JwtTokenParametrs:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration["JwtTokenParametrs:RefreshIntervalAccessTokenInMinutes"])),
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtTokenParametrs:Secret"])), SecurityAlgorithms.HmacSha256),
            };

            return jwtSecurityTokenHandler.CreateEncodedJwt(tokenDecriptor);
        }
        public JwtSecurityToken ReadAccessToken(string accessToken)
        {
            var (IsValid, ErrorMessage) = ValidateAccessToken(accessToken);
            if (!IsValid)
                throw new SecurityTokenException(ErrorMessage);

            return jwtSecurityTokenHandler.ReadJwtToken(accessToken);
        }
        public (bool IsValid, string ErrorMessage) ValidateAccessToken(string accessToken)
        {
            if (!jwtSecurityTokenHandler.CanReadToken(accessToken))
                return (false, "Токен неверного формата");

            var tokenValidationParameters = new TokenValidationParameters()
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
            try
            {
                jwtSecurityTokenHandler.ValidateToken(accessToken, tokenValidationParameters, out _);
            }
            catch (SecurityTokenExpiredException)
            {
            }

            return (true, string.Empty);
        }
        public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string accessToken)
        {
            var (IsValid, ErrorMessage) = ValidateAccessToken(accessToken);
            if (!IsValid)
                throw new SecurityTokenException(ErrorMessage);

            var tokenValidationParameters = new TokenValidationParameters()
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
            return jwtSecurityTokenHandler.ValidateToken(accessToken, tokenValidationParameters, out _);
        }
        public bool IsAccessTokenExpired(string accessToken)
        {
            return ReadAccessToken(accessToken).ValidTo < DateTime.UtcNow;
        }
    }
}
