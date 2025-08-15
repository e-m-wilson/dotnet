# C# Resource Management & I/O

## 1. Unmanaged Resources

**Definition**: Resources not automatically managed by .NET Garbage Collector

- File handles
- Database connections
- Network sockets
- GDI objects

**Handling Requirements**:

- Must be explicitly released
- Failure to release causes resource leaks
- Implement `IDisposable` pattern

```csharp
public class FileHandler : IDisposable
{
   private FileStream _fileStream;
   
   public FileHandler(string path)
   {
       _fileStream = new FileStream(path, FileMode.Open);
   }
   
   // IDisposable implementation
   private bool _disposed = false;
   
   protected virtual void Dispose(bool disposing)
   {
       if (!_disposed)
       {
           if (disposing)
           {
               // Dispose managed resources
               _fileStream?.Dispose();
           }
           
           // Dispose unmanaged resources here
           _disposed = true;
       }
   }
   
   public void Dispose()
   {
       Dispose(true);
       GC.SuppressFinalize(this);
   }
   
   ~FileHandler() => Dispose(false);  // Finalizer
}
```

## 2. IDisposable Pattern

**Key Components**:

1. `Dispose()` method implementation
2. Protected virtual `Dispose(bool)` method
3. Finalizer (for unmanaged resources)
4. `GC.SuppressFinalize` call

**Best Practices**:

- Implement `IDisposable` when holding unmanaged resources
- Use `SafeHandle` for native resources
- Make class `sealed` if no inheritance needed

## 3. using Statements

**Automatic Resource Management**:

```csharp
// Single resource
using (var resource = new FileHandler("data.txt"))
{
   // Use resource
}  // Dispose() called automatically

// Multiple resources (C# 8+)
using var file = new FileStream("log.txt", FileMode.Append);
using var writer = new StreamWriter(file);
writer.WriteLine("New entry");  // Both disposed at scope exit
```

## 4. Serialization & Persistence

### JSON Serialization

```csharp
public class Person
{
   public string Name { get; set; }
   public int Age { get; set; }
}

// System.Text.Json
var person = new Person { Name = "Alice", Age = 30 };
string json = JsonSerializer.Serialize(person);
Person deserialized = JsonSerializer.Deserialize<Person>(json);
```

### Binary Serialization

```csharp
[Serializable]
public class DataPacket
{
   public int Id { get; set; }
   public byte[] Content { get; set; }
}

// Persistence
var formatter = new BinaryFormatter();
using var stream = new FileStream("data.bin", FileMode.Create);
formatter.Serialize(stream, new DataPacket());
```

### Entity Framework (Database)

```csharp
public class AppDbContext : DbContext
{
   public DbSet<Customer> Customers { get; set; }
}

using var context = new AppDbContext();
var customer = new Customer { Name = "Acme Corp" };
context.Customers.Add(customer);
context.SaveChanges();
```

## 5. Logging

### Basic Logging (Microsoft.Extensions.Logging)

```csharp
using Microsoft.Extensions.Logging;

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
   builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Application started");
logger.LogError(new Exception("Error"), "Critical failure");
```

### Serilog Configuration

```csharp
Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .WriteTo.File("log.txt")
   .CreateLogger();

Log.Information("Serilog initialized");
Log.CloseAndFlush();  // Required at shutdown
```

## 6. File I/O

### Text File Operations

```csharp
// Writing
File.WriteAllText("data.txt", "Hello World");  // Overwrites entire file
File.AppendAllText("log.txt", DateTime.Now + "\n");  // Adds to existing

// Reading
string content = File.ReadAllText("config.json");
string[] lines = File.ReadAllLines("data.csv");

// Stream-based
using var writer = new StreamWriter("output.txt");
await writer.WriteLineAsync("Stream content");

using var reader = new StreamReader("input.txt");
string line;
while ((line = await reader.ReadLineAsync()) != null)
{
   Console.WriteLine(line);
}
```

### Binary File Operations

```csharp
// Writing
using var binaryWriter = new BinaryWriter(File.Open("data.bin", FileMode.Create));
binaryWriter.Write(42);
binaryWriter.Write(3.14159);

// Reading
using var binaryReader = new BinaryReader(File.Open("data.bin", FileMode.Open));
int number = binaryReader.ReadInt32();
double pi = binaryReader.ReadDouble();
```

## Key Terminology

| Term                    | Definition                                                                 |
|-------------------------|---------------------------------------------------------------------------|
| **Finalizer**           | Method called by GC before object destruction (`~ClassName()`)            |
| **Serialization**       | Process of converting objects to storage/transmission format              |
| **Deserialization**     | Recreating objects from serialized format                                 |
| **Stream**              | Abstraction for byte sequence input/output                               |
| **Logger**              | Component recording application events for analysis                       |
| **Buffer**              | Temporary storage area for I/O operations                                 |

## Best Practices

1. **Resource Management**:

- Always use `using` blocks with disposable objects
- Avoid empty catch blocks swallowing exceptions
- Use `File.Exists()` checks judiciously (race conditions)

2. **Logging**:

- Use structured logging
- Avoid sensitive data in logs
- Implement log rotation

3. **Serialization**:

- Validate input during deserialization
- Use version-tolerant serialization
- Consider security implications

```csharp
// Safe file copy pattern
try
{
   File.Copy("source.txt", "dest.txt", overwrite: true);
}
catch (IOException ex) when (ex.HResult == -2147024864)
{
   Console.WriteLine("File in use - retrying...");
}
catch (UnauthorizedAccessException)
{
   Console.WriteLine("Permission denied");
}
