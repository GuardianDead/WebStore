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

namespace WebStore.Pages.Products
{
    public class ProductCardBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Parameter] public string ProductModelId { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public byte[] SelectedImage { get; set; }
        public string SelectedColor { get; set; }
        public int SelectedSize { get; set; }
        public ProductArticle SelectedProductArticle { get; set; }

        public List<string> allCollors;
        public List<int> allSizes;
        public ProductModel productModel;
        public List<ProductArticle> allProductArticles;
        public ClaimsPrincipal userState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetPageDotnetReference", DotNetObjectReference.Create(this));
            productModel = await Db.ProductModels
                .Include(productModel => productModel.Photos)
                .Include(productModel => productModel.Materials)
                .Include(productModel => productModel.Features)
                .Include(productModel => productModel.Subcategory.Category)
                .SingleOrDefaultAsync(productModel => productModel.Id == ProductModelId);
            if (productModel is null)
                NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}not-found", true);

            allProductArticles = await Db.ProductArticles
                .Include(productArticle => productArticle.Model)
                .Where(productArticle => productArticle.Model.Id == productModel.Id)
                .ToListAsync();
            if (allProductArticles.Count > 0)
            {
                allCollors = allProductArticles
                    .GroupBy(productArticle => productArticle.Color)
                    .Select(group => group.Key)
                    .ToList();
                allSizes = allProductArticles
                    .GroupBy(productArticle => productArticle.Size)
                    .Select(group => group.Key)
                    .ToList();
                SelectedImage = productModel.Photos.First().Value;
                SelectedColor = allCollors.First();
                SelectedSize = allSizes.First();
                SelectedProductArticle = allProductArticles.SingleOrDefault(productArticle => productArticle.Color == SelectedColor && productArticle.Size == SelectedSize);
            }

            userState = (await AuthenticationStateTask).User;
            if (userState.Identity.IsAuthenticated)
            {
                var userEmail = userState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                currentUser = await Db.Users
                    .Include(user => user.FavoriteList.Products)
                        .ThenInclude(favoritesProducts => favoritesProducts.Article.Model)
                    .Include(user => user.Cart.Products)
                        .ThenInclude(cartProduct => cartProduct.Article.Model)
                    .SingleOrDefaultAsync(user => user.Email == userEmail);
            }
        }

        public void ClickOnSize(int size)
        {
            SelectedSize = size;
            ChangeSelectedProductArticle();
        }
        public void ClickOnColor(string color)
        {
            SelectedColor = color;
            ChangeSelectedProductArticle();
        }
        public async Task MouseEnterMainImageAsync()
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            if (currentWindowInnerWidth > 1090)
                await JSRuntime.InvokeVoidAsync("openZoomedMainImage");
        }
        public async Task MouseLeaveMainImageAsync()
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            if (currentWindowInnerWidth > 1090)
                await JSRuntime.InvokeVoidAsync("closeZoomedMainImage");
        }

        public void ChangeSelectedProductArticle() => SelectedProductArticle = allProductArticles
            .Single(productArticle => productArticle.Color == SelectedColor && productArticle.Size == SelectedSize);

        public async Task AddProductInCartAsync()
        {
            if (SelectedProductArticle is null ||
                currentUser.Cart.Products.Any(product => product.Article.Id == SelectedProductArticle.Id) ||
                !Db.Products.Any(product => product.Article.Id == SelectedProductArticle.Id && !product.IsSold))
                return;
            currentUser.Cart.Products.Add(new CartProduct(SelectedProductArticle, 1));
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task AddProductInFavoritesAsync()
        {
            if (SelectedProductArticle is null ||
                currentUser.FavoriteList.Products.Any(product => product.Article.Id == SelectedProductArticle.Id) ||
                !Db.Products.Any(product => product.Article.Id == SelectedProductArticle.Id && !product.IsSold))
                return;
            currentUser.FavoriteList.Products.Add(new FavoriteProduct(SelectedProductArticle));
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task RemoveProductFromCartAsync()
        {
            if (SelectedProductArticle is null ||
                !currentUser.Cart.Products.Any(product => product.Article.Id == SelectedProductArticle.Id))
                return;
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Id == SelectedProductArticle.Id);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
        public async Task RemoveProductFromFavoritesAsync()
        {
            if (SelectedProductArticle is null ||
                !currentUser.FavoriteList.Products.Any(product => product.Article.Model.Id == SelectedProductArticle.Model.Id))
                return;
            currentUser.FavoriteList.Products.RemoveAll(favoriteProduct => favoriteProduct.Article.Model.Id == SelectedProductArticle.Model.Id);
            Db.SaveChanges();
            await JSRuntime.InvokeVoidAsync("updateCounterStates", currentUser.Cart.Products.Count, currentUser.FavoriteList.Products.GroupBy(product => product.Article.Model.Id, product => product.Article.Model).Count());
        }
    }
}
