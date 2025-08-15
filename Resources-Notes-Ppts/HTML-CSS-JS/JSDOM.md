
# JavaScript DOM

## 1. DOM Structure

**Document Object Model**: Tree representation of HTML document  
**Node Types**:

- Document (root)
- Element nodes (`<div>`, `<p>`)
- Text nodes
- Attribute nodes
- Comment nodes

```html
<!-- HTML -->
<html>
<head>
<title>DOM Demo</title>
</head>
<body>
<div id="container">
<p class="text">Hello World</p>
</div>
</body>
</html>
```

**DOM Tree**:

```
Document
└── html
   ├── head
   │   └── title
   │       └── "DOM Demo"
   └── body
       └── div#container
           └── p.text
               └── "Hello World"
```

---

## 2. Selecting Elements

### Core Methods

| Method                          | Returns                | Example                     |
|---------------------------------|------------------------|-----------------------------|
| `document.getElementById()`    | Single Element         | `#container`                |
| `document.querySelector()`     | First Matching Element | `.text`                     |
| `document.querySelectorAll()`  | NodeList (Static)      | `div > p`                   |
| `element.getElementsByTagName()`| HTMLCollection (Live)  | `getElementsByTagName('p')` |
| `element.getElementsByClassName()`| HTMLCollection      | `getElementsByClassName('text')` |

```javascript
// Common selections
const container = document.getElementById('container');
const paragraphs = document.querySelectorAll('p.text');
const firstDiv = document.querySelector('div');
```

---

## 3. DOM Traversal

### Navigation Properties

| Property                     | Description                        |
|------------------------------|------------------------------------|
| `parentNode`                 | Direct parent node                 |
| `childNodes`                 | All child nodes (including text)   |
| `children`                   | Only element children              |
| `firstChild`/`lastChild`     | First/last child node              |
| `firstElementChild`/`lastElementChild` | First/last element child |
| `nextSibling`/`previousSibling` | Adjacent nodes                 |
| `nextElementSibling`/`previousElementSibling` | Adjacent elements |

```javascript
// Traversal example
const paragraph = document.querySelector('p');
const parentDiv = paragraph.parentNode;
const firstChild = parentDiv.firstElementChild;
```

---

## 4. DOM Manipulation

### Element Creation/Modification

```javascript
// Create element
const newDiv = document.createElement('div');
newDiv.textContent = "New Content";
// Modify attributes
newDiv.setAttribute('data-id', '123');
newDiv.id = "newElement";
newDiv.classList.add('active', 'highlight');
// Modify styles
newDiv.style.backgroundColor = '#f0f0f0';
newDiv.style.fontSize = '1.2rem';
// Insert elements
document.body.appendChild(newDiv);
parentDiv.insertBefore(newDiv, paragraph);
// Remove elements
parentDiv.removeChild(paragraph);
```

### Content Manipulation

```javascript
element.innerHTML = '<strong>Bold Text</strong>'; // Parses HTML
element.textContent = 'Plain Text'; // Safer for user content
element.innerText; // CSS-aware text (avoid for performance)
```

---

## 5. Events & Listeners

### Event Phases

1. **Capturing**: Window → Target
2. **Target**: Element where event occurred
3. **Bubbling**: Target → Window

```javascript
// Event Listener Syntax
element.addEventListener('click', handler, {
 capture: false, // Default (bubbling phase)
 once: true,      // Auto-remove after trigger
 passive: true    // Optimize for scroll events
});
// Common Events
const handleClick = (e) => {
 console.log('Target:', e.target);
 console.log('Current Target:', e.currentTarget);
 e.stopPropagation(); // Prevent bubbling
};
element.addEventListener('click', handleClick);
window.addEventListener('resize', handleResize);
form.addEventListener('submit', handleSubmit);
```

---

## 6. Event Bubbling vs Capturing

|                          | Bubbling            | Capturing           |
|--------------------------|---------------------|---------------------|
| **Direction**            | Target → Document   | Document → Target   |
| **Default Phase**        | Yes                 | No (needs explicit) |
| **Usage**                | Most common         | Special cases       |
| **Control**              | `stopPropagation()` | `capture: true`     |
**Example**:

```javascript
// Capturing Phase
document.querySelector('div').addEventListener('click',
 () => console.log('Capturing'),
 true
);
// Bubbling Phase
document.querySelector('p').addEventListener('click',
 () => console.log('Bubbling')
);
```

---

## 7. Event Delegation

**Efficient Handling** for dynamic elements:

```javascript
document.getElementById('list').addEventListener('click', (e) => {
 if(e.target.matches('li.item')) {
   console.log('List item clicked:', e.target.dataset.id);
 }
});
```

---

# Best Practices

1. **Cache DOM References**: Store frequently used elements
2. **Batch DOM Changes**: Use `documentFragment` for multiple updates
3. **Debounce Events**: For scroll/resize handlers
4. **Use Event Delegation**: For dynamic content
5. **Clean Up**: Remove unused event listeners

```javascript
// Document Fragment Example
const fragment = document.createDocumentFragment();
for(let i = 0; i < 100; i++) {
 const li = document.createElement('li');
 li.textContent = `Item ${i}`;
 fragment.appendChild(li);
}
document.getElementById('list').appendChild(fragment);
```

# Performance Considerations

| Operation                | Cost  | Optimization               |
|--------------------------|-------|----------------------------|
| Reflow (Layout)          | High  | Batch style changes        |
| Query Selectors          | Medium| Use specific selectors     |
| Event Listeners          | Low   | Use delegation             |
| Forced Synchronous Layout| Critical | Avoid reading layout props after writes |
