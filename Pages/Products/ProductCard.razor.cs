using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebStore.Pages.Products
{
    public class ProductCardBase : ComponentBase
    {
        [Parameter] public int ProductModelId { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
