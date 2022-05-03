using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages.Shared.Layout
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }

        public bool ScrollToUpIsActive { get; set; }
        public bool DarkOverlayIsShow { get; set; }
        public string ClassWightCategorySideNavigation { get; set; }
        public string ClassWightSubcategorySideNavigation { get; set; }
        public bool ReturnButtonIsScroling { get; set; }
        public bool CategorySideNavigationIsScroling { get; set; }

        public List<Category> categories;
        public Category selectedCategory;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetMainLayouteDotnetReference", DotNetObjectReference.Create(this)).AsTask();

            categories = await Db.Categories
                .AsNoTracking()
                .Include(category => category.Subcategories)
                .ToListAsync();
        }

        public void NavigateTo(string path) => NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}{path}", true);
        public void GoToProductCatalog(string categoryName)
        {
            NavigateTo(@$"/products/catalog/{categoryName}".Replace(" ", "-").ToLower());
            CloseCategorySideNavigation();
        }
        public void GoToProductCatalog(string categoryName, string SubcategoryName)
        {
            NavigateTo(@$"/products/catalog/{categoryName}/{SubcategoryName}".Replace(" ", "-").ToLower());
            CloseCategorySideNavigation();
            CloseSubcategorySideNavigation();
        }

        [JSInvokable("СancelCategorySideNavigationIsScroling")]
        public void СancelCategorySideNavigationIsScroling()
        {
            CategorySideNavigationIsScroling = false;
            StateHasChanged();
        }
        [JSInvokable]
        public void EnableCategorySideNavigationIsScroling()
        {
            CategorySideNavigationIsScroling = true;
            StateHasChanged();
        }
        [JSInvokable("СancelReturnButtonScroling")]
        public void СancelReturnButtonScroling()
        {
            ReturnButtonIsScroling = false;
            StateHasChanged();
        }
        [JSInvokable]
        public void EnableReturnButtonScroling()
        {
            ReturnButtonIsScroling = true;
            StateHasChanged();
        }
        [JSInvokable]
        public Task ScrollToTopAsync() => JSRuntime.InvokeVoidAsync("scrollToTop").AsTask();
        [JSInvokable]
        public void ShowScrollToUp()
        {
            ScrollToUpIsActive = true;
            StateHasChanged();
        }
        [JSInvokable]
        public void HideScrollToUp()
        {
            ScrollToUpIsActive = false;
            StateHasChanged();
        }
        [JSInvokable]
        public void OpenCategorySideNavigation(string classWightCategorySideNavigation)
        {
            ClassWightCategorySideNavigation = classWightCategorySideNavigation;
            DarkOverlayIsShow = true;
            StateHasChanged();
        }
        [JSInvokable]
        public void CloseCategorySideNavigation()
        {
            ClassWightCategorySideNavigation = "close";
            DarkOverlayIsShow = false;
            StateHasChanged();
        }
        [JSInvokable]
        public async Task OpenSubcategorySideNavigationAsync(Category category)
        {
            var currentWindowInnerWidth = await JSRuntime.InvokeAsync<int>("getCurrentWindowInnerWidth");
            ClassWightSubcategorySideNavigation = currentWindowInnerWidth <= 420 ? "expand100" : "expand75";
            selectedCategory = category;
            StateHasChanged();
        }
        [JSInvokable]
        public void CloseSubcategorySideNavigation()
        {
            selectedCategory = null;
            ClassWightSubcategorySideNavigation = "close";
            StateHasChanged();
        }
    }
}
