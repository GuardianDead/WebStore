using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Domain.Types;
using WebStore.ViewModels;

namespace WebStore.Pages.Products
{
    public class ProductCatalogBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Parameter] public string CategoryName { get; set; }
        [Parameter] public string SubcategoryName { get; set; }

        [Inject] public IValidator<ProductCatalogViewModel> ProductCatalogViewModelValidator { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppDbContext Db { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public ProductCatalogViewModel ProductCatalogViewModel { get; set; } = new ProductCatalogViewModel();

        public bool FilterIsShow { get; set; }
        public bool BottomFilterIsShow { get; set; }

        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public bool ProductModelsIsLoading { get; set; }

        public ClaimsPrincipal currentUserState;
        public User currentUser;
        public Category currentCategory;
        public Subcategory currentSubcategory;
        private Expression<Func<ProductArticle, bool>> сategoryOrSubcategoryPredicateForArticle;
        private Expression<Func<ProductModel, bool>> сategoryOrSubcategoryPredicateForModel;
        public SortProductsType[] allSortProductsType = Enum.GetValues<SortProductsType>();
        public Dictionary<string, bool> allColorsFilterDictionary = new Dictionary<string, bool>();
        public Dictionary<string, bool> allBrandsFilterDictionary = new Dictionary<string, bool>();
        public Dictionary<int, bool> allSizesFilterDictionary = new Dictionary<int, bool>();
        public IEnumerable<ProductModel> selectedProductsModels;
        private IQueryable<ProductModel> currentProductModelsQuery;
        public decimal minPrice;
        public decimal maxPrice;
        private const int chunkProductModels = 27;

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("SetPageDotnetReference", DotNetObjectReference.Create(this));

            сategoryOrSubcategoryPredicateForModel = productModel => true;
            сategoryOrSubcategoryPredicateForArticle = productArticle => true;

            if (!string.IsNullOrEmpty(SubcategoryName))
            {
                currentSubcategory = Db.Subcategories
                    .AsNoTracking()
                    .Include(subcategory => subcategory.Category)
                    .SingleOrDefault(subcategory => subcategory.Name == SubcategoryName);
                currentCategory = Db.Categories
                    .AsNoTracking()
                    .SingleOrDefault(category => category.Name == CategoryName);
                if (currentSubcategory is null || currentCategory is null || currentSubcategory.Category.Id != currentCategory.Id)
                {
                    NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}not-found/");
                    return;
                }
                сategoryOrSubcategoryPredicateForModel = productModel => productModel.Subcategory.Id == currentSubcategory.Id;
                сategoryOrSubcategoryPredicateForArticle = productArticle => productArticle.Model.Subcategory.Id == currentSubcategory.Id;
            }
            else if (!string.IsNullOrEmpty(CategoryName))
            {
                currentCategory = Db.Categories
                    .AsNoTracking()
                    .SingleOrDefault(category => category.Name == CategoryName);
                if (currentCategory is null)
                {
                    NavigationManager.NavigateTo($@"{NavigationManager.BaseUri}not-found/");
                    return;
                }
                сategoryOrSubcategoryPredicateForModel = productModel => productModel.Subcategory.Category.Id == currentCategory.Id;
                сategoryOrSubcategoryPredicateForArticle = productArticle => productArticle.Model.Subcategory.Category.Id == currentCategory.Id;
            }

            FillFilters();
            UpdateFilters();
            currentProductModelsQuery = BuildProductModelsQueryByCurrentFilters();

            ProductModelsIsLoading = true;
            selectedProductsModels = currentProductModelsQuery.AsNoTracking().Take(chunkProductModels).ToList();
            ProductModelsIsLoading = false;

            minPrice = Math.Round(currentProductModelsQuery.AsNoTracking().Min(productModel => productModel.Price));
            maxPrice = Math.Round(currentProductModelsQuery.AsNoTracking().Max(productModel => productModel.Price));

            CurrentPage = 1;
            MaxPage = selectedProductsModels.Count() / chunkProductModels;
            if (MaxPage == 0)
                MaxPage = 1;
            ProductCatalogViewModel.MinPrice = minPrice;
            ProductCatalogViewModel.MaxPrice = maxPrice;

            currentUserState = (await AuthenticationStateTask).User;
            if (currentUserState.Identity.IsAuthenticated)
            {
                var userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
                currentUser = Db.Users
                    .Include(user => user.FavoriteList.Products)
                        .ThenInclude(favoritesProducts => favoritesProducts.Article.Model)
                    .Include(user => user.Cart.Products)
                        .ThenInclude(cartProduct => cartProduct.Article.Model)
                    .SingleOrDefault(user => user.Email == userEmail);
            }
        }

        [JSInvokable]
        public async Task ToogleFilterAsync()
        {
            if (FilterIsShow)
                await CloseFilterAsync();
            else
                await OpenFilterAsync();
            StateHasChanged();
        }
        [JSInvokable]
        public ValueTask CloseFilterAsync()
        {
            FilterIsShow = false;
            return JSRuntime.InvokeVoidAsync("closeFilters");
        }
        [JSInvokable]
        public ValueTask OpenFilterAsync()
        {
            FilterIsShow = true;
            return JSRuntime.InvokeVoidAsync("openFilters");
        }
        [JSInvokable]
        public void OpenBottomFilter()
        {
            BottomFilterIsShow = true;
        }
        [JSInvokable]
        public void CloseBottomFilter()
        {
            BottomFilterIsShow = false;
        }
        public async Task ChangeSortProductsAsync(SortProductsType sortProductsType)
        {
            if (ProductCatalogViewModel.SortProductsType == sortProductsType)
                return;

            if (FilterIsShow)
                await CloseFilterAsync();
            if (BottomFilterIsShow)
                CloseBottomFilter();

            ProductCatalogViewModel.SortProductsType = sortProductsType;
            currentProductModelsQuery = BuildProductModelsQueryByCurrentFilters();
            CurrentPage = 1;
            ProductModelsIsLoading = true;
            StateHasChanged();
            selectedProductsModels = currentProductModelsQuery.Take(chunkProductModels).ToList();
            ProductModelsIsLoading = false;
        }

        public void FillFilters()
        {
            allColorsFilterDictionary = Db.ProductArticles
                .AsNoTracking()
                .Where(сategoryOrSubcategoryPredicateForArticle)
                .AsEnumerable()
                .GroupBy(productArticle => productArticle.Color)
                .ToDictionary(group => group.Key, group => false);
            allSizesFilterDictionary = Db.ProductArticles
                .AsNoTracking()
                .Where(сategoryOrSubcategoryPredicateForArticle)
                .AsEnumerable()
                .GroupBy(productArticle => productArticle.Size)
                .OrderBy(sizeGroup => sizeGroup.Key)
                .ToDictionary(group => group.Key, group => false);
            allBrandsFilterDictionary = Db.ProductModels
                .AsNoTracking()
                .Where(сategoryOrSubcategoryPredicateForModel)
                .AsEnumerable()
                .GroupBy(productModel => productModel.Brand)
                .ToDictionary(group => group.Key, group => false);
        }
        public void UpdateFilters()
        {
            if (allBrandsFilterDictionary.Any(brandKeyValuePair => brandKeyValuePair.Value))
                ProductCatalogViewModel.SelectedBrands = allBrandsFilterDictionary
                    .Where(brandKeyValuePair => brandKeyValuePair.Value)
                    .Select(brandKeyValuePair => brandKeyValuePair.Key);
            else
                ProductCatalogViewModel.SelectedBrands = allBrandsFilterDictionary.Select(brandKeyValuePair => brandKeyValuePair.Key);
            if (allColorsFilterDictionary.Any(colorKeyValuePair => colorKeyValuePair.Value))
                ProductCatalogViewModel.SelectedColors = allColorsFilterDictionary
                    .Where(colorKeyValuePair => colorKeyValuePair.Value)
                    .Select(colorKeyValuePair => colorKeyValuePair.Key);
            else
                ProductCatalogViewModel.SelectedColors = allColorsFilterDictionary.Select(colorKeyValuePair => colorKeyValuePair.Key);
            if (allSizesFilterDictionary.Any(sizeKeyValuePair => sizeKeyValuePair.Value))
                ProductCatalogViewModel.SelectedSizes = allSizesFilterDictionary
                    .Where(sizeKeyValuePair => sizeKeyValuePair.Value)
                    .Select(sizeKeyValuePair => sizeKeyValuePair.Key);
            else
                ProductCatalogViewModel.SelectedSizes = allSizesFilterDictionary.Select(sizeKeyValuePair => sizeKeyValuePair.Key);
        }
        private IQueryable<ProductModel> BuildProductModelsQueryByCurrentFilters()
        {
            IQueryable<ProductModel> productModelsQuery = Db.ProductModels.Where(сategoryOrSubcategoryPredicateForModel);

            if (minPrice != 0 && maxPrice != 0)
                productModelsQuery = productModelsQuery.Where(productModel => productModel.Price >= ProductCatalogViewModel.MinPrice && productModel.Price <= ProductCatalogViewModel.MaxPrice);

            if (ProductCatalogViewModel.SelectedBrands.Count() != 0)
                productModelsQuery = productModelsQuery.Where(productModel => ProductCatalogViewModel.SelectedBrands.Contains(productModel.Brand));
            if (ProductCatalogViewModel.SelectedColors.Count() != 0)
                productModelsQuery = productModelsQuery.Where(productModel => productModel.Articles.Any(productArticle => ProductCatalogViewModel.SelectedColors.Contains(productArticle.Color)));
            if (ProductCatalogViewModel.SelectedSizes.Count() != 0)
                productModelsQuery = productModelsQuery.Where(productModel => productModel.Articles.Any(productArticle => ProductCatalogViewModel.SelectedSizes.Contains(productArticle.Size)));

            if (ProductCatalogViewModel.SortProductsType == SortProductsType.AscendingPrices)
                productModelsQuery = productModelsQuery.OrderBy(productModel => productModel.Price);
            if (ProductCatalogViewModel.SortProductsType == SortProductsType.DescendingPrices)
                productModelsQuery = productModelsQuery.OrderByDescending(productModel => productModel.Price);

            return productModelsQuery;
        }

        public void AddProductInCart(ProductModel productModel)
        {
            if (currentUser.Cart.Products.Any(cartProduct => cartProduct.Article.Model.Id == productModel.Id))
                return;
            var addedFirstProductArticleOfModel = Db.ProductArticles.First(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0);
            currentUser.Cart.Products.Add(new CartProduct(addedFirstProductArticleOfModel, 1));
            Db.SaveChanges();
        }
        public void AddProductInFavorites(ProductModel productModel)
        {
            if (currentUser.FavoriteList.Products.Any(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id))
                return;
            var addedFirstProductArticleOfModel = Db.ProductArticles.First(productArticle => productArticle.Model.Id == productModel.Id && Db.Products.Count(product => product.Article.Id == productArticle.Id) != 0);
            currentUser.FavoriteList.Products.Add(new FavoriteProduct(addedFirstProductArticleOfModel));
            Db.SaveChanges();
        }
        public void RemoveProductFromCart(ProductModel productModel)
        {
            if (!currentUser.Cart.Products.Any(cartProduct => cartProduct.Article.Model.Id == productModel.Id))
                return;
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
        }
        public void RemoveProductFromFavorites(ProductModel productModel)
        {
            if (!currentUser.FavoriteList.Products.Any(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id))
                return;
            currentUser.FavoriteList.Products.RemoveAll(favoriteProduct => favoriteProduct.Article.Model.Id == productModel.Id);
            Db.SaveChanges();
        }

        public async Task SubmitFiltersAsync(EditContext editContext)
        {
            UpdateFilters();
            ValidateFilters(editContext);

            if (FilterIsShow)
                await CloseFilterAsync();
            if (BottomFilterIsShow)
                CloseBottomFilter();

            currentProductModelsQuery = BuildProductModelsQueryByCurrentFilters();

            ProductModelsIsLoading = true;
            StateHasChanged();
            selectedProductsModels = currentProductModelsQuery.AsNoTracking().Take(chunkProductModels).ToList();
            ProductModelsIsLoading = false;
            if (selectedProductsModels.Count() != 0)
            {
                minPrice = Math.Round(currentProductModelsQuery.AsNoTracking().Min(productModel => productModel.Price));
                maxPrice = Math.Round(currentProductModelsQuery.AsNoTracking().Max(productModel => productModel.Price));

                CurrentPage = 1;
                MaxPage = selectedProductsModels.Count() / chunkProductModels;
                if (MaxPage == 0)
                    MaxPage = 1;
            }
        }
        public void ValidateFilters(EditContext editContext)
        {
            ProductCatalogViewModelValidator.ValidateAndThrow(ProductCatalogViewModel);

            bool editContextValidateIsValid = editContext.Validate();
            if (!editContextValidateIsValid)
                new Exception("Валидация формы фильтров закончилась неуспешно, подробнее об ошибках:" + string.Join(" ; ", editContext.GetValidationMessages()));
        }
    }
}
