# ASP.NET Core Advanced

## 1. Middleware

**Definition**: Components in request pipeline handling cross-cutting concerns  
**Execution Order**: Critical (First registered = First executed except terminal middleware)

### Common Built-in Middleware

| Middleware              | Purpose                                | Typical Position |
|-------------------------|----------------------------------------|------------------|
| `UseExceptionHandler`   | Global error handling                  | Early            |
| `UseHttpsRedirection`   | HTTPS enforcement                      | Early            |
| `UseRouting`            | Endpoint routing                      | Mid              |
| `UseAuthentication`     | AuthN/AuthZ                           | After routing    |
| `UseCors`               | Cross-Origin Resource Sharing         | After routing    |
| `UseResponseCaching`    | Response caching                      | Before endpoints |
**Custom Middleware Example**:

```csharp
app.Use(async (context, next) =>
{
   var start = Stopwatch.GetTimestamp();
   await next();
   var elapsed = Stopwatch.GetElapsedTime(start);
   context.Response.Headers.Append("X-Processing-Time", elapsed.ToString());
});
```

## 2. Filters

**Definition**: Pipeline components handling pre/post action processing  
**Filter Types**:

| Filter Type         | Interface               | Execution Timing            |
|---------------------|-------------------------|------------------------------|
| Authorization       | `IAuthorizationFilter`  | Before action execution      |
| Action              | `IActionFilter`         | Before/after action          |
| Exception           | `IExceptionFilter`      | On unhandled exceptions      |
| Result              | `IResultFilter`         | Before/after action result   |
**Action Filter Example**:

```csharp
public class LogActionFilter : IActionFilter
{
   public void OnActionExecuting(ActionExecutingContext context)
   {
       Log.Info($"Starting {context.ActionDescriptor.DisplayName}");
   }
   public void OnActionExecuted(ActionExecutedContext context)
   {
       Log.Info($"Completed {context.ActionDescriptor.DisplayName}");
   }
}
// Registration
services.AddControllers(options =>
{
   options.Filters.Add<LogActionFilter>();
});
```

## 3. CORS (Cross-Origin Resource Sharing)

**Security Protocol**: Controls cross-domain requests  
**Configuration**:

```csharp
// Startup.cs
services.AddCors(options =>
{
   options.AddPolicy("AllowClient", policy =>
   {
       policy.WithOrigins("https://client-app.com")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials();
   });
});
app.UseCors("AllowClient");
```

**Endpoint-Level Control**:

```csharp
[EnableCors("AllowClient")]
[HttpGet("public-data")]
public IActionResult GetPublicData() => /* ... */;
[DisableCors]
[HttpGet("internal-data")]
public IActionResult GetInternalData() => /* ... */;
```

## 4. Caching

### 4.1 Response Caching

```csharp
[HttpGet("cache-me")]
[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
public IActionResult GetCacheableData() => Ok(DateTime.UtcNow);
```

**Server-Side Caching**:

```csharp
// Startup
services.AddResponseCaching();
app.UseResponseCaching();
// Controller
[ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "category" })]
public IActionResult GetProducts(string category) => /* ... */;
```

### 4.2 In-Memory Caching

```csharp
services.AddMemoryCache();
public class DataService
{
   private readonly IMemoryCache _cache;
   public DataService(IMemoryCache cache) => _cache = cache;
   public async Task<Data> GetData()
   {
       return await _cache.GetOrCreateAsync("data-key", async entry =>
       {
           entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
           return await FetchFromDatabase();
       });
   }
}
```

### 4.3 Distributed Caching

```csharp
// Redis example
services.AddStackExchangeRedisCache(options =>
{
   options.Configuration = Configuration.GetConnectionString("Redis");
});
[HttpGet("cached")]
public async Task<IActionResult> GetCachedData()
{
   var data = await _distributedCache.GetStringAsync("data-key");
   if (data == null)
   {
       data = GetFreshData();
       await _distributedCache.SetStringAsync("data-key", data, new DistributedCacheEntryOptions
       {
           AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
       });
   }
   return Ok(data);
}
```

---

# Key Concepts Comparison

## Middleware vs Filters

| Aspect          | Middleware                          | Filters                          |
|-----------------|-------------------------------------|----------------------------------|
| Scope           | Entire request pipeline            | MVC action processing            |
| Execution Order | First registered → First executed  | Filter type determines order     |
| Access          | HttpContext                        | ActionContext                   |
| Use Cases       | Global concerns (e.g., logging)     | Action-specific logic (e.g., validation) |

## Caching Strategies

| Type             | Scope                 | Best For                          |
|------------------|-----------------------|-----------------------------------|
| Client-Side      | Browser cache         | Static assets, public data        |
| Server-Side      | Application memory    | Frequently accessed dynamic data  |
| Distributed      | External cache store  | Scalable applications, multi-node |

---

# Best Practices

1. **Middleware Ordering**

- Exception handling first
- Static files before routing
- Authentication before authorization

2. **CORS Security**

- Avoid `AllowAnyOrigin()` with `AllowCredentials()`
- Prefer specific origins over wildcards
- Use environment-specific configurations

3. **Cache Management**

- Set appropriate expiration times
- Use cache-busting for static assets
- Implement cache invalidation strategies

4. **Filter Usage**

- Prefer action filters over middleware for controller-specific logic
- Use authorization filters for authentication checks
- Handle exceptions globally with middleware or filters

```csharp
// Advanced Cache Profile Configuration
services.AddControllers(options =>
{
   options.CacheProfiles.Add("Default30", new CacheProfile
   {
       Duration = 30,
       Location = ResponseCacheLocation.Any
   });
});
// Controller Usage
[ResponseCache(CacheProfileName = "Default30")]
public IActionResult Get() => /* ... */;
```
