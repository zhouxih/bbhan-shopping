using Product.Api.Models;

namespace Product.Api.Services
{
    public interface IProductService
    {
        public Task AddProduct(ShopProduct product);
        public Task<IEnumerable<ShopProduct>> ProductList(int? PageIndex,int? PageSize);
        public Task<ShopProduct> GetProductById(Guid id);
        public Task<int> DeleteProductById(Guid id);
        public Task<ShopProduct> UpdateProduct(ShopProduct product);
    }
}
