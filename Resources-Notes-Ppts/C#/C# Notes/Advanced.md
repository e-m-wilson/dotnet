# C# Advanced Concepts

## 1. Regular Expressions (RegEx)

**Definition**: Patterns used to match and manipulate text strings based on character sequences  
**Key Components**:

- **Metacharacters**: Special symbols like `\d` (digit), `\w` (word character), `.` (any character)
- **Quantifiers**: `*` (0+), `+` (1+), `{n}` (exact count)
- **Groups**: `()` for capturing subpatterns
- **Anchors**: `^` (start), `$` (end)

```csharp
// Validate email format
var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
bool isValidEmail = Regex.IsMatch("user@domain.com", emailPattern);
```

## 2. Pattern Matching

**Definition**: Technique to test values against structural patterns  
**Types**:

- **Type Pattern**: `if (obj is string s)`
- **Property Pattern**: `case { Length: >5 }`
- **Positional Pattern**: `case (int x, int y)`
- **Relational Pattern**: `case > 100`

```csharp
public string GetShapeInfo(object shape) => shape switch {
   Circle { Radius: 0 }           => "Point",
   Circle c                       => $"Circle R={c.Radius}",
   Rectangle r when r.W == r.H    => $"Square {r.W}x{r.H}",
   Rectangle                      => "Rectangle",
   _                              => "Unknown shape"
};
```

## 3. Structs vs Classes

**Definition**: Value type (struct) vs Reference type (class)  
**Key Differences**:

| **Characteristic**       | **Struct**                            | **Class**                     |
|--------------------------|---------------------------------------|-------------------------------|
| Memory Allocation        | Stack (usually)                       | Heap                          |
| Inheritance              | Implements interfaces                 | Full inheritance support      |
| Nullability              | Cannot be null (except Nullable<T>)   | Can be null                   |
| Default Constructor      | Cannot define                         | Auto-generated if undefined   |
| Mutability               | Should be immutable                   | Typically mutable             |

```csharp
public readonly struct Point3D {
   public readonly double X, Y, Z;
   public Point3D(double x, double y, double z) => (X, Y, Z) = (x, y, z);
}
```

## 4. Advanced Type Conversions

**Definition**: Custom rules for type transformations  
**Techniques**:

- **Implicit**: Automatic conversion (no data loss)
- **Explicit**: Cast required (potential loss)
- **User-defined**: Custom operator overloading

```csharp
public class Temperature {
   private double Celsius { get; }
   public Temperature(double c) => Celsius = c;
   // Implicit: Celsius to Fahrenheit
   public static implicit operator double(Temperature t) => t.Celsius * 9/5 + 32;
   // Explicit: Double to Temperature
   public static explicit operator Temperature(double f) => new Temperature((f - 32) * 5/9);
}
```

## 5. Generics

**Definition**: Create type-agnostic classes/methods  
**Key Features**:

- Type safety without multiple implementations
- Constraints (`where T : ...`)
- Covariance/Contravariance (`in/out`)

```csharp
public class GenericRepository<T> where T : IEntity, new() {
   public T Create() => new T();
   public void Save(T entity) {
       // Persistence logic
   }
}
// Generic method with multiple constraints
public T Max<T>(T a, T b) where T : IComparable<T>, new() => a.CompareTo(b) > 0 ? a : b;
```

## 6. LINQ (Language Integrated Query)

**Definition**: Declarative data querying syntax  
**Key Components**:

- **Deferred Execution**: Queries execute when iterated
- **Standard Query Operators**: `Where`, `Select`, `GroupBy`, `Join`
- **Two Syntax Forms**:
- Query Expression: `from...where...select`
- Method Chaining: `.Where().Select()`

```csharp
var employees = new List<Employee>();
var query = from e in employees
           where e.Department == "Engineering"
           orderby e.LastName
           select new { e.FirstName, e.LastName };
// Method syntax equivalent
var methodQuery = employees
   .Where(e => e.Department == "Engineering")
   .OrderBy(e => e.LastName)
   .Select(e => new { e.FirstName, e.LastName });
```

## 7. Lambda Expressions

**Definition**: Anonymous inline functions  
**Forms**:

- **Expression Lambda**: `(params) => expression`
- **Statement Lambda**: `(params) => { ... }`

```csharp
// Expression lambda
Func<int, int> square = x => x * x;
// Statement lambda
Action<List<int>> processNumbers = numbers => {
   var sum = numbers.Sum();
   Console.WriteLine($"Sum: {sum}");
};
// LINQ usage
var evenNumbers = numbers.Where(n => n % 2 == 0);
```

## 8. Nullable Reference Types

**Definition**: Compiler-enforced null safety (C# 8+)  
**Key Features**:

- Reference types non-nullable by default
- `?` suffix indicates nullable
- `!` null-forgiving operator

```csharp
#nullable enable
string nonNullable = "Hello";  // Can't be null
string? nullable = null;       // Explicit nullable
// Null check patterns
if (nullable is { Length: >0 }) {
   Console.WriteLine(nullable.ToUpper());
}
// Suppress warning
string s = nullable!;  // Use cautiously
```

## 9. Special Operators

| **Operator** | **Name**                     | **Usage**                                 |
|--------------|------------------------------|-------------------------------------------|
| `?.`         | Null-conditional             | `customer?.Address?.City`                 |
| `??`         | Null-coalescing              | `name ?? "Anonymous"`                     |
| `??=`        | Null-coalescing assignment   | `list ??= new List<string>();`            |
| `=>`         | Lambda operator              | `x => x * x`                              |
| `nameof`     | Compile-time name            | `nameof(Customer.Name)` → "Name"          |
| `..`         | Range (C# 8+)                | `array[1..4]`                             |

## 10. Implicit Typing (var)

**Definition**: Type inference by compiler  
**Best Practices**:

- Use when type is obvious from right side
- Avoid with primitive types (`int i = 5` vs `var i = 5`)
- Required for anonymous types

```csharp
var dict = new Dictionary<string, List<int>>();  // Clear type
var anonymous = new { Name = "Alice", Age = 30 }; // Anonymous type requires var
```

## 11. Casting & Type Conversion

**Definitions**:

- **Upcasting**: Convert to base type (implicit)
- **Downcasting**: Convert to derived type (explicit)
- **Safe Casting**: `as` operator (returns null if fails)

```csharp
// Upcasting (always safe)
Shape shape = new Circle();
// Downcasting (potential InvalidCastException)
Circle c = (Circle)shape;
// Safe casting
Circle? c = shape as Circle;
if (c != null) { /*...*/ }
// Pattern matching
if (shape is Circle circle) { /* Use circle */ }
```

## 12. Enums

**Definition**: Named constant value type  
**Features**:

- Underlying type (default int)
- Flags attribute for bitwise combinations
- Explicit values assignment

```csharp
[Flags]
public enum FilePermissions {
   None = 0,
   Read = 1,
   Write = 2,
   Execute = 4,
   All = Read | Write | Execute
}
// Usage
var permissions = FilePermissions.Read | FilePermissions.Write;
if (permissions.HasFlag(FilePermissions.Write)) { /*...*/ }
// Conversions
int value = (int)permissions;
FilePermissions fromInt = (FilePermissions)3;  // Read + Write
```

# Key Terminology Cheat Sheet

| **Term**                 | **Definition**                                                                 |
|--------------------------|-------------------------------------------------------------------------------|
| **Boxing/Unboxing**      | Converting value ↔ reference types (performance impact)                      |
| **Covariance**           | Preserving inheritance in generics (`IEnumerable<Dog>` → `IEnumerable<Animal>`) |
| **Anonymous Type**       | Compiler-generated immutable type (`new { X=1, Y=2 }`)                       |
| **Expression Trees**     | Code-as-data for LINQ providers                                              |
| **Extension Methods**    | Add methods to existing types without inheritance                            |
| **Pattern Combinators**  | `and`, `or`, `not` in C# 9+ pattern matching                                  |

```csharp
// Advanced Pattern Matching (C# 9+)
public bool IsValidSequence(List<int> values) => values is [1, 2, .., 5];
// Matches lists starting with 1, 2 and ending with 5
```
