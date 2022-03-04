using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages.Account
{
    public class OrdersBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject] public AppDbContext Db { get; set; }

        public List<Order> orders = new List<Order>();
        public List<Product> ordersProducts = new List<Product>();

        private ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationState).User;
            var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = Db.Users
                .AsNoTracking()
                .Include(user => user.OrderHistory.Orders)
                .Single(user => user.Email == userEmail);
            orders = Db.Orders
                .AsNoTracking()
               .Include(order => order.Address)
               .Include(order => order.Delivery)
               .Include(order => order.Products)
               .Where(order => currentUser.OrderHistory.Orders.Contains(order))
               .ToList();
            foreach (var order in orders)
            {
                order.Products = Db.Products
                .AsNoTracking()
                .Include(product => product.Article.Model.Subcategory)
                .Where(product => order.Products.Contains(product))
                .ToList();
            }
        }

        public int GetCountSameProducts(Product product, Order order) => order.Products.Where(orderProduct => orderProduct.Article.Id == product.Article.Id).Count();
        public decimal GetFullPriceForProduct(Product product, Order order) => order.Products.Where(orderProduct => orderProduct.Article.Id == product.Article.Id).Sum(product => product.Article.Model.Price);
        public string GetFullAdress(Address address) => $"{address.Country}, {address.City}, {address.Street}, {address.HouseNumber}, {address.PostalCode}";
    }
}
