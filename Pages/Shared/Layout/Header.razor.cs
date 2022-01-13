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

        public static bool IsSubcategoriesShow { get; set; } = false;
        public bool IsUserPanelShow { get; set; } = false;
        public bool IsSearchPanelActive { get; set; } = false;

        public static List<Subcategory> subcategories;
        public static List<Category> categories = new List<Category>();
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
                    .Include(user => user.Cart.Products)
                    .Include(user => user.ListFavourites.Products)
                    .SingleAsync(user => user.Email == userEmail);
            }

            subcategories = Db.Subcategories.Include(x => x.Category).AsNoTracking().ToList();
            foreach (var subcategory in subcategories)
                if (!categories.Any(category => category.Name == subcategory.Category.Name))
                    categories.Add(subcategory.Category);
        }

        public void NavigateToIndex() => NavigationManager.NavigateTo(NavigationManager.BaseUri, true);
        public void NavigateToLogin() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/login/{returnUrl}", true);
        public void NavigateToLogout() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/logout/{returnUrl}", true);

        public int CountFavoriteProducts() => currentUser.ListFavourites.Products.Count;
        public int CountCartProducts() => currentUser.Cart.Products.Count;

        [JSInvokable]
        public ValueTask HideSubcategoriesAsync()
        {
            IsSubcategoriesShow = false;
            selectedCategory = null;
            selectedSubcaregories = null;
            StateHasChanged();
            return JSRuntime.InvokeVoidAsync("hideSubcategories");
        }
        [JSInvokable]
        public ValueTask ShowSubcategoriesAsync(Category category)
        {
            IsSubcategoriesShow = true;
            selectedCategory = category;
            selectedSubcaregories = subcategories.Where(subcategory => subcategory.Category.Id == category.Id).ToList();
            StateHasChanged();
            return JSRuntime.InvokeVoidAsync("showSubcategories");
        }
        [JSInvokable]
        public async ValueTask ToggleUserPanelAsync()
        {
            if (IsUserPanelShow)
            {
                await JSRuntime.InvokeVoidAsync("hideUserPanel");
                IsUserPanelShow = !IsUserPanelShow;
                StateHasChanged();
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("showUserPanel");
                IsUserPanelShow = !IsUserPanelShow;
                StateHasChanged();
            }
        }
        [JSInvokable]
        public async ValueTask ToggleSearchPanelAsync()
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            if (currentWindowInnerWidth <= 835)
            {
                if (IsSearchPanelActive)
                {
                    //TODO : Ищем имена тех товаров который ввел пользователь
                }
                else
                {
                    IsSearchPanelActive = true;
                    StateHasChanged();
                }
            }

            //TODO : Ищем имена тех товаров который ввел пользователь
        }
        [JSInvokable]
        public void HideSearchPanel()
        {
            IsSearchPanelActive = false;
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
