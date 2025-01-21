using Marten;
using Marten.Internal.Sessions;
using Marten.Pagination;
using Product.Api.Models;

namespace Product.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDocumentSession documentSession;
        public ProductService(IDocumentSession documentSession) 
        {
            this.documentSession = documentSession;
        }

        public async Task AddProduct(ShopProduct product)
        {
            this.documentSession.Store(product);
            await this.documentSession.SaveChangesAsync();
        }

      

        public async Task<ShopProduct> GetProductById(Guid id)
        {
            var shopProduct = await this.documentSession.Query<ShopProduct>().Where(sp => sp.Id == id).FirstOrDefaultAsync();
            return shopProduct;
        }

        public async Task<IEnumerable<ShopProduct>> ProductList(int? PageIndex, int? PageSize)
        {
            var list = await this.documentSession.Query<ShopProduct>().ToPagedListAsync(PageIndex ?? 1, PageSize ?? 10);
            return list;
        }

        public async Task<ShopProduct> UpdateProduct(ShopProduct product)
        {
            this.documentSession.Update<ShopProduct>(product);
            await this.documentSession.SaveChangesAsync();
            return await this.documentSession.Query<ShopProduct>()
                .Where(sp=>sp.Id == product.Id).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteProductById(Guid id)
        {
            this.documentSession.Delete<ShopProduct>(id);
            await this.documentSession.SaveChangesAsync();
            return 1;
        }
    }
}
