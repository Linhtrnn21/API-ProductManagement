using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PM.AppServices;
using PM.Entities;
using PM.Models.Product;
using System.Security.Claims;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(null, null, _mockProductService.Object);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10, Quantity = 5, Description = "Description 1" },
            new Product { Id = 2, Name = "Product 2", Price = 20, Quantity = 3, Description = "Description 2" }
        };

        _mockProductService.Setup(service => service.GetProductsAsync(It.IsAny<string>()))
            .ReturnsAsync(products);

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnProducts = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(2, returnProducts.Count);
    }

    [Fact]
    public async Task GetProduct_ReturnsOkResult_WithExistingProduct()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product { Id = productId, Name = "Test Product", Price = 10, Quantity = 5, Description = "Test Description" };

        // Thiết lập mock cho IProductService
        _mockProductService
            .Setup(service => service.GetProductByIdAsync(productId, "test-user-id"))
            .ReturnsAsync(expectedProduct);

        // Tạo claims cho user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
    };
        var userIdentity = new ClaimsIdentity(claims, "TestAuthentication");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = userPrincipal };

        // Act
        var result = await _controller.GetProductById(productId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Product>>(result);
        var okResult = Assert.IsType<Product>(actionResult.Value);
        Assert.Equal(expectedProduct.Id, okResult.Id);
        Assert.Equal(expectedProduct.Name, okResult.Name);
    }


    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";

        _mockProductService.Setup(service => service.GetProductByIdAsync(1, userId))
            .ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProductById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostProduct_ReturnsOkResult()
    {
        // Arrange
        var productRequest = new ProductRequest { Name = "Product 1", Price = 10, Quantity = 5, Description = "Description 1" };

        // Tạo claims cho user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
    };
        var userIdentity = new ClaimsIdentity(claims, "TestAuthentication");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = userPrincipal };

        // Act
        var result = await _controller.PostProduct(productRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Product>>(result);
        Assert.IsType<OkResult>(actionResult.Result);
        _mockProductService.Verify(service => service.AddProductAsync(productRequest, "test-user-id"), Times.Once);
    }

    [Fact]
    public async Task PutProduct_ReturnsOkResult()
    {
        // Arrange
        var productRequest = new ProductRequest { Name = "Updated Product", Price = 15, Quantity = 10, Description = "Updated Description" };

        // Tạo claims cho user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
    };
        var userIdentity = new ClaimsIdentity(claims, "TestAuthentication");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = userPrincipal };

        // Act
        var result = await _controller.PutProduct(1, productRequest);

        // Assert
        Assert.IsType<OkResult>(result); // Đảm bảo loại trả về là OkResult
        _mockProductService.Verify(service => service.UpdateProductAsync(1, productRequest, "test-user-id"), Times.Once);
    }


    /// <summary>
    /// Kiểm tra xem phương thức DeleteProduct trả về OkResult khi sản phẩm tồn tại.
    /// </summary>
    [Fact]
    public async Task DeleteProduct_ReturnsOkResult_WhenProductExists()
    {
        // Arrange
        var productId = 1;

        // Thiết lập mock cho IProductService
        _mockProductService
            .Setup(service => service.DeleteProductAsync(productId, "test-user-id"))
            .Returns(Task.CompletedTask);

        // Tạo claims cho user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
    };
        var userIdentity = new ClaimsIdentity(claims, "TestAuthentication");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = userPrincipal };

        // Act
        var result = await _controller.DeleteProduct(productId);

        // Assert
        Assert.IsType<OkResult>(result);

        // Kiểm tra xem phương thức mock đã được gọi
        _mockProductService.Verify(service => service.DeleteProductAsync(productId, "test-user-id"), Times.Once);
    }

}