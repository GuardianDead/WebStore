using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages
{
    public class IndexBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] public AppDbContext Db { get; set; }

        public WMBSCInitialSettings configurations = new WMBSCInitialSettings()
        {
            dotsClass = "carousel__dots",
            arrows = true,
            dots = true,
            waitForAnimate = true,
            slidesToShow = 3,
            slidesToScroll = 1,
            touchThreshold = 20,
            centerMode = true,
            responsive = new List<WMBSCResponsiveSettings>()
            {
                new WMBSCResponsiveSettings // <= 500px размера экрана
                {
                    breakpoint = 500,
                    settings = new WMBSCSettings
                    {
                        dotsClass = "carousel__dots",
                        arrows = true,
                        dots = true,
                        waitForAnimate = true,
                        slidesToScroll = 1,
                        touchThreshold = 20,
                    }
                },
                new WMBSCResponsiveSettings // <= 825px размера экрана
                {
                    breakpoint = 825,
                    settings = new WMBSCSettings
                    {
                        dotsClass = "carousel__dots",
                        arrows = true,
                        dots = true,
                        waitForAnimate = true,
                        slidesToScroll = 1,
                        touchThreshold = 20,
                        centerMode = true,
                        slidesToShow = 2,
                    }
                }
            }
        };

        public List<ProductModel> productsModelsSection1;
        public List<ProductModel> productsModelsSection2;
        public List<ProductModel> productsModelsSection3;
        public ClaimsPrincipal currentUserState;
        public User currentUser;

        public int CurrentCountProductsOfModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            productsModelsSection1 = await Db.ProductModels
                .AsNoTracking()
                .Where(productModel => Db.ProductArticles
                    .Include(productArticle => productArticle.Model)
                    .Where(productArticle => productArticle.Model.Id == productModel.Id)
                    .Any(productArticle => Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0))
                .Take(9).ToListAsync();
            productsModelsSection2 = productsModelsSection1;
            productsModelsSection3 = productsModelsSection1;
            currentUserState = (await AuthenticationStateTask).User;
            if (currentUserState.Identity.IsAuthenticated)
            {
                var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                currentUser = await Db.Users
                    .Include(user => user.ListFavourites.Products)
                        .ThenInclude(favoritesProducts => favoritesProducts.Article.Model)
                    .Include(user => user.Cart.Products)
                        .ThenInclude(favoritesProducts => favoritesProducts.Article.Model)
                    .SingleOrDefaultAsync(user => user.Email == userEmail);
            }
        }

        public async Task AddProductInCartAsync(ProductModel productModel)
        {
            if (currentUser.Cart.Products.Any(product => product.Article.Model.Id == productModel.Id))
                return;
            var addedFirstProductArticleOfModel = await Db.ProductArticles
                .Include(productArticle => productArticle.Model)
                .FirstAsync(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0);
            currentUser.Cart.Products.Add(new CartProduct(addedFirstProductArticleOfModel, 1));
            await Db.SaveChangesAsync();
        }
        public async Task AddProductInFavoritesAsync(ProductModel productModel)
        {
            if (currentUser.ListFavourites.Products.Any(product => product.Article.Model.Id == productModel.Id))
                return;
            var addedFirstProductArticleOfModel = await Db.ProductArticles
                .Include(productArticle => productArticle.Model)
                .FirstAsync(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0);
            currentUser.ListFavourites.Products.Add(new FavoriteProduct(addedFirstProductArticleOfModel));
            await Db.SaveChangesAsync();
        }
        public async Task RemoveProductFromCartAsync(ProductModel productModel)
        {
            if (!currentUser.Cart.Products.Any(product => product.Article.Model.Id == productModel.Id))
                return;
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Model.Id == productModel.Id);
            await Db.SaveChangesAsync();
        }
        public async Task RemoveProductFromFavoritesAsync(ProductModel productModel)
        {
            if (!currentUser.ListFavourites.Products.Any(product => product.Article.Model.Id == productModel.Id))
                return;
            currentUser.ListFavourites.Products.RemoveAll(cartProduct => cartProduct.Article.Model.Id == productModel.Id);
            await Db.SaveChangesAsync();
        }
    }
}
