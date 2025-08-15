# Collections

## 1. Collections Overview

**System.Collections.Generic** namespace contains type-safe, performant collections

| Collection Type    | Best Use Case                        | Key Characteristics          |
|--------------------|--------------------------------------|------------------------------|
| `List<T>`          | Dynamic arrays                       | Fast access by index         |
| `Dictionary<K,V>`  | Key-value lookups                    | O(1) average lookup          |
| `HashSet<T>`       | Unique element storage               | Set operations               |
| `Queue<T>`         | FIFO processing                      | Enqueue/Dequeue operations   |
| `Stack<T>`         | LIFO processing                      | Push/Pop operations          |
| `LinkedList<T>`    | Frequent insertions/removals         | Bidirectional access         |

## 2. List<T> and Generic Classes

**Dynamic array** with automatic resizing:

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };
numbers.Add(4);                     // O(1) amortized
numbers.Insert(0, 0);               // O(n)
int third = numbers[2];             // O(1)
numbers.RemoveAt(1);                // O(n)
```

## 3. foreach and IEnumerable

**Iteration pattern** for collections:

```csharp
List<string> colors = new List<string> { "Red", "Blue" };

// foreach implementation
foreach (string color in colors)
{
   Console.WriteLine(color);
}

// Manual IEnumerable usage
IEnumerator<string> enumerator = colors.GetEnumerator();
while (enumerator.MoveNext())
{
   Console.WriteLine(enumerator.Current);
}
```

## 4. Dictionary<K,V>

**Hash table implementation** for key-value pairs:

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>
{
   ["Alice"] = 30,
   ["Bob"] = 25
};

// Operations
ages.Add("Charlie", 28);           // O(1) average
if (ages.TryGetValue("Alice", out int age))  // O(1)
{
   Console.WriteLine(age);
}
```

## 5. Specialized Collections

### LinkedList<T>

```csharp
LinkedList<string> chain = new LinkedList<string>();
var node = chain.AddFirst("First");    // O(1)
chain.AddAfter(node, "Second");        // O(1)
chain.Remove(node);                    // O(1)
```

### Queue<T>

```csharp
Queue<int> orders = new Queue<int>();
orders.Enqueue(1001);                // O(1)
int nextOrder = orders.Dequeue();    // O(1)
```

### Stack<T>

```csharp
Stack<string> history = new Stack<string>();
history.Push("page1");              // O(1)
string last = history.Pop();         // O(1)
```

## 6. Asymptotic Notation (Big O)

### Common Complexities

| Notation    | Name               | Example Operation                 |
|-------------|--------------------|------------------------------------|
| O(1)        | Constant Time      | Array access, Dictionary lookup    |
| O(log n)    | Logarithmic Time   | Binary search                      |
| O(n)        | Linear Time        | Linear search, List iteration      |
| O(n log n)  | Linearithmic Time  | Efficient sorting algorithms       |
| O(n²)       | Quadratic Time     | Nested loops                       |

### Collection Complexities

| Collection       | Add          | Remove       | Access       | Search       |
|------------------|--------------|--------------|--------------|--------------|
| `List<T>`        | O(1)*        | O(n)         | O(1)         | O(n)         |
| `Dictionary<K,V>`| O(1)         | O(1)         | O(1)         | O(1)         |
| `HashSet<T>`     | O(1)         | O(1)         | N/A          | O(1)         |
| `LinkedList<T>`  | O(1)         | O(1)         | O(n)         | O(n)         |
| `Queue<T>`       | O(1)         | O(1)         | N/A          | N/A          |
| `Stack<T>`       | O(1)         | O(1)         | N/A          | N/A          |

*Amortized constant time for List.Add

## 7. HashSet<T>

**Unique element storage** with set operations:

```csharp
HashSet<int> setA = new HashSet<int> { 1, 2, 3 };
HashSet<int> setB = new HashSet<int> { 2, 3, 4 };

// Set operations
setA.UnionWith(setB);          // {1,2,3,4}     O(n)
setA.IntersectWith(setB);      // {2,3}         O(n)
bool has = setA.Contains(2);   // True          O(1)
```

## Key Terminology

| Term                  | Definition                                                                 |
|-----------------------|---------------------------------------------------------------------------|
| **Amortized Cost**    | Average cost over multiple operations                                     |
| **Enumerators**       | Objects implementing collection traversal                                 |

```csharp
// Custom IEnumerable implementation
public class Range : IEnumerable<int>
{
   private readonly int _start, _end;
   
   public Range(int start, int end) => (_start, _end) = (start, end);
   
   public IEnumerator<int> GetEnumerator()
   {
       for (int i = _start; i <= _end; i++)
           yield return i;
   }
   
   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```
