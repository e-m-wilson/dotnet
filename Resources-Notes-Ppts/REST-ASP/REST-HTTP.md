# REST & HTTP 

## 1. Introduction to REST

**REST** (Representational State Transfer) is an architectural style for distributed systems, emphasizing:

- Resource-based interactions
- Stateless communication
- Standard HTTP methods
- Uniform interface design

## 2. REST Constraints (Detailed)

### 2.1 Client-Server Architecture

- **Separation of concerns**: UI/consumer (client) vs data storage/business logic (server)
- **Benefits**: Independent evolution, scalability, portability

### 2.2 Statelessness

- **Rule**: Server contains no client context between requests
- **Each request** must contain all necessary information
- **Benefits**: Reliability, visibility, scalability
- **Example**:

 ```http
 GET /orders/123 HTTP/1.1
 Authorization: Bearer xyz123
 ```

### 2.3 Cacheability

- **Rule**: Responses must define cacheability explicitly
- **Headers**: `Cache-Control`, `ETag`, `Last-Modified`
- **Benefits**: Reduced latency, server load reduction

### 2.4 Uniform Interface

Four sub-constraints:

#### a) Resource Identification

- Resources exposed as URIs
- Example: `/products/42` vs `/getProduct?id=42`

#### b) Representation Manipulation

- Clients interact via representations (JSON, XML)
- Example:

 ```json
 {"id":42,"name":"Widget","price":19.99}
 ```

#### c) Self-Descriptive Messages

- Messages contain enough information for processing
- Example headers:

 ```http
 Content-Type: application/json
 Accept-Language: en-US
 ```

#### d) HATEOAS (Hypermedia as the Engine of Application State)

- Responses contain links to related actions
- Example:

 ```json
 {
   "id": 42,
   "links": [
     {"rel": "update", "method": "PUT", "href": "/products/42"},
     {"rel": "delete", "method": "DELETE", "href": "/products/42"}
   ]
 }
 ```

### 2.5 Layered System

- Architecture composed of hierarchical layers
- **Components**: Proxies, gateways, load balancers
- **Benefits**: Security, scalability, encapsulation

### 2.6 Code on Demand (Optional)

- Server can extend client functionality via executable code
- Examples: JavaScript, Java applets

## 3. HTTP Basics

### Protocol Structure

| Component          | Description                               |
|--------------------|-------------------------------------------|
| **Request Line**   | `METHOD URI HTTP/VERSION`                 |
| **Headers**        | Key-value metadata (e.g., `Content-Type`) |
| **Body**           | Optional data payload                     |

### Example HTTP Flow

```http
GET /products/42 HTTP/1.1
Host: api.example.com
Accept: application/json
HTTP/1.1 200 OK
Content-Type: application/json
{
 "id": 42,
 "name": "Widget"
}
```

## 4. HTTP Verbs (Methods)

| Verb      | Safe | Idempotent | Usage                                  |
|-----------|------|------------|----------------------------------------|
| `GET`     | Yes  | Yes        | Retrieve resource                      |
| `POST`    | No   | No         | Create new resource                    |
| `PUT`     | No   | Yes        | Replace entire resource                |
| `PATCH`   | No   | No         | Partial resource update                |
| `DELETE`  | No   | Yes        | Remove resource                        |
| `HEAD`    | Yes  | Yes        | Get headers only                       |
| `OPTIONS` | Yes  | Yes        | List supported methods                 |

## 5. HTTP Status Codes

### Key Categories

| Code Range | Category           | Example Codes                          |
|------------|--------------------|----------------------------------------|
| 1xx        | Informational      | 100 Continue                           |
| 2xx        | Success            | 200 OK, 201 Created, 204 No Content    |
| 3xx        | Redirection        | 301 Moved Permanently, 304 Not Modified|
| 4xx        | Client Error       | 400 Bad Request, 401 Unauthorized, 404 Not Found|
| 5xx        | Server Error       | 500 Internal Server Error, 503 Service Unavailable|

## 6. Exposing Endpoints (ASP.NET Core Example)

```csharp
[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
   [HttpGet("{id}")]
   public IActionResult GetProduct(int id)
   {
       var product = _repository.GetProduct(id);
       return Ok(product);
   }
   [HttpPost]
   public IActionResult CreateProduct([FromBody] Product product)
   {
       var created = _repository.AddProduct(product);
       return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
   }
}
```

## 7. Consuming Endpoints (C# HttpClient)

```csharp
using var client = new HttpClient();
client.BaseAddress = new Uri("https://api.example.com");
var response = await client.GetAsync("/api/v1/products/42");
if (response.IsSuccessStatusCode)
{
   var product = await response.Content.ReadAsAsync<Product>();
   Console.WriteLine($"Product: {product.Name}");
}
```

## 8. REST Resource & URL Construction

### Best Practices

1. **Resources as Nouns**:

- Good: `/products`
- Bad: `/getAllProducts`

2. **Hierarchy**:

- `/stores/{storeId}/products`

3. **Versioning**:

- `/api/v1/products`

4. **Filtering/Sorting**:

- `/products?category=books&sort=price_desc`

5. **Pluralization**:

- `/users` instead of `/user`

### Example URL Structures

| Resource          | GET                     | POST              |
|-------------------|-------------------------|-------------------|
| Products          | `/products`             | `/products`       |
| Single Product    | `/products/42`          | -                 |
| Product Reviews   | `/products/42/reviews`  | `/products/42/reviews`|

## Key Terminology

| Term                | Definition                                                                 |
|---------------------|---------------------------------------------------------------------------|
| **Idempotent**      | Multiple identical requests → same effect as single request               |
| **Safe Method**     | Doesn't modify resource state (GET, HEAD, OPTIONS)                       |
| **Content Negotiation** | Client-server agreement on data format via `Accept`/`Content-Type` headers |
| **HATEOAS**         | Hypermedia-driven navigation through API endpoints                        |
| **ETag**            | Entity tag for resource versioning and caching                           |
| **Idempotency Key** | Unique identifier to prevent duplicate operations                        |

```http
# Example HATEOAS Response
GET /products/42
200 OK
{
 "id": 42,
 "name": "Widget",
 "_links": {
   "self": { "href": "/products/42" },
   "reviews": { "href": "/products/42/reviews" },
   "update": { "href": "/products/42", "method": "PUT" }
 }
}
```
