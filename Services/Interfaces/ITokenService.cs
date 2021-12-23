using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebStore.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateAccessToken(ClaimsPrincipal claimsPrincipal);
        public JwtSecurityToken ReadAccessToken(string accessToken);
        public (bool IsValid, string ErrorMessage) ValidateAccessToken(string accessToken);
        public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string accessToken);
        public bool IsAccessTokenExpired(string accessToken);
    }
}
