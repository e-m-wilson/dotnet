using ECommerceDemo.Models;
using ECommerceDemo.Services; // Since services contains the data/repo layer
using Microsoft.AspNetCore.Authorization;

// and the data/repo layer contains a reference to the model layer,
// we can gain access to the models transitively through this using for the service layer

using Microsoft.AspNetCore.Mvc;

namespace ECommerceDemo.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    // My controller is going to get an IProductService
    private readonly IProductService _service;

    //Constructor for my controller class, takes in an IProductService
    //during run time this is called by the DI Container as needed,
    // and the service object is injected in
    public ProductController(IProductService service)
    {
        _service = service;
    }

    // GET: /api/product
    // Returns all of the products in our DB
    //Our Controller methods return Tasks that resolve to a rather unique type
    //We end up with a task, wrapping an ActionResult,wrapping our actual return from
    //our service layer method.
    //ActionResult is just an object that represents an HTTP status code + a payload
    //that ASP.NET will serialize into JSON for us.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        try
        {
            return Ok(await _service.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //POST: api/products
    //Create a new product
    [Authorize]
    [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto dto)
    {
        try
        {
            //Explicitly checking the modelstate to make sure that out dto conforms
            //to whatever we need it to be
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            //If we pass model binding based on the rules we set via Data Annotations
            //inside of our CreateProductDto, and this object is created
            //We can not just echo back what the user sent in, but we can return
            //the actual object as it exists in our DB with its generated id and everything
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //GET: api/products/{id}
    //This will get a specific product by its id.
    //The id will come in from the route
    [AllowAnonymous] // Evil twin to [Authorize], allows non auth access to this method despite controller level auth attribute 
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);

        //If product is null (i.e. we didnt find one in the db for any reason)
        //we return a NotFound - 404
        //Otherwise return a 200 with the product in the body of the response
        return product == null ? NotFound() : Ok(product);
    }
}
