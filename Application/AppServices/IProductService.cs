using PM.Entities;
using PM.Models.Product;
using System.Collections;

namespace PM.AppServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string userId);
        Task<Product> GetProductByIdAsync(int id, string userId);
        Task AddProductAsync(ProductRequest productRequest, string userId);
        Task UpdateProductAsync(int id, ProductRequest productRequest, string userId);
        Task DeleteProductAsync(int id, string userId);

    }
}
