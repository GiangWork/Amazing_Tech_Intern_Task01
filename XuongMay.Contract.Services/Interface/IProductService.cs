using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.ProductModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IProductService
    {
        Task<Product> CreateProduct(ProductModelView request);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
        Task<Product> UpdateProduct(string id, ProductModelView request);
        Task<bool> DeleteProduct(string id);
    }
}
