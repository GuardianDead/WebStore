using Microsoft.Extensions.DependencyInjection;
using WebStore.Data;
using WebStore.Data.Mocks;
using WebStore.Data.Mocks.CategoryMock;
using WebStore.Data.Mocks.DeliveryMock;
using WebStore.Data.Mocks.OrderMock;
using WebStore.Data.Mocks.ProductArticleMock;
using WebStore.Data.Mocks.ProductMock;
using WebStore.Data.Mocks.ProductModelMock;
using WebStore.Data.Mocks.RoleMock;
using WebStore.Data.Mocks.SubcategoryMock;
using WebStore.Data.Mocks.UserMock;

namespace WebStore.Domain.Configurations
{
    public static class MockDependenciesConfiguration
    {
        public static IServiceCollection AddMockDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryMock, CategoryMock>();
            services.AddScoped<IRoleMock, RoleMock>();
            services.AddScoped<IDeliveryMock, DeliveryMock>();
            services.AddScoped<ISubcategoryMock, SubcategoryMock>();
            services.AddScoped<IProductModelMock, ProductModelMock>();
            services.AddScoped<IProductArticleMock, ProductArticleMock>();
            services.AddScoped<IProductMock, ProductMock>();
            services.AddScoped<IOrderMock, OrderMock>();
            services.AddScoped<IUserMock, UserMock>();

            services.AddScoped<AppDbContextSeed>();

            return services;
        }
    }
}
