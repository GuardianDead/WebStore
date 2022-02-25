using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
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

        public List<FavoritesListProduct> favoritesProductsList;
        public List<CartProduct> cartProductsList;
        private ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationState).User;
            var userEmail = currentUserState.Claims.ToList().Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = await Db.Users
                .Include(user => user.Cart.Products)
                .Include(user => user.ListFavourites.Products)
                .SingleAsync(user => user.Email == userEmail);
            favoritesProductsList = Db.FavoritesListProducts
                .Include(p => p.ProductArticle.Model)
                .Where(p => currentUser.ListFavourites.Products.Contains(p))
                .ToList();
            cartProductsList = Db.CartProducts
                .Include(p => p.ProductArticle.Model)
                .Where(p => currentUser.Cart.Products.Contains(p))
                .ToList();
        }

        public void AddProductInUserCart(FavoritesListProduct favoriteProduct)
        {
            cartProductsList.Add(new CartProduct(Db.ProductArticles.Include(productArticle => productArticle.Model).First(productArticle => productArticle.Id == favoriteProduct.ProductArticle.Id), 1));
            currentUser.Cart.Products = cartProductsList;
            Db.SaveChanges();

        }
        public void AddProductInUserFavorites(FavoritesListProduct favoriteProduct)
        {
            favoritesProductsList.Add(new FavoritesListProduct(Db.ProductArticles.Include(productArticle => productArticle.Model).First(productArticle => productArticle.Id == favoriteProduct.ProductArticle.Id)));
            currentUser.ListFavourites.Products = favoritesProductsList;
            Db.SaveChanges();
        }
        public void RemoveProductInUserCart(FavoritesListProduct favoriteProduct)
        {
            cartProductsList.Remove(cartProductsList.First(cartProductList => cartProductList.ProductArticle.Id == favoriteProduct.ProductArticle.Id));
            currentUser.Cart.Products = cartProductsList;
            Db.SaveChanges();
        }
        public void RemoveProductInUserFavorites(FavoritesListProduct favoriteProduct)
        {
            favoritesProductsList.Remove(favoritesProductsList.First(favoriteProduct => favoriteProduct.ProductArticle.Id == favoriteProduct.ProductArticle.Id));
            currentUser.ListFavourites.Products = favoritesProductsList;
            Db.SaveChanges();
        }
    }
}
