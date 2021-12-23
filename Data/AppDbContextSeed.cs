using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Mocks;
using WebStore.Data.Mocks.CategoryMock;
using WebStore.Data.Mocks.DeliveryMock;
using WebStore.Data.Mocks.OrderMock;
using WebStore.Data.Mocks.ProductArticleMock;
using WebStore.Data.Mocks.ProductModelMock;
using WebStore.Data.Mocks.RoleMock;
using WebStore.Data.Mocks.SubcategoryMock;
using WebStore.Data.Mocks.UserMock;

namespace WebStore.Data
{
    public class AppDbContextSeed
    {
        private readonly IDeliveryMock deliveryMock;
        private readonly ICategoryMock categoryMock;
        private readonly IRoleMock roleMock;
        private readonly ISubcategoryMock subcategoryMock;
        private readonly IUserMock userMock;
        private readonly IProductModelMock productModelMock;
        private readonly IProductArticleMock productArticleMock;
        private readonly IProductMock productMock;
        private readonly IOrderMock orderMock;

        public AppDbContextSeed(IDeliveryMock deliveryMock, ICategoryMock categoryMock, IRoleMock roleMock,
            ISubcategoryMock subcategoryMock, IUserMock userMock, IProductModelMock productModelMock,
            IProductArticleMock productArticleMock, IProductMock productMock, IOrderMock orderMock)
        {
            this.deliveryMock = deliveryMock;
            this.categoryMock = categoryMock;
            this.roleMock = roleMock;
            this.subcategoryMock = subcategoryMock;
            this.userMock = userMock;
            this.productModelMock = productModelMock;
            this.productArticleMock = productArticleMock;
            this.productMock = productMock;
            this.orderMock = orderMock;
        }

        public async ValueTask<bool> SeedAsync(CancellationToken cancellationToken = default)
        {
            var mocks = new IMockAsync[]
            {
                deliveryMock, categoryMock, roleMock,
                subcategoryMock, productModelMock,
                productArticleMock, productMock, orderMock, userMock
            };

            var results = new List<bool>();
            foreach (IMockAsync mock in mocks)
                results.Add(await mock.InitAsync(cancellationToken));

            return results.All(result => result);
        }
    }
}
