using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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

        public string SearchProductModelsName { get; set; }

        public string returnUrl;
        public List<Category> categories;
        public Category selectedCategory;
        public ClaimsPrincipal currentUserState;
        public User currentUser;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetHeaderDotnetReference", DotNetObjectReference.Create(this));
            returnUrl = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");
            currentUserState = (await AuthenticationStateTask).User;
            if (currentUserState.Identity.IsAuthenticated)
            {
                var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                currentUser = await Db.Users
                    .Include(user => user.Cart.Products)
                    .Include(user => user.FavoriteList.Products)
                    .SingleAsync(user => user.Email == userEmail);
            }
            categories = await Db.Categories
                .AsNoTracking()
                .Include(category => category.Subcategories)
                .ToListAsync();
        }

        public void NavigateTo(string path) => NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}{path}", true);
        public int CountFavoriteProducts() => currentUser.FavoriteList.Products.Count;
        public int CountCartProducts() => currentUser.Cart.Products.Count;

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
            await JSRuntime.InvokeVoidAsync("hideSubcategories");
            StateHasChanged();
        }
        [JSInvokable]
        public async ValueTask ShowSubcategoriesAsync(Category category)
        {
            SubcategoriesIsShow = true;
            selectedCategory = category;
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
                    NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}products/catalog/search/{SearchProductModelsName}", true);
                }
                else
                {
                    SearchPanelIsActive = true;
                    StateHasChanged();
                }
            }
            NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}products/catalog/search/{SearchProductModelsName}", true);
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

        public void SearchProductModelsByName(KeyboardEventArgs keyboardEventArgs)
        {
            if (keyboardEventArgs.Key == "Enter" && !string.IsNullOrWhiteSpace(SearchProductModelsName))
            {
                var resultProductModelName = new Regex(@"\s\s+").Replace("     we   fwef   qwef       ", " ").Replace(" ", "-").ToLower();
                NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}products/catalog/search/{resultProductModelName}", true);
            }
        }
    }
}
