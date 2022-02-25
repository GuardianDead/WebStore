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
    public class CartBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }

        public bool IsSelectedAll { get; set; } = true;

        public decimal totalCost;

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
            cartProductsList.ForEach(cartProduct => cartProduct.IsSelected = true);
            UpdateTotalCurrentCostCart();
        }

        public void UpdateTotalCurrentCostCart() => totalCost = cartProductsList.Where(cartProduct => cartProduct.IsSelected)
            .Sum(cartProduct => cartProduct.ProductArticle.Model.Price * cartProduct.Count);

        public void AddProductInUserCart(CartProduct cartProduct)
        {
            cartProductsList.Add(new CartProduct(Db.ProductArticles.Include(productArticle => productArticle.Model).First(productArticle => productArticle.Id == cartProduct.ProductArticle.Id), 1));
            currentUser.Cart.Products = cartProductsList;
            UpdateTotalCurrentCostCart();
            Db.SaveChanges();

        }
        public void AddProductInUserFavorites(CartProduct cartProduct)
        {
            favoritesProductsList.Add(new FavoritesListProduct(Db.ProductArticles.Include(productArticle => productArticle.Model).First(productArticle => productArticle.Id == cartProduct.ProductArticle.Id)));
            currentUser.ListFavourites.Products = favoritesProductsList;
            Db.SaveChanges();
        }
        public void RemoveProductInUserCart(CartProduct cartProduct)
        {
            cartProductsList.Remove(cartProductsList.First(cartProductList => cartProductList.ProductArticle.Id == cartProduct.ProductArticle.Id));
            currentUser.Cart.Products = cartProductsList;
            UpdateTotalCurrentCostCart();
            Db.SaveChanges();
        }
        public void RemoveProductInUserFavorites(CartProduct cartProduct)
        {
            favoritesProductsList.Remove(favoritesProductsList.First(favoriteProduct => favoriteProduct.ProductArticle.Id == cartProduct.ProductArticle.Id));
            currentUser.ListFavourites.Products = favoritesProductsList;
            Db.SaveChanges();
        }

        public void Increment(CartProduct cartProduct)
        {
            if (cartProduct.Count == 999)
                return;
            ++cartProduct.Count;
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
            if (IsSelectedAll && cartProductsList.All(cartProduct => cartProduct.IsSelected))
            {
                IsSelectedAll = false;
                cartProductsList.ForEach(cartProduct => cartProduct.IsSelected = false);
            }
            else if (!IsSelectedAll && cartProductsList.Any(cartProduct => !cartProduct.IsSelected))
            {
                IsSelectedAll = true;
                cartProductsList.ForEach(cartProduct => cartProduct.IsSelected = true);
            }
            UpdateTotalCurrentCostCart();
        }
        public void ProductSelecterToggle(CartProduct cartProduct)
        {
            cartProduct.IsSelected = !cartProduct.IsSelected;
            if (!IsSelectedAll && cartProductsList.All(cartProduct => cartProduct.IsSelected))
                IsSelectedAll = true;
            else if (IsSelectedAll && cartProductsList.Any(cartProduct => !cartProduct.IsSelected))
                IsSelectedAll = false;
            UpdateTotalCurrentCostCart();
        }
    }
}
