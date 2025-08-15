// Outgoing DTO sent to client

namespace ECommerceDemo.Models;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Stock { get; set; }
}
