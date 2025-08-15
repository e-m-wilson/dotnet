# Exception Handling

## 1. Errors vs Exceptions

| **Errors**                          | **Exceptions**                       |
|-------------------------------------|--------------------------------------|
| Broad category of issues            | Specific runtime error objects       |
| Includes compile-time errors        | Derived from `System.Exception`      |
| Example: Syntax mistakes            | Example: FileNotFoundException      |
| Prevent code compilation            | Disrupt normal execution flow        |

## 2. Exception Handling Fundamentals

### Basic Structure

```csharp
try
{
   // Code that might throw exceptions
   File.ReadAllText("missing.txt");
}
catch (FileNotFoundException ex)
{
   Console.WriteLine($"File not found: {ex.FileName}");
}
catch (Exception ex)
{
   Console.WriteLine($"General error: {ex.Message}");
}
finally
{
   Console.WriteLine("Cleanup code always executes");
}
```

## 3. Key Components

### try Block

- Contains guarded code
- Must be followed by at least one `catch` or `finally`

### catch Block

- Handles specific exception types
- Order matters (most specific → most general)

```csharp
catch (InvalidOperationException ex) { /* Specific handler */ }
catch (Exception ex) { /* General handler */ }
```

### finally Block

- Always executes (even after `return` or uncaught exceptions)
- Used for resource cleanup

```csharp
FileStream file = null;
try
{
   file = File.Open("data.txt", FileMode.Open);
}
finally
{
   file?.Dispose();  // Alternative: using statement
}
```

## 4. Throwing Exceptions

### Basic Throwing

```csharp
public decimal CalculateInterest(decimal principal)
{
   if (principal <= 0)
       throw new ArgumentException("Principal must be positive", nameof(principal));
   // Calculation logic
}
```

### Custom Exceptions

```csharp
public class InsufficientFundsException : Exception
{
   public decimal CurrentBalance { get; }
   public decimal RequiredAmount { get; }
   public InsufficientFundsException(decimal balance, decimal amount)
       : base($"Required: {amount}, Available: {balance}")
   {
       CurrentBalance = balance;
       RequiredAmount = amount;
   }
}
// Usage
throw new InsufficientFundsException(100, 500);
```

## 5. Exception Bubbling

**Propagation through call stack**:

```csharp
void MethodA() => MethodB();
void MethodB() => MethodC();
void MethodC() => throw new InvalidOperationException();
try
{
   MethodA();
}
catch (InvalidOperationException ex)
{
   // Catches exception from MethodC
}
```

## 6. Compile-Time vs Runtime Exceptions

| **Compile-Time Errors**         | **Runtime Exceptions**             |
|---------------------------------|------------------------------------|
| Syntax errors                   | NullReferenceException            |
| Type mismatches                 | IndexOutOfRangeException           |
| Missing members                 | DivideByZeroException             |
| Prevent successful compilation  | Occur during program execution     |

```csharp
// Compile-time error example
int x = "hello";  // Cannot convert string to int
// Runtime exception example
int[] arr = new int[5];
int value = arr[10];  // IndexOutOfRangeException
```

## 7. Best Practices

1. **Specific Catch Blocks First**
2. **Avoid Empty Catch Blocks**
3. **Use using for IDisposable**
4. **Log Exceptions Appropriately**
5. **Throw Meaningful Exceptions**
6. **Avoid Exception Control Flow**

```csharp
// Good practice example
try
{
   using var file = File.OpenRead("data.csv");
   ProcessFile(file);
}
catch (FileNotFoundException ex)
{
   Logger.LogWarning(ex, "Data file missing");
   CreateDefaultFile();
}
catch (IOException ex)
{
   Logger.LogError(ex, "File I/O error");
   throw;  // Re-throw for higher level handling
}
```

## Common Exception Types

| Exception Type               | Common Cause                      |
|------------------------------|-----------------------------------|
| `NullReferenceException`     | Accessing null object members     |
| `ArgumentException`          | Invalid method parameters         |
| `InvalidOperationException`  | Object state invalid for operation|
| `FormatException`            | Invalid string format conversion  |
| `NotImplementedException`    | Missing method implementation    |
| `TimeoutException`           | Operation timed out               |

## Key Terminology

| Term                      | Definition                                                                 |
|---------------------------|---------------------------------------------------------------------------|
| **Stack Trace**           | Call hierarchy showing exception origin                                  |
| **Re-throwing**           | `throw;` preserves original stack trace                                  |
| **Exception Filter**      | C# 6+ `when` clause for conditional catching                             |
| **AggregateException**    | Contains multiple exceptions (common in async)                           |
| **Faulted State**         | Object becomes unusable after exception                                  |
| **Exception Shielding**   | Protecting system boundaries from internal exceptions                    |

```csharp
// Exception filtering (C# 6+)
try
{
   ConnectToDatabase();
}
catch (SqlException ex) when (ex.Number == 18456)
{
   HandleInvalidLogin();
}
catch (SqlException ex) when (ex.Number == 1205)
{
   HandleDeadlock();
}
```
