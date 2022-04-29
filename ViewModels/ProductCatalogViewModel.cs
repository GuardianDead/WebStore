using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebStore.Domain.Types;

namespace WebStore.ViewModels
{
    public class ProductCatalogViewModel
    {
        [Required]
        [DisplayName("Минимальная цена")]
        public decimal MinPrice { get; set; }
        [Required]
        [DisplayName("Максимальная цена")]
        public decimal MaxPrice { get; set; }
        [Required]
        [DisplayName("Выбранные цвета")]
        public IEnumerable<string> SelectedColors { get; set; } = Enumerable.Empty<string>();
        [Required]
        [DisplayName("Выбранные размеры")]
        public IEnumerable<int> SelectedSizes { get; set; } = Enumerable.Empty<int>();
        [Required]
        [DisplayName("Выбранные бренды")]
        public IEnumerable<string> SelectedBrands { get; set; } = Enumerable.Empty<string>();
        [Required]
        [DisplayName("Отсортированно по")]
        [EnumDataType(typeof(SortProductsType))]
        public SortProductsType SortProductsType { get; set; }

        public ProductCatalogViewModel()
        {
        }
        public ProductCatalogViewModel(decimal minPrice, decimal maxPrice, IEnumerable<string> selectedCollors, IEnumerable<int> selectedSizes, IEnumerable<string> selectedBrands, SortProductsType sortProductsType)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            SelectedColors = selectedCollors;
            SelectedSizes = selectedSizes;
            SelectedBrands = selectedBrands;
            SortProductsType = sortProductsType;
        }
    }
}
