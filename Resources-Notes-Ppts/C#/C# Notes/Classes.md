# Classes and OOP

## 1. OOP Fundamentals

**Four Pillars**:

1. **Encapsulation**: Bundling data/methods into objects
2. **Inheritance**: Creating hierarchies via base classes
3. **Polymorphism**: Different implementations via interfaces
4. **Abstraction**: Hiding complex implementation details

```csharp
// Basic Class
public class Vehicle
{
   // Field
   private string _vin;
   
   // Constructor
   public Vehicle(string vin) => _vin = vin;
   
   // Method
   public void StartEngine() => Console.WriteLine("Engine started");
}
```

## 2. Objects vs Classes

| **Class**                  | **Object**                  |
|----------------------------|-----------------------------|
| Blueprint/template         | Concrete instance           |
| Defined at compile-time    | Created at runtime          |
| Memory not allocated       | Memory allocated on heap    |

```csharp
// Instantiation
Vehicle car = new Vehicle("1HGBH41JXMN109186");
```

## 3. Fields & Methods

### Fields

```csharp
private int _count;          // Instance field
private static int _total;   // Static field
```

### Instance vs Static Methods

```csharp
public class Calculator 
{
   // Instance method
   public int Add(int a, int b) => a + b;
   
   // Static method
   public static double Pi => 3.14159;
}

// Usage
var calc = new Calculator();
calc.Add(2, 3);            // Instance call
double pi = Calculator.Pi; // Static access
```

## 4. Access Modifiers

| Modifier            | Accessibility                              |
|---------------------|--------------------------------------------|
| `public`            | No restrictions                            |
| `private`           | Containing class only                      |
| `protected`         | Containing class + derived classes         |
| `internal`          | Current assembly                           |
| `protected internal`| Union of protected + internal              |
| `private protected` | Containing class + derived in same assembly|

## 5. Properties

**Controlled field access**:

```csharp
private string _name;

// Traditional property
public string Name 
{
   get => _name;
   set => _name = value ?? throw new ArgumentNullException();
}

// Auto-property
public decimal Price { get; private set; }

// Expression-bodied (C# 7+)
public DateTime Created => DateTime.UtcNow;
```

## 6. Object Initializers

**Set properties during creation**:

```csharp
var product = new Product 
{
   Name = "Widget",
   Price = 9.99m,
   Created = DateTime.UtcNow
};
```

## 7. Constructors

```csharp
public class Person
{
   // Field
   private readonly string _id;
   
   // Default constructor
   public Person() => _id = Guid.NewGuid().ToString();
   
   // Parameterized constructor
   public Person(string id) => _id = id;
   
   // Static constructor
   static Person() => Console.WriteLine("Type initialized");
}
```

## 8. Encapsulation

**Data protection patterns**:

```csharp
// Read-only access
public class BankAccount
{
   private decimal _balance;
   
   public decimal Balance => _balance;  // Read-only
   
   public void Deposit(decimal amount) 
   {
       if(amount > 0) _balance += amount;
   }
}
```

## 9. Namespaces

**Logical grouping**:

```csharp
namespace Company.Project.Shapes
{
   public class Circle { /*...*/ }
}

// Using directive
using Company.Project.Shapes;

var circle = new Circle();
```

## 10. Abstract Classes

**Incomplete base classes**:

```csharp
public abstract class Shape
{
   public abstract double Area();  // Must be implemented
   
   public virtual void Draw() => Console.WriteLine("Drawing shape");
}

public class Circle : Shape
{
   public override double Area() => Math.PI * radius * radius;
}
```

## 11. Interfaces

**Contract definition** (C# 8+ with default implementations):

```csharp
public interface ILogger
{
   void Log(string message);
   
   // Default implementation
   void LogError(string error) => Log($"ERROR: {error}");
}

public class FileLogger : ILogger
{
   public void Log(string message) => File.WriteAllText("log.txt", message);
}
```

## 12. Internal Access

**Assembly-level visibility**:

```csharp
// Assembly1
internal class InternalUtility
{
   public static void Help() => Console.WriteLine("Internal help");
}

// Assembly2 (cannot access InternalUtility)
```

**Friend Assemblies** (AssemblyInfo.cs):

```csharp
[assembly: InternalsVisibleTo("TrustedAssembly")]
```

# Key OOP Terminology

| Term                   | Definition                                                                 |
|------------------------|---------------------------------------------------------------------------|
| **Polymorphism**       | Treating objects of different types through common interface               |
| **Coupling**           | Degree of interdependence between classes                                  |
| **Cohesion**           | How focused a class is on single responsibility                           |
| **Virtual**            | Method that can be overridden in derived classes                           |
| **Sealed**             | Prevents inheritance/method overriding                                     |
| **Partial Class**      | Class definition split across multiple files                               |
