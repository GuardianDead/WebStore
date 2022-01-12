using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;
using WMBlazorSlickCarousel.WMBSC;

namespace WebStore.Pages
{
    public class IndexBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] public AppDbContext Db { get; set; }

        public BlazorSlickCarousel theCarousel1;
        public BlazorSlickCarousel theCarousel2;
        public BlazorSlickCarousel theCarousel3;
        public WMBSCInitialSettings configurations;

        public List<ProductModel> productModelsSection1;
        public List<ProductModel> productModelsSection2;
        public List<ProductModel> productModelsSection3;
        public User currentUser;
        public ClaimsPrincipal currentUserState;

        protected override async Task OnInitializedAsync()
        {
            WMBSCResponsiveSettings breakpoint500Responsive = new WMBSCResponsiveSettings // <= 500px размера экрана
            {
                breakpoint = 500,
                settings = new WMBSCSettings()
                {
                    dotsClass = "carousel__dots",
                    arrows = true,
                    dots = true,
                    waitForAnimate = true,
                    slidesToScroll = 1,
                    touchThreshold = 20,

                }
            };
            WMBSCResponsiveSettings breakpoint825Responsive = new WMBSCResponsiveSettings // <= 825px размера экрана
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
            };
            configurations = new WMBSCInitialSettings
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
                    breakpoint825Responsive,
                    breakpoint500Responsive
                }
            };

            productModelsSection1 = Db.ProductModels.AsNoTracking().Take(7).ToList();
            productModelsSection2 = Db.ProductModels.AsNoTracking().Skip(3).Take(7).ToList();
            productModelsSection3 = Db.ProductModels.AsNoTracking().Take(5).ToList();

            currentUserState = (await AuthenticationStateTask).User;
            if (currentUserState.Identity.IsAuthenticated)
            {
                var userEmailClaim = currentUserState.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                currentUser = Db.Users
                    .Include(user => user.Cart.Products)
                            .Include(user => user.ListFavourites.Products)
                            .SingleOrDefault(user => user.Email == userEmailClaim.Value);
            }
        }

        public void AddProductInUserCart(ProductModel productModel) => currentUser.Cart.Products.Add(new CartProduct(Db.ProductArticles.Single(productArticle => productArticle.Model.Id == productModel.Id), 1));
        public void AddProductInUserFavorites(ProductModel productModel) => currentUser.ListFavourites.Products.Add(new FavoritesListProduct(Db.ProductArticles.Single(productArticle => productArticle.Model.Id == productModel.Id)));
        public void RemoveProductInUserCart(ProductModel productModel) => currentUser.Cart.Products.Remove(currentUser.Cart.Products.Single(cartProduct => cartProduct.ProductArticle.Model.Id == productModel.Id));
        public void RemoveProductInUserFavorites(ProductModel productModel) => currentUser.ListFavourites.Products.Remove(currentUser.ListFavourites.Products.Single(faforiteProduct => faforiteProduct.ProductArticle.Model.Id == productModel.Id));
    }
}
