using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebStore.Pages.Shared.Layout
{
    public class FooterBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public ClaimsPrincipal currentUserState;

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationStateTask).User;
        }
    }
}
