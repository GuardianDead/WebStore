using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class TokenAuthenticationStateService : ServerAuthenticationStateProvider
    {
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILocalStorageService localStorage;
        private readonly ISessionStorageService sessionStorage;

        public TokenAuthenticationStateService(
            ITokenService tokenService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILocalStorageService localStorage,
            ISessionStorageService sessionStorage)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.localStorage = localStorage;
            this.sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await localStorage.GetItemAsStringAsync("userAccessToken");
            var rememberUser = true;
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = await sessionStorage.GetItemAsStringAsync("userAccessToken");
                rememberUser = false;
                if (string.IsNullOrWhiteSpace(accessToken))
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var jwtSecurityToken = tokenService.ReadAccessToken(accessToken);
            var user = await userManager.FindByEmailAsync(jwtSecurityToken.Payload.Claims.Single(claim => claim.Type == "email").Value);
            if (user is null)
                throw new ArgumentNullException("Запрошенного пользователя нет в системе");

            var authenticationState = new AuthenticationState(await signInManager.CreateUserPrincipalAsync(user));
            if (tokenService.IsAccessTokenExpired(accessToken))
                await SetAuthenticationStateAsync(authenticationState, rememberUser);

            return authenticationState;
        }
        public async Task SetAuthenticationStateAsync(AuthenticationState authenticationStateTask, bool rememberUser)
        {
            var claimsPrincipal = authenticationStateTask.User;
            var user = await userManager.GetUserAsync(claimsPrincipal);
            if (user is null)
                throw new ArgumentNullException("Данный пользователь не найден");

            if (rememberUser)
                await localStorage.SetItemAsStringAsync("userAccessToken", tokenService.CreateAccessToken(claimsPrincipal));
            else
                await sessionStorage.SetItemAsStringAsync("userAccessToken", tokenService.CreateAccessToken(claimsPrincipal));
        }
        public async Task LogoutAuthenticationStateAsync()
        {
            await localStorage.RemoveItemAsync("userAccessToken");
            await sessionStorage.RemoveItemAsync("userAccessToken");
        }
    }
}
