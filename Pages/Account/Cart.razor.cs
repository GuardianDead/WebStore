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
    public class CartBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }

        public bool IsSelectedAll { get; set; } = true;

        public decimal totalCost;

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
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.ProductArticle.Count == 0);
            await Db.SaveChangesAsync();
            currentUser.Cart.Products.ForEach(cartProduct => cartProduct.IsSelected = true);
            UpdateTotalCurrentCostCart();
        }

        public void UpdateTotalCurrentCostCart() => totalCost = currentUser.Cart.Products.Where(cartProduct => cartProduct.IsSelected)
            .Sum(cartProduct => cartProduct.ProductArticle.Model.Price * cartProduct.Count);

        public Task AddProductInFavoritesAsync(CartProduct cartProduct)
        {
            var addedFavoriteProduct = currentUser.ListFavourites.Products
                .SingleOrDefault(favoriteProduct => favoriteProduct.ProductArticle.Id == cartProduct.ProductArticle.Id);
            if (currentUser.ListFavourites.Products.Contains(addedFavoriteProduct))
                return Task.CompletedTask;
            currentUser.ListFavourites.Products.Add(new FavoritesListProduct(cartProduct.ProductArticle));
            return Db.SaveChangesAsync();
        }
        public Task RemoveProductFromCartAsync(CartProduct cartProduct)
        {
            if (!currentUser.Cart.Products.Contains(cartProduct))
                return Task.CompletedTask;
            currentUser.Cart.Products.Remove(cartProduct);
            return Db.SaveChangesAsync();
        }
        public Task RemoveProductFromFavoritesAsync(CartProduct cartProduct)
        {
            var removedFavoriteProduct = currentUser.ListFavourites.Products
                    .SingleOrDefault(cartProductList => cartProductList.ProductArticle.Id == cartProduct.ProductArticle.Id);
            if (!currentUser.ListFavourites.Products.Contains(removedFavoriteProduct))
                return Task.CompletedTask;
            currentUser.ListFavourites.Products.Remove(removedFavoriteProduct);
            return Db.SaveChangesAsync();
        }

        public void Increment(CartProduct cartProduct)
        {
            if (cartProduct.Count == cartProduct.ProductArticle.Count)
                return;
            if (cartProduct.Count == 999)
                return;
            cartProduct.Count++;
            UpdateTotalCurrentCostCart();
        }
        public void Decrement(CartProduct cartProduct)
        {
            if (cartProduct.Count == 1)
                return;
            --cartProduct.Count;
            UpdateTotalCurrentCostCart();
        }

        public void SelectAllToggle()
        {
            if (IsSelectedAll && currentUser.Cart.Products.All(cartProduct => cartProduct.IsSelected))
            {
                IsSelectedAll = false;
                currentUser.Cart.Products.ForEach(cartProduct => cartProduct.IsSelected = false);
            }
            else if (!IsSelectedAll && currentUser.Cart.Products.Any(cartProduct => !cartProduct.IsSelected))
            {
                IsSelectedAll = true;
                currentUser.Cart.Products.ForEach(cartProduct => cartProduct.IsSelected = true);
            }
            UpdateTotalCurrentCostCart();
        }
        public void ProductSelecterToggle(CartProduct cartProduct)
        {
            cartProduct.IsSelected = !cartProduct.IsSelected;
            if (!IsSelectedAll && currentUser.Cart.Products.All(cartProduct => cartProduct.IsSelected))
                IsSelectedAll = true;
            else if (IsSelectedAll && currentUser.Cart.Products.Any(cartProduct => !cartProduct.IsSelected))
                IsSelectedAll = false;
            UpdateTotalCurrentCostCart();
        }
    }
}
