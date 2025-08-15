using ECommerceDemo.Data;
using ECommerceDemo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceDemo.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private const string ProductsKey = "ProductsCache";
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository repo, IMemoryCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        if (!_cache.TryGetValue(ProductsKey, out IEnumerable<Product> products))
        {
            products = await _repo.GetAllAsync();
            _cache.Set(ProductsKey, products);
        }

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock
        });
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null
            ? null
            : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };
        var created = await _repo.CreateAsync(product);

        //If someone adds a new product, the info in our cache (if it exists at that time) is invalid.
        _cache.Remove(ProductsKey);

        return new ProductDto
        {
            Id = created.Id,
            Name = created.Name,
            Price = created.Price,
            Stock = created.Stock
        };
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var updatedEntity = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };
        var updated = await _repo.UpdateAsync(id, updatedEntity);
        return updated == null
            ? null
            : new ProductDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Price = updated.Price,
                Stock = updated.Stock
            };
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
