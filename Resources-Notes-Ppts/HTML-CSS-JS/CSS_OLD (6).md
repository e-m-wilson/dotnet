
# JavaScript Intro

## 1. JavaScript Introduction & History

**Created**: 1995 by Brendan Eich (10 days!) for Netscape Navigator  
**Evolution**:

- 1997: ECMAScript 1 (ES1) standardization
- 2009: ES5 (JSON, strict mode)
- 2015: ES6/ES2015 (modern features)
- Annual updates since 2015 (ES2016, ES2017, etc.)
**Key Features**:
- Dynamic, prototype-based language
- Single-threaded with event loop
- Runs in browsers & servers (Node.js)
- Weakly typed with dynamic typing

---

## 2. Data Types

### Primitive Types (Immutable)

| Type       | Example              | typeof Result |
|------------|----------------------|---------------|
| String     | `"Hello"`, `'World'` | `"string"`    |
| Number     | `42`, `3.14`, `NaN`  | `"number"`    |
| Boolean    | `true`, `false`      | `"boolean"`   |
| Null       | `null`               | `"object"`    |
| Undefined  | `undefined`          | `"undefined"` |
| Symbol     | `Symbol('id')`       | `"symbol"`    |
| BigInt     | `123n`               | `"bigint"`    |

### Object Types (Mutable)

- Objects: `{ key: 'value' }`
- Arrays: `[1, 2, 3]`
- Functions: `function() {}`
- Dates, RegExp, etc.

---

## 3. Type Coercion

**Implicit Conversion**:

```javascript
"5" + 2      // "52" (string concatenation)
"5" - 2      // 3 (number subtraction)
true + 1     // 2 (boolean → number)
null + "abc" // "nullabc"
```

**Explicit Conversion**:

```javascript
Number("123")  // 123
String(123)    // "123"
Boolean(0)     // false
!!"text"       // true (double NOT)
```

**Truthy/Falsy Values**:

```javascript
Falsy: false, 0, "", null, undefined, NaN
Truthy: All other values
```

---

## 4. Arrays

**Creation & Manipulation**:

```javascript
const fruits = ['Apple', 'Banana'];
fruits.push('Orange');        // Add to end
const last = fruits.pop();    // Remove from end
fruits[0] = 'Mango';          // Direct access
```

**Key Methods**:

```javascript
// ES6+ Methods
const doubled = [1, 2, 3].map(x => x * 2);
const even = [1, 2, 3].filter(x => x % 2 === 0);
const sum = [1, 2, 3].reduce((acc, val) => acc + val, 0);
```

**Characteristics**:

- Zero-indexed
- Dynamic size
- Can hold mixed types
- Array.isArray() for type checking

---

## 5. Functions & Scope

### Function Types

```javascript
// Function Declaration (hoisted)
function add(a, b) {
 return a + b;
}
// Function Expression
const multiply = function(a, b) { return a * b; }
// Arrow Function (ES6+)
const divide = (a, b) => a / b;
```

### Scope Hierarchy

1. **Global Scope**: Accessible everywhere
2. **Function Scope**: Created with `function`
3. **Block Scope**: Created with `{}` (let/const)
**Example**:

```javascript
let global = 1;
function test() {
 var functionScoped = 2;
 if (true) {
   let blockScoped = 3;
 }
}
```

---

## 6. Variable Declarations

|            | var      | let       | const         |
|------------|----------|-----------|---------------|
| Scope      | Function | Block     | Block         |
| Hoisting   | Yes      | Yes (TDZ) | Yes (TDZ)     |
| Reassign   | Yes      | Yes       | No            |
| Redeclare  | Yes      | No        | No            |
**Temporal Dead Zone (TDZ)**:

```javascript
console.log(x); // ReferenceError (TDZ)
let x = 5;
```

---

## 7. Strict Mode

**Enable**: `"use strict";` (script/function-level)  
**Enforces**:

- No undeclared variables
- No duplicate parameter names
- Restrictions on `eval`
- Immutable global `this` (undefined in functions)
**Example**:

```javascript
function strictFunc() {
 "use strict";
 undeclaredVar = 42; // ReferenceError
}
```

---

## 8. Template Literals (ES6+)

```javascript
const name = "Alice";
const age = 30;
// Interpolation
console.log(`Hello ${name}!`);
// Multi-line
const html = `
<div>
<p>Age: ${age}</p>
</div>
`;
// Tagged Templates
function tag(strings, ...values) {
 return strings[0] + values[0].toUpperCase();
}
tag`Hello ${name}`; // "Hello ALICE"
```

---

## 9. Naming Conventions

- **camelCase**: Variables, functions (`myVariable`)
- **PascalCase**: Constructors, Classes (`MyClass`)
- **UPPER_CASE**: Constants (`API_KEY`)
- **Valid Characters**: Letters, `$`, `_`
- **Reserved Words**: Avoid `class`, `function`, etc.

---

## 10. Operators

### Assignment

```javascript
let x = 10;
x += 5;  // 15
x **= 2; // 225 (exponentiation)
```

### Arithmetic

```javascript
10 % 3   // 1 (modulus)
2 ** 3   // 8 (exponent)
++x      // Prefix increment
x--      // Postfix decrement
```

### Comparison

```javascript
5 == "5"   // true (type coercion)
5 === "5"  // false (strict equality)
null == undefined  // true
NaN === NaN        // false (use Number.isNaN)
```

### Logical

```javascript
true && false  // false (AND)
true || false  // true (OR)
!true          // false (NOT)
null ?? "default" // "default" (Nullish coalescing)
```

---

## 11. Control Flow

### Conditionals

```javascript
// if-else
if (age > 18) {
 // ...
} else if (age > 13) {
 // ...
} else {
 // ...
}
// Ternary
const status = age >= 18 ? "Adult" : "Minor";
// Switch
switch(day) {
 case 1:
   console.log("Monday");
   break;
 default:
   console.log("Weekend");
}
```

### Loops

```javascript
// for
for (let i = 0; i < 5; i++) { ... }
// while
let i = 0;
while (i < 5) { ... }
// for...of (ES6+)
for (const item of array) { ... }
// for...in (objects)
for (const key in object) { ... }
```

---

## 12. Object Literals

**Creation**:

```javascript
const person = {
 name: "Alice",
 age: 30,
 greet() {
   console.log(`Hello, I'm ${this.name}`);
 }
};
```

**Features**:

- Property Shorthand:

 ```javascript
 const name = "Alice";
 const obj = { name }; // { name: "Alice" }
 ```

- Computed Properties:

 ```javascript
 const key = "id";
 const obj = { [key]: 123 }; // { id: 123 }
 ```

- Method Notation:

 ```javascript
 // Old: greet: function() {}
 // New: greet() {}
 ```

**Object Reference**:

```javascript
const obj1 = { a: 1 };
const obj2 = obj1; // Reference, not copy
obj2.a = 2;
console.log(obj1.a); // 2
```

---

# Key JavaScript Concepts

| Concept          | Description                           | Example                     |
|------------------|---------------------------------------|-----------------------------|
| **Hoisting**     | Variable/function declarations moved to top | `console.log(x); var x = 5;` → undefined |
| **Closure**      | Function + its lexical environment    | Function inside function    |
| **Prototype**    | Inheritance mechanism                 | `obj.__proto__`             |
| **Event Loop**   | Async execution model                 | `setTimeout`, Promises      |
| **IIFE**         | Immediately Invoked Function Expression | `(function() { ... })()` |

```javascript
// Modern JavaScript Best Practices
const PI = 3.14159;          // Use const by default
let count = 0;               // Use let when reassigning
function createUser() { ... } // Function declarations
const add = (a, b) => a + b; // Arrow for simple functions
```
