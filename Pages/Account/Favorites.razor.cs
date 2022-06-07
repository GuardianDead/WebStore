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

        private ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationState).User;
            var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = await Db.Users
                .Include(user => user.Cart.Products)
                    .ThenInclude(cartProducts => cartProducts.Article.Model)
                .Include(user => user.FavoriteList.Products)
                    .ThenInclude(favoriteProducts => favoriteProducts.Article.Model)
                .SingleAsync(user => user.Email == userEmail);
        }

        public IEnumerable<IGrouping<string, ProductModel>> GetDistinctProductsByModel() => currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model);

        public async Task AddProductInCartAsync(ProductModel productModel)
        {
            var addedFirstProductArticleOfModel = Db.ProductArticles
                .Include(productArticle => productArticle.Model)
                .First(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Any(product => product.Article.Id == productArticle.Id && !product.IsSold));
            currentUser.Cart.Products.Add(new CartProduct(addedFirstProductArticleOfModel, 1));
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());

        }
        public async Task RemoveProductFromCartAsync(ProductModel productModel)
        {
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task RemoveProductFromFavoritesAsync(ProductModel productModel)
        {
            currentUser.FavoriteList.Products.RemoveAll(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
    }
}
