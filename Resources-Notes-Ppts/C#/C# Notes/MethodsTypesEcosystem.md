# Methods, Types, Ecosystem

## 1. C# Coding Conventions

**Official Microsoft Guidelines** for consistent code:

- **Naming**:
- PascalCase: `public void CalculateTotal()`
- camelCase: `private int _itemCount`
- Interfaces: `ILogger` (prefix with I)
- **Braces**: Allman style (braces on new line)
- **Spacing**: Space after `if`, before `{`
- **LINQ**: Format queries with line breaks

```csharp
// Good Example
public class OrderProcessor
{
   private readonly ILogger _logger;
   public void ProcessOrder(Order order)
   {
       if (order.IsValid)
       {
           var results = from item in order.Items
                         where item.Quantity > 0
                         select item.Price * item.Quantity;
       }
   }
}
```

## 2. Method Overloading

Create multiple methods with same name but different parameters:

```csharp
public class Calculator
{
   public int Add(int a, int b) => a + b;
   public double Add(double a, double b) => a + b;
   public int Add(int a, int b, int c) => a + b + c;
}
```

**Rules**:

- Must differ in parameter types or count
- Cannot overload by return type alone
- Can combine with optional parameters

## 3. Strings

**Key Features**:

- Immutable Unicode character sequences
- `System.String` reference type
- New features in C# 11: raw string literals

```csharp
// Common Operations
string name = "Alice";
string message = $"Hello {name}!";  // Interpolation
string path = @"C:\Users\";        // Verbatim
string multiline = """
   This is a
   multi-line string
   """;
// Important Methods
string.Join(", ", new[] { "A", "B" });
int index = message.IndexOf("lo");
string substr = message.Substring(3, 5);
```

## 4. Arrays

**Fixed-size** collections of same-type elements:

```csharp
// Single-dimensional
int[] numbers = new int[5] { 1, 2, 3, 4, 5 };
// Multidimensional (rectangular)
int[,] matrix = new int[2,3] { {1,2,3}, {4,5,6} };
// Jagged (array of arrays)
int[][] jagged = new int[2][];
jagged[0] = new int[3];
jagged[1] = new int[2];
```

## 5. Main Method

**Entry point** of application:

```csharp
// Traditional
static void Main(string[] args)
{
   // Program logic
}
// Top-level statements (C# 9+)
Console.WriteLine("Simplified entry point");
```

**Overloads**:

- `static int Main()` for return codes
- `static async Task Main()` for async

## 6. Debugger

**Essential Tools**:

- Breakpoints (conditional/function)
- Watch/Locals/Autos windows
- Call Stack and Immediate Window
- Exception Settings
**Debugging Techniques**:

```csharp
// Debugger Attributes
[DebuggerDisplay("Product {Name}")]
public class Product
{
   [DebuggerBrowsable(DebuggerBrowsableState.Never)]
   public string InternalCode { get; }
}
```

## 7. Project References

**Two Main Types**:

1. **Project References** (local solutions):

  ```xml
<ItemGroup>
<ProjectReference Include="..\SharedLib\SharedLib.csproj" />
</ItemGroup>
  ```

2. **Package References** (NuGet):

  ```xml
<ItemGroup>
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
</ItemGroup>
  ```

**CLI Commands**:

```bash
dotnet add reference ../OtherProject/OtherProject.csproj
dotnet add package Newtonsoft.Json
```

## 8. Project Types

| Template              | Command                     | Description                  |
|-----------------------|-----------------------------|------------------------------|
| Console App           | `dotnet new console`        | Command-line applications    |
| Class Library         | `dotnet new classlib`       | Reusable components          |
| ASP.NET Core Web App  | `dotnet new webapp`         | Web applications             |
| xUnit Test Project    | `dotnet new xunit`          | Unit tests                   |
| WPF Application       | `dotnet new wpf`            | Desktop GUI apps             |

## 9. NuGet & Package References

**Package Manager Workflow**:

1. Search packages at nuget.org
2. Add to .csproj:

  ```xml
<PackageReference Include="Serilog" Version="3.0.1" />
  ```

3. Restore packages: `dotnet restore`
**Version Syntax**:

- `1.0.0`: Exact version
- `[1.0,2.0)`: Version range
- `3.*`: Latest 3.x

## 10. Common Language Infrastructure (CLI)

**.NET Execution Foundation**:

- **CTS** (Common Type System): Defines all data types
- **CLS** (Common Language Specification): Cross-language interoperability rules
- **VES** (Virtual Execution System): Runtime environment
- **Metadata**: Type information embedded in assemblies
**Key Benefits**:
- Language interoperability
- Cross-platform execution
- Managed code execution

## 11. Automatic Memory Management

**Garbage Collection Process**:

1. **Mark**: Identify referenced objects
2. **Sweep**: Remove unreachable objects
3. **Compact**: Reorganize memory (optional)
**Generations**:
| Generation | Description                  |
|------------|------------------------------|
| Gen 0      | Short-lived objects          |
| Gen 1      | Buffer between Gen 0 and 2   |
| Gen 2      | Long-lived objects           |
**Best Practices**:

```csharp
// Dispose unmanaged resources
public class ResourceHolder : IDisposable
{
   private Stream _stream;
   public void Dispose()
   {
       _stream?.Dispose();
       GC.SuppressFinalize(this);
   }
}
// Using statement
using (var resource = new ResourceHolder())
{
   // Auto-disposed
}
```

# Key Terminology Expansion

| Term                     | Definition                                                                 |
|--------------------------|---------------------------------------------------------------------------|
| **JIT Compilation**      | Converts IL to native code during execution                               |
| **Nullable Reference**   | C# 8+ feature requiring explicit null checks                             |
| **Span<T>**              | Stack-allocated memory region for high-performance scenarios              |
| **Roslyn**               | C#/VB.NET compiler platform                                               |
| **IL (CIL)**             | Intermediate Language executed by CLR                                    |
| **Assembly**             | Compiled .NET unit (.dll/.exe) containing IL and metadata                 |
