// --- Entity/Model: Represents the DB table structure ---

namespace ECommerceDemo.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Stock { get; set; }
}
