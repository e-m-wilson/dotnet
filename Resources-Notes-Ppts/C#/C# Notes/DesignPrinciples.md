# Design Principles & Patterns 

## 1. SOLID Principles

### Single Responsibility Principle (SRP)

```csharp
// Bad: Mixing report generation and printing
public class ReportManager {
   public void GenerateReport() { /*...*/ }
   public void PrintReport() { /*...*/ }
}
// Good: Separate responsibilities
public class ReportGenerator {
   public void Generate() { /*...*/ }
}
public class ReportPrinter {
   public void Print() { /*...*/ }
}
```

### Open/Closed Principle (OCP)

```csharp
public abstract class Shape {
   public abstract double Area();
}
public class Circle : Shape {
   public double Radius { get; set; }
   public override double Area() => Math.PI * Radius * Radius;
}
// New shape types can be added without modifying existing code
```

### Liskov Substitution Principle (LSP)

```csharp
public class Bird {
   public virtual void Fly() { /*...*/ }
}
public class Penguin : Bird {
   public override void Fly() => throw new NotSupportedException();  // Violates LSP
}
// Better: Segregate interfaces
public interface IFlyingBird { void Fly(); }
public class Sparrow : IFlyingBird { /*...*/ }
public class Penguin : Bird { /*...*/ }
```

### Interface Segregation Principle (ISP)

```csharp
// Bad: Fat interface
public interface IWorker {
   void Work();
   void Eat();
}
// Good: Split interfaces
public interface IWorkable { void Work(); }
public interface IEatable { void Eat(); }
```

### Dependency Inversion Principle (DIP)

```csharp
public interface ILogger {
   void Log(string message);
}
public class FileLogger : ILogger { /*...*/ }
public class Processor {
   private readonly ILogger _logger;
   public Processor(ILogger logger) => _logger = logger;  // High-level depends on abstraction
}
```

## 2. Dependency Injection

**Implementation Patterns**:

```csharp
// Constructor Injection
public class OrderService {
   private readonly IOrderRepository _repo;
   public OrderService(IOrderRepository repo) => _repo = repo;
}
// Property Injection
public class CustomerService {
   public ILogger Logger { get; set; }
}
// Method Injection
public class ReportGenerator {
   public void Generate(IFileExporter exporter) { /*...*/ }
}
```

## 3. Separation of Concerns

**Data Processing Example**:

```csharp
public class DataFetcher {
   public string GetRawData() { /*...*/ }
}
public class DataParser {
   public ParsedData Parse(string rawData) { /*...*/ }
}
public class DataAnalyzer {
   public AnalysisResult Analyze(ParsedData data) { /*...*/ }
}
// Coordinator class
public class DataProcessor {
   public void Process() {
       var raw = new DataFetcher().GetRawData();
       var parsed = new DataParser().Parse(raw);
       new DataAnalyzer().Analyze(parsed);
   }
}
```

## 4. Singleton Pattern

**Thread-Safe Implementation**:

```csharp
public sealed class AppConfig {
   private static readonly Lazy<AppConfig> _instance =
       new Lazy<AppConfig>(() => new AppConfig());
   public static AppConfig Instance => _instance.Value;
   private AppConfig() { /*...*/ }
   // Configuration properties
   public string ConnectionString { get; set; }
}
// Usage:
string connStr = AppConfig.Instance.ConnectionString;
```

## 5. Factory Pattern

**Interface-Based Factory**:

```csharp
public interface IPaymentProcessor {
   void ProcessPayment(decimal amount);
}
public class CreditCardProcessor : IPaymentProcessor { /*...*/ }
public class PayPalProcessor : IPaymentProcessor { /*...*/ }
public static class PaymentFactory {
   public static IPaymentProcessor Create(string type) {
       return type switch {
           "CreditCard" => new CreditCardProcessor(),
           "PayPal" => new PayPalProcessor(),
           _ => throw new ArgumentException()
       };
   }
}
```

## 6. Unit of Work Pattern

**Transaction Management**:

```csharp
public interface IUnitOfWork : IDisposable {
   IRepository<Order> Orders { get; }
   IRepository<Customer> Customers { get; }
   void Commit();
   void Rollback();
}
public class SqlUnitOfWork : IUnitOfWork {
   private readonly IDbTransaction _transaction;
   public SqlUnitOfWork(IDbConnection connection) {
       _transaction = connection.BeginTransaction();
   }
   public void Commit() => _transaction.Commit();
   public void Rollback() => _transaction.Rollback();
   // Repository implementations
   private IRepository<Order> _orders;
   public IRepository<Order> Orders => _orders ??= new OrderRepository(_transaction);
}
```

## 7. Repository Pattern

**Generic Implementation**:

```csharp
public interface IRepository<T> where T : class {
   T GetById(int id);
   void Add(T entity);
   void Update(T entity);
   void Delete(T entity);
   IEnumerable<T> GetAll();
}
public class GenericRepository<T> : IRepository<T> where T : class {
   private readonly IDbConnection _connection;
   public GenericRepository(IDbConnection connection) {
       _connection = connection;
   }
   public T GetById(int id) => _connection.Get<T>(id);
   public void Add(T entity) => _connection.Insert(entity);
   // Other implementations...
}
// Usage:
public class OrderService {
   private readonly IRepository<Order> _repo;
   public OrderService(IRepository<Order> repo) => _repo = repo;
   public Order GetOrder(int id) => _repo.GetById(id);
}
```

# Key Terminology

| Term                        | Definition                                                                 |
|-----------------------------|---------------------------------------------------------------------------|
| **Inversion of Control**    | Delegating object creation to external source                            |
| **Transient vs Singleton**  | Object lifetime management (new instance vs single instance)              |
| **Abstract Factory**        | Factory pattern variant for creating families of related objects         |
| **Persistence Ignorance**   | Domain objects unaware of persistence implementation                     |
| **Lazy Initialization**     | Deferring object creation until first use                                |

```csharp
// Composite Pattern Example (Bonus)
public interface IComponent {
   void Execute();
}
public class Leaf : IComponent {
   public void Execute() => Console.WriteLine("Leaf operation");
}
public class Composite : IComponent {
   private readonly List<IComponent> _children = new List<IComponent>();
   public void Add(IComponent component) => _children.Add(component);
   public void Execute() {
       foreach (var child in _children) {
           child.Execute();
       }
   }
}
```
