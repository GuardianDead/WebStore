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
                .Include(user => user.ListFavourites.Products)
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
                var cartProductCount = await Db.Products.CountAsync(product => product.Article.Id == cartProduct.Article.Id);
                if (cartProductCount == 0)
                    currentUser.Cart.Products.Remove(cartProduct);
            }
        }
        public void UpdateTotalCurrentCostCart() => totalCost = currentUser.Cart.Products.Where(cartProduct => cartProduct.IsSelected)
            .Sum(cartProduct => cartProduct.Article.Model.Price * cartProduct.Count);

        public void AddProductInFavorites(CartProduct cartProduct)
        {
            var addedFavoriteProduct = currentUser.ListFavourites.Products
                .SingleOrDefault(favoriteProduct => favoriteProduct.Article.Id == cartProduct.Article.Id);
            if (currentUser.ListFavourites.Products.Contains(addedFavoriteProduct))
                return;
            currentUser.ListFavourites.Products.Add(new FavoriteProduct(cartProduct.Article));
            Db.SaveChanges();
        }
        public void RemoveProductFromCart(CartProduct cartProduct)
        {
            if (!currentUser.Cart.Products.Contains(cartProduct))
                return;
            currentUser.Cart.Products.Remove(cartProduct);
            Db.SaveChanges();
        }
        public void RemoveProductFromFavorites(CartProduct cartProduct)
        {
            var removedFavoriteProduct = currentUser.ListFavourites.Products
                    .SingleOrDefault(favoriteProduct => favoriteProduct.Article.Id == cartProduct.Article.Id);
            if (!currentUser.ListFavourites.Products.Contains(removedFavoriteProduct))
                return;
            currentUser.ListFavourites.Products.Remove(removedFavoriteProduct);
            Db.SaveChanges();
        }

        public async Task IncrementAsync(CartProduct cartProduct)
        {
            var countProduct = await Db.Products.CountAsync(product => product.Article.Id == cartProduct.Article.Id);
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
