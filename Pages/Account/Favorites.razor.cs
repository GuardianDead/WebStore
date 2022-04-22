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
                .Include(user => user.ListFavourites.Products)
                    .ThenInclude(favoriteProducts => favoriteProducts.Article.Model)
                .SingleAsync(user => user.Email == userEmail);
        }

        public IEnumerable<IGrouping<string, ProductModel>> GetDistinctProductsByModel() => currentUser.ListFavourites.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model);

        public void AddProductInCart(ProductModel productModel)
        {
            if (!currentUser.Cart.Products.Any(cartProduct => cartProduct.Article.Model.Id == productModel.Id))
                return;
            var addedFirstProductArticleOfModel = Db.ProductArticles
                .Include(productArticle => productArticle.Model)
                .First(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0);
            currentUser.Cart.Products.Add(new CartProduct(addedFirstProductArticleOfModel, 1));
            Db.SaveChanges();

        }
        public void RemoveProductFromCart(ProductModel productModel)
        {
            if (!currentUser.Cart.Products.Any(cartProduct => cartProduct.Article.Model.Id == productModel.Id))
                return;
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
        }
        public void RemoveProductFromFavorites(ProductModel productModel)
        {
            if (!currentUser.ListFavourites.Products.Any(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id))
                return;
            currentUser.ListFavourites.Products.RemoveAll(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
        }
    }
}
