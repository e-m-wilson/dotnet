# Basics


## 1. Built-in Data Types

### Value Types

**Stored on the stack** - Directly contain their data.
Full list of basic value types:

| Keyword  | Size     | Range/Description                  | Example             |
|----------|----------|------------------------------------|---------------------|
| `byte`   | 8 bits   | 0 to 255                           | `byte b = 100;`     |
| `sbyte`  | 8 bits   | -128 to 127                        | `sbyte sb = -50;`   |
| `short`  | 16 bits  | -32,768 to 32,767                  | `short s = 30000;`  |
| `ushort` | 16 bits  | 0 to 65,535                        | `ushort us = 60000;`|
| `int`    | 32 bits  | -2^31 to 2^31-1                    | `int i = 2147483647;` |
| `uint`   | 32 bits  | 0 to 4,294,967,295                 | `uint ui = 4000000;`|
| `long`   | 64 bits  | -2^63 to 2^63-1                    | `long l = 9e18;`    |
| `ulong`  | 64 bits  | 0 to 18,446,744,073,709,551,615    | `ulong ul = 18e18;` |
| `float`  | 32 bits  | ~6-9 digits of precision           | `float f = 3.14f;`  |
| `double` | 64 bits  | ~15-17 digits of precision         | `double d = 3.14159;` |
| `decimal`| 128 bits | 28-29 significant digits           | `decimal m = 3.14159265358979323846m;` |
| `char`   | 16 bits  | Unicode character                  | `char c = 'A';`     |
| `bool`   | 1 bit*   | `true` or `false`                  | `bool flag = true;` |
| `enum`   | Varies   | Named constant values              | `enum Days { Sun, Mon }` |
| `struct` | Varies   | User-defined value type            | `Point { int x, y; }` |
*\* Actual storage size depends on implementation*

### Reference Types

**Stored on the heap** - Contain references to memory locations. Basic reference types:

| Keyword     | Description                          | Example                          |
|-------------|--------------------------------------|----------------------------------|
| `string`    | Immutable Unicode character sequence | `string s = "Hello";`            |
| `object`    | Base type for all types              | `object o = 10;`                 |
| `class`     | User-defined reference type          | `class Animal { }`               |
| `interface` | Contract for classes                 | `interface ILogger { void Log(); }` |
| `delegate`  | Function pointer type                | `delegate void Callback();`      |
| `dynamic`   | Bypasses compile-time type checking  | `dynamic d = "test"; d = 10;`    |
| `Array`     | Collection of values                 | `int[] arr = new int[5];`        |

### Value vs Reference Types

| Characteristic          | Value Types                          | Reference Types                  |
|-------------------------|--------------------------------------|----------------------------------|
| **Storage**             | Stack                                | Heap (with stack reference)      |
| **Assignment**          | Copy entire value                    | Copy reference (memory address)  |
| **Nullability**         | Cannot be null (except nullable)     | Can be null                      |
| **Default Value**       | Zero/null for struct                 | `null`                           |
| **Memory Management**   | Destroyed when out of scope          | Garbage-collected                |
| **Example**             | `int`, `char`, `struct`              | `string`, `class`, `Array`       |

```csharp
// Value Type Example
int a = 10;
int b = a;  // Copy of value (b = 10)
a = 20;     // b remains 10
// Reference Type Example
int[] arr1 = { 1, 2 };
int[] arr2 = arr1;  // Copy reference (same memory)
arr1[0] = 99;       // arr2[0] also becomes 99
```

---

## 2. Expanded Concepts

### Managed Execution Process (Detailed)

1. **Compilation**:

- C# → Intermediate Language (IL) via `csc` compiler
- IL stored in .dll/.exe assemblies

2. **JIT Compilation**:

- CLR compiles IL → Native code **per method**
- Cached for subsequent calls

3. **Execution**:

- CLR manages:
  - Memory (Garbage Collection)
  - Exception handling
  - Security (Code Access Security)
  - Thread synchronization

### Console I/O (Advanced)

```csharp
// Reading Numbers
Console.Write("Enter age: ");
int age = int.Parse(Console.ReadLine());
// Formatting Output
Console.WriteLine("{0} is {1} years old", name, age);  // Composite formatting
Console.WriteLine($"{name} is {age:D3} years old");    // Interpolation + formatting
// Error Handling
if (int.TryParse(input, out int result)) {
   // Valid number
} else {
   Console.WriteLine("Invalid input!");
}
```

### Type Conversions (Deep Dive)

**Implicit** (Safe):

```csharp
int i = 100;
long l = i;  // No data loss
```

**Explicit** (Cast):

```csharp
double d = 3.14;
int i = (int)d;  // i = 3 (truncates)
```

**Conversion Methods**:

```csharp
// Parse (throws exception on failure)
int num = int.Parse("123");
// TryParse (safe)
bool success = int.TryParse("abc", out int result);
// Convert class
decimal m = Convert.ToDecimal("3.14");
// Boxing/Unboxing
object boxed = 42;           // Value → Reference
int unboxed = (int)boxed;    // Reference → Value
```

### Conditional Operators (Extended)

```csharp
// Null-conditional (?.)
string? name = null;
int? length = name?.Length;  // null if name is null
// Null-coalescing assignment (??=)
List<int>? numbers = null;
numbers ??= new List<int>();  // Assign if null
// Switch expression (C# 8+)
string grade = score switch {
>= 90 => "A",
>= 80 => "B",
   _ => "F"
};
```

---

## Key Terminology Cheat Sheet

| Term                 | Definition                                                                 |
|----------------------|---------------------------------------------------------------------------|
| **Stack**            | Memory region for value types (fast, scope-bound allocation)             |
| **Heap**             | Memory region for reference types (dynamic allocation, garbage-collected)|
| **Boxing**           | Converting value type → reference type (`object`)                         |
| **Unboxing**         | Converting boxed value → original value type                              |
| **Nullable**         | Value type wrapper allowing `null` (e.g., `int?`)                         |
| **Garbage Collector**| CLR component managing heap memory                                       |
| **JIT**              | Just-In-Time compiler (IL → native code during execution)                 |
