
# ASP.NET

## 1. Service-Oriented Architecture (SOA)

**Definition**: Architectural pattern where components expose and consume services over a network  
**Key Principles**:

- **Service Contracts**: Well-defined interfaces (OpenAPI/Swagger)
- **Loose Coupling**: Services operate independently
- **Reusability**: Shared business functionality
- **Discoverability**: Services are self-describing
- **Composability**: Combine services for complex workflows
**Benefits**:
- Technology agnosticism
- Scalability
- Independent deployment
- Fault isolation

## 2. ASP.NET Core Overview

**Web API Framework Features**:

- Cross-platform (Windows/Linux/macOS)
- High-performance Kestrel web server
- Built-in Dependency Injection
- Middleware pipeline
- Configuration system
- Integrated authentication/authorization
- OpenAPI support
**Key Components**:
| Component         | Purpose                                  |
|-------------------|------------------------------------------|
| **Host**          | Manages app startup and lifetime         |
| **Middleware**    | Request processing pipeline              |
| **Controllers**   | Handle HTTP requests/responses           |
| **Services**      | Business logic components                |
| **Configuration** | App settings and environment variables   |

## 3. ASP.NET Web API Types

### 3.1 Minimal APIs

**Simplified syntax** for small services:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/products/{id}", async (int id, ProductDb db) =>
   await db.Products.FindAsync(id) is Product product
       ? Results.Ok(product)
       : Results.NotFound());
app.Run();
```

### 3.2 Controller-Based APIs

**Traditional approach** for complex services:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
   private readonly ProductService _service;
   public ProductsController(ProductService service) => _service = service;
   [HttpGet("{id}")]
   public async Task<IActionResult> GetProduct(int id)
   {
       var product = await _service.GetProductAsync(id);
       return product is null ? NotFound() : Ok(product);
   }
}
```

### Comparison

| Feature              | Minimal API              | Controller-Based API        |
|----------------------|--------------------------|-----------------------------|
| **Entry Point**      | Program.cs               | Controller classes          |
| **Routing**          | Map* methods             | Attribute routing           |
| **Complexity**       | Simple endpoints         | Full MVC features           |
| **DI Integration**   | Parameter binding        | Constructor injection       |
| **Validation**       | Manual                   | Automatic via attributes    |
| **Best For**         | Microservices, simple APIs | Enterprise applications   |

## 4. HTTP Request Lifecycle

1. **Request Receipt**: Kestrel receives HTTP request
2. **Middleware Pipeline**:

- Authentication
- Routing
- CORS
- Compression
- Custom middleware

3. **Endpoint Selection**: Route matching
4. **Controller Instantiation** (if using controllers)
5. **Action Execution**:

- Model binding
- Validation
- Business logic

6. **Result Processing**:

- Serialization
- Status code handling

7. **Response Transmission**

## 5. Controllers & Model Binding

### Controller Responsibilities

- Handle incoming HTTP requests
- Orchestrate business logic
- Return appropriate responses
- Manage status codes
- Handle errors

### Model Binding

**Automatic mapping** of request data to parameters:

```csharp
[HttpPost]
public IActionResult CreateProduct([FromBody] Product product,
                                 [FromQuery] bool validate = true)
{
   // product populated from request body
   // validate parameter from query string
}
```

**Binding Sources**:

| Attribute       | Source                      |
|-----------------|-----------------------------|
| `[FromBody]`    | Request body                |
| `[FromQuery]`   | Query string parameters     |
| `[FromRoute]`   | Route parameters            |
| `[FromHeader]`  | Request headers             |
| `[FromForm]`    | Form data                   |

## 6. Creating a New Web API

### Step 1: Create Project

```bash
# Minimal API
dotnet new web -n ProductApi -o src/ProductApi
# Controller-Based API
dotnet new webapi -n ProductApi -o src/ProductApi
```

### Step 2: Add Core Services

```csharp
var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddDbContext<ProductDb>(opt =>
   opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### Step 3: Configure Middleware

```csharp
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();  // For controller-based APIs
app.Run();
```

### Step 4: Implement Endpoints

**Minimal API Example**:

```csharp
app.MapGet("/products", async (ProductDb db) =>
   await db.Products.ToListAsync());
app.MapPost("/products", async (Product product, ProductDb db) =>
{
   db.Products.Add(product);
   await db.SaveChangesAsync();
   return Results.Created($"/products/{product.Id}", product);
});
```

**Controller-Based Example**:

```csharp
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
   private readonly ProductDb _db;
   public ProductsController(ProductDb db) => _db = db;
   [HttpGet]
   public async Task<ActionResult<List<Product>>> GetAll() =>
       await _db.Products.ToListAsync();
}
```

## 7. API Testing

**Built-in Tools**:

- Swagger UI (`/swagger`)
- Postman/Thunder Client
- Unit tests with `Microsoft.AspNetCore.Mvc.Testing`
**Example Integration Test**:

```csharp
public class ProductsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
   private readonly HttpClient _client;
   public ProductsApiTests(WebApplicationFactory<Program> factory)
   {
       _client = factory.CreateClient();
   }
   [Fact]
   public async Task GET_ReturnsProductsList()
   {
       var response = await _client.GetAsync("/products");
       response.EnsureSuccessStatusCode();
       Assert.Equal("application/json",
           response.Content.Headers.ContentType?.MediaType);
   }
}
```

## Key Concepts

| Term                 | Description                                                                 |
|----------------------|-----------------------------------------------------------------------------|
| **Endpoint**         | URL + HTTP method combination handling requests                            |
| **DTO**              | Data Transfer Object - schema for API input/output                         |
| **Content Negotiation** | Automatic response format selection (JSON/XML)                          |
| **Route Constraints**| `{id:int}`, `{active:bool}`, `{date:datetime}`                             |
| **API Versioning**   | Manage breaking changes via URL/header/query parameter                     |
| **Rate Limiting**    | Control request frequency using `Microsoft.AspNetCore.RateLimiting`       |

```csharp
// Advanced Minimal API Example
app.MapPost("/upload", async (IFormFile file, FileService service) =>
{
   var result = await service.ProcessFileAsync(file);
   return Results.Ok(result);
})
.Accepts<IFormFile>("multipart/form-data")
.Produces<FileResult>(200)
.WithTags("File Operations");
```
