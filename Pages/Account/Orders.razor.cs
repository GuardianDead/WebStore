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

        public List<ProductModel> productModels;
        private ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationState).User;
            var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = await Db.Users
                .AsNoTracking()
                .Include(user => user.OrderHistory.Orders)
                    .ThenInclude(orders => orders.Delivery)
                .Include(user => user.OrderHistory.Orders)
                    .ThenInclude(orders => orders.Address)
                .Include(user => user.OrderHistory.Orders)
                    .ThenInclude(orders => orders.Products)
                .SingleAsync(user => user.Email == userEmail);
        }

        public IEnumerable<IGrouping<string, ProductModel>> DisctictProductsByProductModel(List<OrderProduct> orderProducts) =>
            orderProducts.GroupBy(orderProduct => orderProduct.Product.Article.Model.Id, orderProduct => orderProduct.Product.Article.Model);
    }
}
