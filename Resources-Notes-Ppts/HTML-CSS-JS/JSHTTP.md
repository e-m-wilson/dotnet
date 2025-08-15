
# Asynchronous JavaScript: JSON, Fetch, Promises, and Async/Await

## 1. JSON (JavaScript Object Notation)

**Definition**: Lightweight data format for data exchange  
**Structure**:

- Key-value pairs
- Double quotes for strings/property names
- Supported types: strings, numbers, booleans, objects, arrays, `null`

```javascript
// Valid JSON
{
 "name": "Alice",
 "age": 30,
 "isStudent": false,
 "courses": ["Math", "History"]
}
```

**JavaScript Methods**:

```javascript
// Convert object → JSON string
const jsonString = JSON.stringify({ id: 1, name: "Product" });
// Convert JSON → JavaScript object
const parsedObj = JSON.parse('{"id":1,"name":"Product"}');
```

---

## 2. Fetch API

**Modern HTTP Request Interface**  

```javascript
fetch('https://api.example.com/data')
 .then(response => {
   if (!response.ok) throw new Error('Network error');
   return response.json();
 })
 .then(data => console.log(data))
 .catch(error => console.error('Error:', error));
```

**Key Features**:

- Returns a Promise
- Supports all HTTP methods (GET, POST, etc.)
- Handles headers and request configuration
**POST Request Example**:

```javascript
fetch('https://api.example.com/items', {
 method: 'POST',
 headers: {
   'Content-Type': 'application/json',
 },
 body: JSON.stringify({ title: 'New Item' })
});
```

---

## 3. Promises

**Asynchronous Operation Manager**  
**States**:

- Pending: Initial state
- Fulfilled: Operation completed successfully
- Rejected: Operation failed
**Creation**:

```javascript
const promise = new Promise((resolve, reject) => {
 setTimeout(() => {
   Math.random() > 0.5
     ? resolve('Success!')
     : reject('Failed!');
 }, 1000);
});
```

**Methods**:

| Method          | Description                          |
|-----------------|--------------------------------------|
| `.then()`       | Handles fulfillment                  |
| `.catch()`      | Handles rejection                   |
| `.finally()`    | Runs regardless of outcome          |
| `Promise.all()` | Waits for all promises to resolve   |
| `Promise.race()`| Returns first settled promise       |

---

## 4. Promise Chaining with .then()

**Sequential Asynchronous Operations**  

```javascript
fetch('/api/user')
 .then(response => response.json())
 .then(user => fetch(`/api/posts/${user.id}`))
 .then(response => response.json())
 .then(posts => displayPosts(posts))
 .catch(error => showError(error));
```

**Error Handling**:

```javascript
somePromise
 .then(result => process(result))
 .catch(error => {
   console.error('Error:', error);
   return recoveryData; // Continue chain
 })
 .then(finalData => saveData(finalData));
```

---

## 5. Async/Await

**Syntactic Sugar for Promises**  

```javascript
async function fetchData() {
 try {
   const response = await fetch('/api/data');
   const data = await response.json();
   return processData(data);
 } catch (error) {
   console.error('Fetch failed:', error);
   throw error; // Propagate error
 }
}
```

**Key Rules**:

1. `async` functions always return a Promise
2. `await` pauses execution until Promise settles
3. Use `try/catch` for error handling
**Parallel Execution**:

```javascript
async function loadAll() {
 const [users, products] = await Promise.all([
   fetch('/users').then(r => r.json()),
   fetch('/products').then(r => r.json())
 ]);
 return { users, products };
}
```

---

# Comparison: Promises vs Async/Await

| Feature               | Promises                  | Async/Await               |
|-----------------------|---------------------------|---------------------------|
| **Readability**       | Chaining can get complex  | Sequential, synchronous-like |
| **Error Handling**    | `.catch()` chain          | `try/catch` blocks        |
| **Debugging**         | Harder with long chains   | Better stack traces       |
| **Return Value**      | Promise object            | Promise object            |
| **Browser Support**   | ES6+                      | ES2017+                   |

---

# Best Practices

1. **Always Handle Errors**: Use `.catch()` or `try/catch`
2. **Avoid Callback Hell**: Use Promise chains or async/await
3. **Clean Up Resources**: Use `finally` for cleanup tasks
4. **Cancelable Requests**: Use `AbortController` with Fetch
5. **Batch Parallel Requests**: Use `Promise.all()`

```javascript
// Advanced Fetch Example with Abort
const controller = new AbortController();
const timeoutId = setTimeout(() => controller.abort(), 5000);
try {
 const response = await fetch('/api/data', {
   signal: controller.signal
 });
 clearTimeout(timeoutId);
 // Process response
} catch (error) {
 if (error.name === 'AbortError') {
   console.log('Request timed out');
 }
}
```

# Real-World Example

```javascript
async function getUserPosts(userId) {
 try {
   const userRes = await fetch(`/users/${userId}`);
   if (!userRes.ok) throw new Error('User not found');
   const user = await userRes.json();
   const postsRes = await fetch(`/posts?userId=${user.id}`);
   return await postsRes.json();
 } catch (error) {
   console.error('Failed to load data:', error);
   return [];
 }
}
```
