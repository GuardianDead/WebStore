using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages.Account
{
    public class FavoritesBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }

        private ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationState).User;
            var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = await Db.Users
                .Include(user => user.Cart.Products)
                    .ThenInclude(cartProducts => cartProducts.ProductArticle.Model)
                .Include(user => user.ListFavourites.Products)
                    .ThenInclude(favoriteProducts => favoriteProducts.ProductArticle.Model)
                .SingleAsync(user => user.Email == userEmail);
        }

        public Task AddProductInCartAsync(FavoritesListProduct favoriteProduct)
        {
            var addedCartProduct = currentUser.Cart.Products
                .SingleOrDefault(cartProduct => cartProduct.ProductArticle.Id == favoriteProduct.ProductArticle.Id);
            if (currentUser.Cart.Products.Contains(addedCartProduct))
                return Task.CompletedTask;
            currentUser.Cart.Products.Add(new CartProduct(favoriteProduct.ProductArticle, 1));
            return Db.SaveChangesAsync();

        }
        public Task RemoveProductFromCartAsync(FavoritesListProduct favoriteProduct)
        {
            var removedCartProduct = currentUser.Cart.Products
                    .SingleOrDefault(cartProductList => cartProductList.ProductArticle.Id == favoriteProduct.ProductArticle.Id);
            if (removedCartProduct is null)
                return Task.CompletedTask;
            currentUser.Cart.Products.Remove(removedCartProduct);
            return Db.SaveChangesAsync();
        }
        public Task RemoveProductFromFavoritesAsync(FavoritesListProduct favoriteProduct)
        {
            if (!currentUser.ListFavourites.Products.Contains(favoriteProduct))
                return Task.CompletedTask;
            currentUser.ListFavourites.Products.Remove(favoriteProduct);
            return Db.SaveChangesAsync();
        }
    }
}
