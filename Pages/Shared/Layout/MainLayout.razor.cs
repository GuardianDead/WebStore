using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebStore.Pages.Shared.Layout
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }

        public bool ScrollToUpIsActive { get; set; }

        protected override Task OnInitializedAsync()
        {
            return JSRuntime.InvokeVoidAsync("SetMainLayouteDotnetReference", DotNetObjectReference.Create(this)).AsTask();
        }

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
        public Task ScrollToTopAsync() => JSRuntime.InvokeVoidAsync("scrollToTop").AsTask();
    }
}
