using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;

namespace WebStore.Pages.Shared.Layout
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }

        public bool ScrollToUpIsActive { get; set; }
        public bool DarkOverlayIsShow { get; set; }
        public string ClassWightCategorySideNavigation { get; set; }
        public string ClassWightSubcategorySideNavigation { get; set; }

        public static List<Subcategory> subcategories;
        public static List<Category> categories = new List<Category>();
        public static Category selectedCategory;
        public static List<Subcategory> selectedSubcaregories;

        protected override Task OnInitializedAsync()
        {
            subcategories = Db.Subcategories.Include(x => x.Category).AsNoTracking().ToList();
            foreach (var subcategory in subcategories)
                if (!categories.Any(category => category.Name == subcategory.Category.Name))
                    categories.Add(subcategory.Category);

            return JSRuntime.InvokeVoidAsync("SetMainLayouteDotnetReference", DotNetObjectReference.Create(this)).AsTask();
        }

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
            selectedSubcaregories = subcategories.Where(subcategory => subcategory.Category.Id == category.Id).ToList();
            StateHasChanged();
        }
        [JSInvokable]
        public void CloseSubcategorySideNavigation()
        {
            selectedCategory = null;
            selectedSubcaregories = null;
            ClassWightSubcategorySideNavigation = "close";
            StateHasChanged();
        }

    }
}
