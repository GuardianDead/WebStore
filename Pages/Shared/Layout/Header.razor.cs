using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
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

        [Inject] public AppDbContext Db { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public List<Subcategory> subcategories;
        public List<Category> categories = new List<Category>();
        public Category selectedCategory;
        public List<Subcategory> selectedSubcaregories;

        public string returnUrl;
        public ClaimsPrincipal currentUserState;

        protected override async Task OnInitializedAsync()
        {
            returnUrl = NavigationManager.Uri.Replace('/', '$');
            currentUserState = (await AuthenticationStateTask).User;
            subcategories = Db.Subcategories.Include(x => x.Category).AsNoTracking().ToList();
            foreach (var subcategory in subcategories)
                if (!categories.Any(category => category.Name == subcategory.Category.Name))
                    categories.Add(subcategory.Category);
        }

        public void NavigateToIndex() => NavigationManager.NavigateTo(NavigationManager.BaseUri, true);
        public void NavigateToLogin() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/login/{returnUrl}", true);
        public void NavigateToLogout() => NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/authorization/logout/{returnUrl}", true);
    }
}
