using ECommerceDemo.Models;

namespace ECommerceDemo.Data;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product updated);
    Task<bool> DeleteAsync(int id);
}