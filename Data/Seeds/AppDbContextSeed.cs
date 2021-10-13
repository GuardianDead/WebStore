using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Mocks;
using WebStore.Data.Mocks.CategoryMock;
using WebStore.Data.Mocks.DeliveryMock;
using WebStore.Data.Mocks.ProductArticleMock;
using WebStore.Data.Mocks.ProductModelMock;
using WebStore.Data.Mocks.RoleMock;
using WebStore.Data.Mocks.SubcategoryMock;
using WebStore.Data.Mocks.UserMock;

namespace WebStore.Data
{
    public class AppDbContextSeed : ISeedAsync
    {
        private readonly IDeliveryMock deliveryMock;
        private readonly ICategoryMock categoryMock;
        private readonly IRoleMock roleMock;
        private readonly ISubcategoryMock subcategoryMock;
        private readonly IUserMock userMock;
        private readonly IProductModelMock productModelMock;
        private readonly IProductArticleMock productArticleMock;
        private readonly IProductMock productMock;

        public AppDbContextSeed(IDeliveryMock deliveryMock, ICategoryMock categoryMock, IRoleMock roleMock,
            ISubcategoryMock subcategoryMock, IUserMock userMock, IProductModelMock productModelMock,
            IProductArticleMock productArticleMock, IProductMock productMock)
        {
            this.deliveryMock = deliveryMock;
            this.categoryMock = categoryMock;
            this.roleMock = roleMock;
            this.subcategoryMock = subcategoryMock;
            this.userMock = userMock;
            this.productModelMock = productModelMock;
            this.productArticleMock = productArticleMock;
            this.productMock = productMock;
        }

        public async ValueTask<bool> SeedAsync(CancellationToken cancellationToken = default)
        {
            var mocks = new IMockAsync[]
            {
                deliveryMock, categoryMock, roleMock,
                subcategoryMock, userMock, productModelMock,
                productArticleMock, productMock
            };

            var results = mocks.Select(async mock => await mock.InitAsync(cancellationToken));
            return await new ValueTask<bool>(results.All(result => result.Result == true));
        }
    }
}
