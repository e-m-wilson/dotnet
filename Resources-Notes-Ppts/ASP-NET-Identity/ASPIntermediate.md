
# Advanced ASP.NET 

## 1. Routing

**Definition**: Mechanism to map incoming HTTP requests to API endpoints  
**Approaches**:

### 1.1 Convention-Based Routing (Minimal APIs)

```csharp
app.MapGet("/products", () => /* ... */);
app.MapPost("/products/{id}", (int id) => /* ... */);
```

### 1.2 Attribute Routing (Controller-Based)

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
   [HttpGet("{id:int}")]  // Route constraint
   public IActionResult GetProduct(int id) => /* ... */
}
```

**Route Constraints**:

| Constraint      | Example              | Description                   |
|-----------------|----------------------|-------------------------------|
| `int`           | `{id:int}`           | Matches integers              |
| `guid`          | `{userId:guid}`      | Matches GUIDs                 |
| `regex(...)`    | `{code:regex(^\\d{3}$)}` | Regular expression pattern  |
| `range`         | `{age:range(18,120)}`| Value range validation        |

---

## 2. Data Annotations

**Validation Attributes**:

```csharp
public class ProductCreateDto
{
   [Required(ErrorMessage = "Name is required")]
   [StringLength(100, MinimumLength = 3)]
   public string Name { get; set; }
   [Range(0.01, 10000)]
   public decimal Price { get; set; }
   [EmailAddress]
   public string SupportEmail { get; set; }
   [Url]
   public string ProductPage { get; set; }
}
```

**Validation in Controllers**:

```csharp
[HttpPost]
public IActionResult CreateProduct([FromBody] ProductCreateDto dto)
{
   if (!ModelState.IsValid)
   {
       return BadRequest(ModelState);
   }
   // Process valid DTO
}
```

---

## 3. Data Transfer Objects (DTOs)

**Pattern**: Decouple internal entities from API contracts  
**Common Types**:

- **Request DTO**: Input validation schema
- **Response DTO**: Output shaping and serialization
**Example Structure**:

```csharp
// Entity
public class Product
{
   public int Id { get; set; }
   public string Name { get; set; }
   public decimal Price { get; set; }
   public string InternalCode { get; set; }
}
// Request DTO
public record ProductCreateDto(string Name, decimal Price);
// Response DTO
public record ProductResponse(int Id, string Name, decimal Price);
```

**AutoMapper Configuration**:

```csharp
public class ProductProfile : Profile
{
   public ProductProfile()
   {
       CreateMap<ProductCreateDto, Product>();
       CreateMap<Product, ProductResponse>();
   }
}
```

---

## 4. API Versioning

**Package**: `Microsoft.AspNetCore.Mvc.Versioning`  
**Strategies**:

### 4.1 URL Path Versioning

```csharp
services.AddApiVersioning(options => {
   options.DefaultApiVersion = new ApiVersion(1, 0);
   options.ReportApiVersions = true;
});
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController : ControllerBase
{
   [MapToApiVersion("2.0")]
   [HttpGet]
   public IActionResult GetProductsV2() => /* ... */
}
```

### 4.2 Header Versioning

```csharp
services.AddApiVersioning(options => {
   options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});
```

### 4.3 Query String Versioning

```http
GET /api/products?api-version=2.0
```

---

## 5. OpenAI Integration

**Example API Integration**:

```csharp
[HttpPost("generate-description")]
public async Task<IActionResult> GenerateProductDescription(
   [FromBody] ProductDescriptionRequest request)
{
   var openAIClient = new OpenAIClient(Configuration["OpenAI:ApiKey"]);
   var response = await openAIClient.Completions.CreateCompletion(
       new CompletionCreateOptions {
           Prompt = $"Generate product description for {request.ProductName}",
           MaxTokens = 200
       });
   return Ok(new { description = response.Choices[0].Text });
}
```

**Security Considerations**:

- Store API keys in Secret Manager or Azure Key Vault
- Implement rate limiting
- Add input validation
- Use Polly for resilience

---

## 6. Swagger/OpenAPI Documentation

**Configuration**:

```csharp
builder.Services.AddSwaggerGen(c => {
   c.SwaggerDoc("v1", new OpenApiInfo {
       Title = "Product API",
       Version = "v1",
       Description = "API for managing products",
       Contact = new OpenApiContact { Name = "Support", Email = "support@company.com" }
   });
   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
       Type = SecuritySchemeType.Http,
       Scheme = "bearer"
   });
});
```

**Annotating Endpoints**:

```csharp
[HttpGet("{id}")]
[ProducesResponseType(typeof(ProductResponse), 200)]
[ProducesResponseType(404)]
[SwaggerOperation(
   Summary = "Get product by ID",
   Description = "Retrieves a single product with full details")]
public IActionResult GetProduct(int id) => /* ... */
```

**Testing Endpoints**:

```
http://localhost:5000/swagger
```

---

# Best Practices

| Category          | Recommendation                                      |
|-------------------|----------------------------------------------------|
| **Routing**       | Use attribute routing for complex APIs             |
| **Validation**    | Combine data annotations with Fluent Validation    |
| **DTOs**          | Separate read/write models                        |
| **Versioning**    | Start versioning early, use semantic versioning    |
| **Documentation** | Keep Swagger docs updated with code changes        |
| **AI Integration**| Implement circuit breakers for external services   |

```csharp
// Advanced DTO Example with Validation
public record UserCreateDto(
   [Required][EmailAddress] string Email,
   [Required][StringLength(100, MinimumLength = 8)] string Password,
   [Required][Phone] string PhoneNumber);
```
