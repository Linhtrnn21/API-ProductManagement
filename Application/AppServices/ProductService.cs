using Microsoft.AspNetCore.Http.HttpResults;
using PM.Entities;
using PM.Helper;
using PM.Helper.Interface;
using PM.Models.Product;
using PM.Repository.UnitOfWork;
using System.Security.Claims;

namespace PM.AppServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggingHelper _loggingHelper;
        private readonly ICachingHelper _cachingHelper;

        /// <summary>
        /// Constructor ProductService
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductService(IUnitOfWork unitOfWork, ILoggingHelper loggingHelper, ICachingHelper cachingHelper)
        {
            _unitOfWork = unitOfWork;
            _loggingHelper = loggingHelper;
            _cachingHelper = cachingHelper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string userId)
        {
            _loggingHelper.LogUserRequest(userId, "get products");

            var product = await _cachingHelper.GetOrCreateAsync("productList", async () =>
            {
                var productList = await _unitOfWork.ProductRepository.GetAllAsync();
                _loggingHelper.LogSuccessfulRequest(userId, "product list", productList.Count());
                return productList;
            });

            return product;
        }

        public async Task<Product> GetProductByIdAsync(int id, string userId)
        {
            _loggingHelper.LogUserRequest(userId, $"get product by id = {id}");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                _loggingHelper.LogFailedRequest(userId, $"Not found product with id = {id}");
                return null;
            }

            _loggingHelper.LogSuccessfulRequest(userId, $"get product with id = {id}", 1);
            return product;

        }

        public async Task AddProductAsync(ProductRequest productRequest, string userId)
        {
            _loggingHelper.LogUserRequest(userId, "add product");

            var newProduct = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                Description = productRequest.Description,
            };

            await _unitOfWork.ProductRepository.AddAsync(newProduct);
            await _unitOfWork.CompleteAsync();

            _loggingHelper.LogSuccessfulRequest(userId, "add product", 1);
        }

        public async Task UpdateProductAsync(int id, ProductRequest productRequest, string userId)
        {
            _loggingHelper.LogUserRequest(userId, "update product");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Not found product with id = {id}");

            // Update
            product.Name = productRequest.Name;
            product.Price = productRequest.Price;
            product.Quantity = productRequest.Quantity;
            product.Description = productRequest.Description;

            await _unitOfWork.CompleteAsync();
            _loggingHelper.LogSuccessfulRequest(userId, "update product", 1);
        }

        public async Task DeleteProductAsync(int id, string userId)
        {
            _loggingHelper.LogUserRequest(userId, $"delete product with id {id}");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id {id} not found.");
            }

            // Xóa sản phẩm khỏi database
            await _unitOfWork.ProductRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            // Xóa cache liên quan đến danh sách sản phẩm
            _cachingHelper.Remove("productList");

            _loggingHelper.LogSuccessfulRequest(userId, $"deleted product with id {id}", 1);
        }
    }
}