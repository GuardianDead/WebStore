using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebStore.Pages.Shared.Layout
{
    public class FooterBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        public ElementReference? selectedSpoilerContent;
        public ElementReference firstSpoilerContent;
        public ElementReference secondSpoilerContent;
        public ElementReference thirdSpoilerContent;
        public ElementReference fourthSpoilerContent;
        public ClaimsPrincipal currentUserState;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetFooterDotnetReference", DotNetObjectReference.Create(this));
            currentUserState = (await AuthenticationStateTask).User;
        }

        public async Task ToogleSpoilerAsync(ElementReference spoilerContent)
        {
            if (selectedSpoilerContent?.Id == spoilerContent.Id)
            {
                await JSRuntime.InvokeVoidAsync("hideSpoilerContent", spoilerContent);
                selectedSpoilerContent = null;
                StateHasChanged();
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("showSpoilerContent", spoilerContent);
                selectedSpoilerContent = spoilerContent;
                StateHasChanged();
            }
        }
    }
}
