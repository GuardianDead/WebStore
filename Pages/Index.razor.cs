using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Data.Identity;
using WebStore.Data.Repositories.AppIdentityUserRepository;
using WebStore.Data.Repositories.ProductRepository;

namespace WebStore.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] public UserManager<User> UserManager { get; set; }
        [Inject] public IUserRepository UserRepository { get; set; }
        //[Inject] public AuthenticationState AuthenticationState { get; set; }
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public AppDbContext db { get; set; }

        public List<Product> firstProducts;
        public User currentUser;

        protected override Task OnInitializedAsync()
        {
            firstProducts = db.Products.Include(p => p.Article.Model).AsNoTracking().Take(10).ToList();
            return base.OnInitializedAsync();
        }

        public void AddProductInUserCart(ProductArticle productArticle) => currentUser.Cart.Products.ToList().Add(new CartProduct(productArticle, 1));
        public void AddProductInUserFavorites(ProductArticle productArticle) => currentUser.ListFavourites.Products.ToList().Add(new FavoritesListProduct(productArticle));
    }
}
