# Unit Testing & Moq

## 1. Unit Testing Fundamentals

**Definition**: Testing individual code units in isolation  
**Goals**:

- Verify component behavior
- Enable refactoring
- Document system behavior
- Catch regressions

```csharp
// Simple Unit Test Example
public class CalculatorTests
{
   [Fact]
   public void Add_TwoNumbers_ReturnsSum()
   {
       // Arrange
       var calc = new Calculator();
       // Act
       var result = calc.Add(2, 3);
       // Assert
       Assert.Equal(5, result);
   }
}
```

## 2. Arrange-Act-Assert Pattern

| Phase       | Purpose                                 | Example                      |
|-------------|-----------------------------------------|------------------------------|
| **Arrange** | Setup test prerequisites                | Create objects, mock deps    |
| **Act**     | Execute unit under test                 | Call method being tested     |
| **Assert**  | Verify outcomes                         | Check return values/state    |

## 3. Test-Driven Development (TDD)

**Red-Green-Refactor Cycle**:

1. **Red**: Write failing test
2. **Green**: Write minimal code to pass
3. **Refactor**: Improve code while keeping tests passing

```csharp
// TDD Example (String Reverser)
// 1. Write failing test
[Fact]
public void Reverse_InputString_ReturnsReversed()
{
   var reverser = new StringReverser();
   string result = reverser.Reverse("hello");
   Assert.Equal("olleh", result);  // Fails initially
}
// 2. Implement minimal solution
public class StringReverser
{
   public string Reverse(string input) => new string(input.Reverse().ToArray());
}
// 3. Refactor (e.g., optimize algorithm)
```

## 4. xUnit Framework

### Fact Tests

```csharp
public class MathTests
{
   [Fact]
   public void Square_Number_ReturnsSquaredValue()
   {
       var math = new MathOperations();
       double result = math.Square(4);
       Assert.Equal(16, result);
   }
}
```

### Theory Tests with Inline Data

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(-1, 5, 4)]
[InlineData(0, 0, 0)]
public void Add_MultipleCases_ReturnsCorrectSum(int a, int b, int expected)
{
   var calc = new Calculator();
   int result = calc.Add(a, b);
   Assert.Equal(expected, result);
}
```

## 5. Moq Framework (Mocking)

### Basic Mocking

```csharp
public interface ILogger
{
   void Log(string message);
}
[Fact]
public void Process_ValidInput_LogsMessage()
{
   // Arrange
   var mockLogger = new Mock<ILogger>();
   var processor = new DataProcessor(mockLogger.Object);
   // Act
   processor.Process("test");
   // Assert
   mockLogger.Verify(l => l.Log("Processing: test"), Times.Once);
}
```

### Mock Return Values

```csharp
public interface IInventoryService
{
   int GetStock(string itemId);
}
[Fact]
public void OrderItem_ValidItem_ReducesStock()
{
   // Arrange
   var mockInventory = new Mock<IInventoryService>();
   mockInventory.Setup(i => i.GetStock("A123")).Returns(10);
   var orderSystem = new OrderSystem(mockInventory.Object);
   // Act
   orderSystem.PlaceOrder("A123", 3);
   // Assert
   mockInventory.Verify(i => i.UpdateStock("A123", 7), Times.Once);
}
```

## 6. Mocking Strategies

| Technique            | Purpose                                   | Moq Example                          |
|----------------------|-------------------------------------------|--------------------------------------|
| **Setup**            | Configure mock behavior                   | `mock.Setup(m => m.Method()).Returns(value)` |
| **Verify**           | Check method interactions                | `mock.Verify(m => m.Method(), Times.Once)` |
| **Callback**         | Execute code when mock is called         | `mock.Setup(...).Callback(() => {...})` |
| **Sequence**         | Verify call order                        | `mock.SetupSequence(...)`           |

## 7. Best Practices

1. **Test Naming**: Follow "MethodName_Scenario_Expected" pattern
2. **Single Responsibility**: Test one behavior per test
3. **Avoid Over-Mocking**: Only mock external dependencies
4. **Test Behavior, Not Implementation**: Focus on outcomes not internals
5. **Use Meaningful Data**: Avoid magic numbers in tests

```csharp
// Anti-Pattern: Overly Complex Mock
[Fact]
public void BadTest_Overmocking()
{
   var mockService = new Mock<IService>();
   mockService.Setup(s => s.GetA()).Returns(1);
   mockService.Setup(s => s.GetB()).Returns(2);
   mockService.Setup(s => s.Process(It.IsAny<int>(), It.IsAny<int>()));
   var sut = new SystemUnderTest(mockService.Object);
   sut.Execute();
   mockService.Verify(s => s.Process(1, 2), Times.Once);  // Brittle test
}
```

## Key Terminology

| Term                      | Definition                                                                 |
|---------------------------|---------------------------------------------------------------------------|
| **SUT**                   | System Under Test (the component being tested)                           |
| **Test Double**           | Generic term for test substitutes (mocks, stubs, fakes)                  |
| **Mock**                  | Object recording interactions for verification                           |
| **Stub**                  | Provides predefined answers to method calls                              |
| **Fake**                  | Simplified functional implementation for testing                          |
| **State Verification**    | Checking system state after execution                                    |
| **Behavior Verification** | Verifying interactions with dependencies                                 |

## xUnit vs Moq Cheat Sheet

| xUnit Attribute | Purpose                          | Moq Method          | Purpose                          |
|-----------------|----------------------------------|---------------------|----------------------------------|
| `[Fact]`        | Single test case                | `Setup()`           | Configure mock behavior          |
| `[Theory]`      | Parameterized test              | `Verify()`          | Check mock interactions          |
| `[InlineData]`  | Provide test parameters         | `Returns()`         | Set mock return value            |
| `[MemberData]`  | External data source            | `It.IsAny<T>()`     | Argument matching                |
| `[Trait]`       | Categorize tests                | `Callback()`        | Execute code during mock calls   |

```csharp
// Advanced Mocking Example
[Fact]
public void PaymentProcessor_RetriesOnFailure()
{
   var mockGateway = new Mock<IPaymentGateway>();
   var calls = 0;
   mockGateway.Setup(g => g.Process(It.IsAny<decimal>()))
       .Callback(() => calls++)
       .Throws<TimeoutException>()
       .Throws<TimeoutException>()
       .Returns(true);
   var processor = new PaymentProcessor(mockGateway.Object, retries: 3);
   var result = processor.ProcessPayment(100m);
   Assert.True(result);
   Assert.Equal(3, calls);
}
```
