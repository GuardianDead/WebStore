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

        [Inject] public NavigationManager NavigationManager { get; set; }
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
                    .ThenInclude(cartProducts => cartProducts.Article.Model)
                .Include(user => user.FavoriteList.Products)
                    .ThenInclude(favoriteProducts => favoriteProducts.Article.Model)
                .SingleAsync(user => user.Email == userEmail);
            await ClearCartAsync();
            currentUser.Cart.Products.ForEach(cartProduct => cartProduct.IsSelected = true);
            await Db.SaveChangesAsync();
            UpdateTotalCurrentCostCart();
        }

        public async Task ClearCartAsync()
        {
            foreach (var cartProduct in currentUser.Cart.Products)
            {
                var cartProductCount = await Db.Products.CountAsync(product => product.Article.Id == cartProduct.Article.Id && !product.IsSold);
                if (cartProductCount == 0)
                    currentUser.Cart.Products.Remove(cartProduct);
            }
        }
        public void UpdateTotalCurrentCostCart() => totalCost = currentUser.Cart.Products.Where(cartProduct => cartProduct.IsSelected)
            .Sum(cartProduct => cartProduct.Article.Model.Price * cartProduct.Count);

        public async Task AddProductInFavoritesAsync(CartProduct cartProduct)
        {
            currentUser.FavoriteList.Products.Add(new FavoriteProduct(cartProduct.Article));
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task RemoveProductFromCartAsync(CartProduct cartProduct)
        {
            currentUser.Cart.Products.Remove(cartProduct);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task RemoveProductFromFavoritesAsync(CartProduct cartProduct)
        {
            var removedFavoriteProduct = currentUser.FavoriteList.Products.SingleOrDefault(favoriteProduct => favoriteProduct.Article.Id == cartProduct.Article.Id);
            currentUser.FavoriteList.Products.Remove(removedFavoriteProduct);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }

        public async Task IncrementAsync(CartProduct cartProduct)
        {
            var countProduct = await Db.Products.CountAsync(product => product.Article.Id == cartProduct.Article.Id && !product.IsSold);
            if (cartProduct.Count == countProduct)
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
