
# Advanced JavaScript

## 1. OOP in JavaScript

**Prototype-Based Inheritance**  
All objects inherit properties/methods from a prototype.  

```javascript
// Constructor Function (Pre-ES6)
function Person(name) {
 this.name = name;
}
Person.prototype.greet = function() {
 console.log(`Hello, I'm ${this.name}`);
};
const alice = new Person("Alice");
alice.greet(); // "Hello, I'm Alice"
```

**Key Concepts**:

- Every object has a `[[Prototype]]` property (accessed via `__proto__`)
- `Object.create()` creates new objects with specified prototype
- Prototype chain allows property/method lookup up to `Object.prototype`

---

## 2. The `this` Keyword

**Context-Dependent Value**  

| Context                | `this` Value                | Example                     |
|------------------------|-----------------------------|-----------------------------|
| Global Scope           | `window` (browser)          | `console.log(this)`         |
| Function Call          | `undefined` (strict mode)   | `function test() { console.log(this) }` |
| Method Call            | Owning object               | `obj.method()`              |
| Constructor            | New instance                | `new MyClass()`             |
| Arrow Function         | Lexical parent's `this`     | `() => { console.log(this) }` |
**Common Pitfall**:

```javascript
const obj = {
 name: "Alice",
 greet: function() {
   setTimeout(function() {
     console.log(this.name); // `this` = window (loses context)
   }, 100);
 }
};
// Solution: Arrow function or .bind()
setTimeout(() => console.log(this.name), 100);
```

---

## 3. Classes (ES6+)

**Syntactic Sugar for Prototypes**  

```javascript
class Animal {
 constructor(name) {
   this.name = name;
 }
 // Instance method
 speak() {
   console.log(`${this.name} makes a sound`);
 }
 // Static method
 static info() {
   console.log("Animal class");
 }
}
class Dog extends Animal {
 constructor(name, breed) {
   super(name);
   this.breed = breed;
 }
 speak() {
   super.speak();
   console.log("Woof!");
 }
}
const spot = new Dog("Spot", "Labrador");
spot.speak();
// "Spot makes a sound"
// "Woof!"
```

---

## 4. Hoisting

**Declaration Elevation**  

- Function declarations: Fully hoisted  
- `var`: Declaration hoisted (initialized to `undefined`)  
- `let/const`: Hoisted but not initialized (Temporal Dead Zone)  

```javascript
console.log(x); // undefined (var)
var x = 5;
console.log(y); // ReferenceError (TDZ)
let y = 10;
// Function hoisting
sayHello(); // Works
function sayHello() { console.log("Hi!"); }
```

---

## 5. Error Handling

**Try/Catch/Finally**  

```javascript
try {
 JSON.parse(invalidJson);
} catch (err) {
 console.error("Parsing error:", err.message);
} finally {
 console.log("Cleanup complete");
}
// Custom Errors
class ValidationError extends Error {
 constructor(message) {
   super(message);
   this.name = "ValidationError";
 }
}
throw new ValidationError("Invalid input");
```

---

## 6. Default Parameters

**ES6 Function Defaults**  

```javascript
function createUser(name = "Anonymous", age = 0) {
 return { name, age };
}
// Works with expressions too
function createElement(type, content = getDefaultContent()) {
 /* ... */
}
```

---

## 7. Spread & Rest Operators

### Spread (`...`)  

Expands iterables into individual elements:  

```javascript
// Arrays
const nums = [1, 2, 3];
const combined = [...nums, 4, 5]; // [1,2,3,4,5]
// Objects
const defaults = { color: "red", size: "M" };
const shirt = { ...defaults, size: "L" };
// Function Arguments
Math.max(...nums); // 3
```

### Rest (`...`)  

Collects elements into an array:  

```javascript
function sum(...numbers) {
 return numbers.reduce((acc, num) => acc + num, 0);
}
sum(1, 2, 3); // 6
// Object Rest
const { id, ...rest } = { id: 1, name: "A", age: 30 };
```

---

# Key Differences Table

| Feature               | `var`                  | `let/const`            |
|-----------------------|------------------------|------------------------|
| Scope                 | Function               | Block                  |
| Hoisting              | Initialized as `undefined` | Not initialized (TDZ) |
| Re-declaration        | Allowed                | Not allowed            |
| Global Object Property| Yes                    | No (window in browser) |
| Operator              | Spread Use             | Rest Use               |

| Syntax                | `[...arr]`             | `function(...args)`    |
|-----------------------|------------------------|------------------------|
| Context               | Array/Object expansion | Parameter collection   |

|Error Type             | Common Cause             |
|-----------------------|------------------------|
| `ReferenceError`      | Undefined variable     |
| `TypeError`           | Invalid type operation |
| `SyntaxError`         | Code parsing failure   |
| `RangeError`          | Invalid numeric range  |

```javascript
// Modern JS Best Practices
class MyComponent {          // Use classes for complex logic
 #privateField;             // Private field (ES2022)
 constructor() {
   this.state = {};
 }
 static version = "1.0.0";  // Static property
}
const config = {             // Object shorthand
 apiUrl: process.env.API_URL ?? 'default',  // Nullish coalescing
 ...(condition && { debug: true })          // Conditional spread
};
```
