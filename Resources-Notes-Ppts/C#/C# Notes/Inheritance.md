# C# Inheritance & Polymorphism 

## 1. Inheritance Fundamentals

**Base → Derived Class Relationship**:

```csharp
public class Animal  // Base class
{
   public void Eat() => Console.WriteLine("Eating");
}
public class Dog : Animal  // Derived class
{
   public void Bark() => Console.WriteLine("Woof!");
}
// Usage
Dog spot = new Dog();
spot.Eat();  // Inherited method
spot.Bark(); // Class-specific
```

## 2. Method Overriding

**Virtual/Override Pattern**:

```csharp
public class Shape
{
   public virtual void Draw() => Console.WriteLine("Drawing shape");
}
public class Circle : Shape
{
   public override void Draw() => Console.WriteLine("Drawing circle");
}
// Polymorphic usage
Shape shape = new Circle();
shape.Draw();  // Output: "Drawing circle"
```

## 3. Inheritance & Constructors

**Constructor Chaining**:

```csharp
public class Vehicle
{
   private readonly string _vin;
   public Vehicle(string vin) => _vin = vin;
}
public class Car : Vehicle
{
   public Car(string vin, int doors) : base(vin)
   {
       DoorCount = doors;
   }
   public int DoorCount { get; }
}
```

**Execution Order**:

1. Base class constructor
2. Derived class constructor

## 4. Common Modifiers

| Modifier      | Usage Context       | Effect                                 |
|---------------|---------------------|----------------------------------------|
| `virtual`     | Base class method   | Allows overriding in derived classes   |
| `override`    | Derived class method| Replaces base implementation          |
| `abstract`    | Base class method   | Must be overridden in derived classes  |
| `sealed`      | Overridden method   | Prevents further overriding            |
| `new`         | Derived member      | Hides base implementation (shadowing) |

## 5. Inheritance Considerations

**Key Principles**:

1. **Liskov Substitution**: Derived classes must be substitutable for base
2. **Single Inheritance**: C# only allows single class inheritance
3. **Composition over Inheritance**: Favor object composition for flexibility
**Common Pitfalls**:

- Fragile base class problem
- Overly deep inheritance hierarchies
- Accidental method hiding

```csharp
// Problematic hiding example
public class Base
{
   public void Log() => Console.WriteLine("Base");
}
public class Derived : Base
{
   public new void Log() => Console.WriteLine("Derived");  // Warning without 'new'
}
Base obj = new Derived();
obj.Log();  // Output: "Base" (not polymorphic)
```

## 6. Inheritance & Class Types

### Abstract Classes

```csharp
public abstract class DatabaseConnection
{
   public abstract void Connect();  // Must be implemented
   public virtual void Close() => Console.WriteLine("Closing connection");
}
```

### Sealed Classes

```csharp
public sealed class FinalClass { }  // Cannot be inherited
// Error: public class Derived : FinalClass { }
```

### Static Classes

```csharp
public static class MathUtilities  // Cannot be inherited/instantiated
{
   public static double Pi => 3.14159;
}
```

## Inheritance vs. Composition

| Aspect          | Inheritance                     | Composition                   |
|-----------------|---------------------------------|-------------------------------|
| Relationship    | "Is-a"                         | "Has-a"                       |
| Flexibility     | Compile-time                   | Runtime                       |
| Code Reuse      | White-box                      | Black-box                     |
| Coupling        | Tight                          | Loose                         |

```csharp
// Composition example
public class Car
{
   private readonly Engine _engine;  // Composed object
   public Car(Engine engine) => _engine = engine;
}
```

# Key Inheritance Terminology

| Term                          | Definition                                                                 |
|-------------------------------|---------------------------------------------------------------------------|
| **Polymorphism**              | Treating objects as instances of base type while using derived behavior    |
| **Covariance**                | Using derived return types in overridden methods                          |
| **Shadowing**                 | Hiding base member with `new` keyword                                     |
| **Base Keyword**              | Access base class members from derived class                              |
| **Dynamic Binding**           | Runtime determination of method implementation                           |
| **Abstract Method**           | Method without implementation (must be overridden)                        |

```csharp
// Abstract class implementation
public class SqlConnection : DatabaseConnection
{
   public override void Connect() => Console.WriteLine("SQL connected");
   public override void Close()  // Optional override
   {
       base.Close();
       Console.WriteLine("SQL resources released");
   }
}
