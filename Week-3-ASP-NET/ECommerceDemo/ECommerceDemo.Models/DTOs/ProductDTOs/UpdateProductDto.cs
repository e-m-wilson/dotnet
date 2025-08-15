// Incoming DTO used for updating product

namespace ECommerceDemo.Models;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;

    public float Price { get; set; }

    public int Stock { get; set; }
}
