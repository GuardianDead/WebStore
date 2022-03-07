using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages.Shared.Layout
{
    public class HeaderBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public UserManager<User> UserManager { get; set; }

        public static bool SubcategoriesIsShow { get; set; }
        public bool UserPanelIsShow { get; set; }
        public bool SearchPanelIsActive { get; set; }
        public bool HeaderIsScroling { get; set; }

        public static List<Subcategory> subcategories;
        public static List<Category> categories;
        public static Category selectedCategory;
        public static List<Subcategory> selectedSubcaregories;
        public string searchContent;

        public string returnUrl;
        public ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetHeaderDotnetReference", DotNetObjectReference.Create(this));
            returnUrl = NavigationManager.Uri.Replace('/', '$');
            currentUserState = (await AuthenticationStateTask).User;
            if (currentUserState.Identity.IsAuthenticated)
            {
                var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                currentUser = await Db.Users
                    .AsNoTracking()
                    .Include(user => user.Cart.Products)
                    .Include(user => user.ListFavourites.Products)
                    .SingleAsync(user => user.Email == userEmail);
            }
            categories = await Db.Categories
                .AsNoTracking()
                .ToListAsync();
            subcategories = await Db.Subcategories
                .AsNoTracking()
                .Include(subcategory => subcategory.Category)
                .ToListAsync();
        }

        public void NavigateToIndex() => NavigationManager.NavigateTo(NavigationManager.BaseUri, true);
        public void NavigateToOrderHistory() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/orders", true);
        public void NavigateToCart() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/cart", true);
        public void NavigateToFavoritesList() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}/account/favorites", true);
        public void NavigateToLogin() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/login/{returnUrl}", true);
        public void NavigateToLogout() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/logout/{returnUrl}", true);
        public int CountFavoriteProducts() => currentUser.ListFavourites.Products.Count();
        public int CountCartProducts() => currentUser.Cart.Products.Count();

        [JSInvokable]
        public void СancelHeaderScrolling()
        {
            HeaderIsScroling = false;
            StateHasChanged();
        }
        [JSInvokable]
        public void EnableHeaderScrolling()
        {
            HeaderIsScroling = true;
            StateHasChanged();
        }
        [JSInvokable]
        public async ValueTask HideSubcategoriesAsync()
        {
            SubcategoriesIsShow = false;
            selectedCategory = null;
            selectedSubcaregories = null;
            await JSRuntime.InvokeVoidAsync("hideSubcategories");
            StateHasChanged();
        }
        [JSInvokable]
        public async ValueTask ShowSubcategoriesAsync(Category category)
        {
            SubcategoriesIsShow = true;
            selectedCategory = category;
            selectedSubcaregories = subcategories
                .Where(subcategory => subcategory.Category.Id == category.Id)
                .ToList();
            await JSRuntime.InvokeVoidAsync("showSubcategories");
            StateHasChanged();
        }
        [JSInvokable]
        public ValueTask ToggleUserPanelAsync() => UserPanelIsShow ? HideUserPanelAsync() : ShowUserPanelAsync();
        [JSInvokable]
        public async ValueTask ShowUserPanelAsync()
        {
            UserPanelIsShow = true;
            await JSRuntime.InvokeVoidAsync("showUserPanel");
            StateHasChanged();
        }
        [JSInvokable]
        public async ValueTask HideUserPanelAsync()
        {
            UserPanelIsShow = false;
            await JSRuntime.InvokeVoidAsync("hideUserPanel");
            StateHasChanged();
        }
        [JSInvokable]
        public async ValueTask ToggleSearchPanelAsync()
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            if (currentWindowInnerWidth <= 835)
            {
                if (SearchPanelIsActive)
                {
                    //TODO : Ищем имена тех товаров который ввел пользователь
                }
                else
                {
                    SearchPanelIsActive = true;
                    StateHasChanged();
                }
            }

            //TODO : Ищем имена тех товаров который ввел пользователь
        }
        [JSInvokable]
        public void HideSearchPanel()
        {
            SearchPanelIsActive = false;
            StateHasChanged();
        }
        [JSInvokable]
        public async Task OpenCategorySideNavigationAsync()
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            await JSRuntime.InvokeVoidAsync("openCategorySideNavigation", currentWindowInnerWidth <= 420 ? "expand100" : "expand75");
        }
    }
}
