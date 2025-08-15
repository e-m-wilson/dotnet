// Incoming DTO used when creating new product

using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Models;

public class CreateProductDto
{
    //Inside of my DTOs (since these will be used only by my controllers)
    //I can use data annotations to customize the model binding rules
    //based on my business needs
    [Required] // This field is required and cannot be left off the JSON sent in
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 10000)] //Setting some upper and lower bounds for this field
    public float Price { get; set; }

    [Range(0, 1000)]
    public int Stock { get; set; }
}
