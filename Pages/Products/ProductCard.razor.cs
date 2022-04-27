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
        public User user;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetPageDotnetReference", DotNetObjectReference.Create(this));
            productModel = await Db.ProductModels
                .Include(productModel => productModel.Subcategory.Category)
                .SingleOrDefaultAsync(productModel => productModel.Id == ProductModelId);
            if (productModel is null)
                NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}not-found");

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
                SelectedImage = productModel.Photos.First();
                SelectedColor = allCollors.First();
                SelectedSize = allSizes.First();
                SelectedProductArticle = allProductArticles.SingleOrDefault(productArticle => productArticle.Color == SelectedColor && productArticle.Size == SelectedSize);
            }

            userState = (await AuthenticationStateTask).User;
            if (userState.Identity.IsAuthenticated)
            {
                var userEmail = userState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                user = await Db.Users
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
            ChangeSelectedProductArtucle();
        }
        public void ClickOnColor(string color)
        {
            SelectedColor = color;
            ChangeSelectedProductArtucle();
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

        public void ChangeSelectedProductArtucle() => SelectedProductArticle = allProductArticles.Single(productArticle => productArticle.Color == SelectedColor && productArticle.Size == SelectedSize);

        public void AddProductInCart()
        {
            if (SelectedProductArticle is null ||
                user.Cart.Products.Any(product => product.Article.Id == SelectedProductArticle.Id) ||
                Db.Products.Count(product => product.Article.Id == SelectedProductArticle.Id) < 1)
                return;
            user.Cart.Products.Add(new CartProduct(SelectedProductArticle, 1));
            Db.SaveChanges();
        }
        public void AddProductInFavorites()
        {
            if (SelectedProductArticle is null ||
                user.FavoriteList.Products.Any(product => product.Article.Id == SelectedProductArticle.Id) ||
                Db.Products.Count(product => product.Article.Id == SelectedProductArticle.Id) < 1)
                return;
            user.FavoriteList.Products.Add(new FavoriteProduct(SelectedProductArticle));
            Db.SaveChanges();
        }
        public void RemoveProductFromCart()
        {
            if (SelectedProductArticle is null ||
                !user.Cart.Products.Any(product => product.Article.Id == SelectedProductArticle.Id))
                return;
            user.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Id == SelectedProductArticle.Id);
            Db.SaveChanges();
        }
        public void RemoveProductFromFavorites()
        {
            if (SelectedProductArticle is null ||
                !user.FavoriteList.Products.Any(product => product.Article.Id == SelectedProductArticle.Id))
                return;
            user.FavoriteList.Products.RemoveAll(favoriteProduct => favoriteProduct.Article.Id == SelectedProductArticle.Id);
            Db.SaveChanges();
        }
    }
}
