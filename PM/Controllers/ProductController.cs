using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PM.AppServices;
using PM.DbContextt;
using PM.Entities;
using PM.Models.Product;
using System.Security.Claims;

[Authorize(Roles = "Admin, User")]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMemoryCache _cache;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IMemoryCache cache, IProductService productService)
    {
        _logger = logger;
        _cache = cache;
        _productService = productService;
    }

    /// <summary>
    /// Get list product
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _productService.GetProductsAsync(userId));
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var product = await _productService.GetProductByIdAsync(id, userId);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    /// <summary>
    /// Add product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> PostProduct(ProductRequest request)
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        await _productService.AddProductAsync(request, userId);
        return Ok();
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutProduct(int id, ProductRequest request)
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        await _productService.UpdateProductAsync(id, request, userId);
        return Ok();
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        await _productService.DeleteProductAsync(id, userId);
        return Ok();
    }
}