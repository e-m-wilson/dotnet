
# CSS

## 1. CSS Styling Methods Expanded

### Inline Styling

- **Definition**: Styles applied directly within HTML elements using `style` attribute
- **Example**:

 ```html
<div style="color: red; margin: 10px;">Content</div>
 ```

- **Use Cases**:
- Quick prototyping
- Overriding other styles temporarily
- **Drawbacks**:
- Hard to maintain
- No reusability
- Highest specificity (hard to override)

### Internal/Embedded Styling

- **Definition**: Styles placed within `<style>` tags in document head
- **Example**:

 ```html
<head>
<style>
     .container {
       width: 80%;
       margin: 0 auto;
     }
</style>
</head>
 ```

- **Best For**:
- Small, single-page projects
- Page-specific overrides

### External Styling

- **Definition**: Styles in separate .css files linked via `<link>` tag
- **Structure**:

 ```html
<!-- index.html -->
<head>
<link rel="stylesheet" href="styles/main.css">
</head>
 /* main.css */
 .button {
   padding: 10px 20px;
   border-radius: 5px;
 }
 ```

- **Advantages**:
- Reusability across pages
- Better maintainability
- Caching benefits

## 2. CSS Box Model Deep Dive

### Box Model Components

| Component | Description | Visual Representation |
|-----------|-------------|------------------------|
| Content   | Actual element content | `[Content]` |
| Padding   | Space between content and border | `[Padding ░ Content ░]` |
| Border    | Line around padding | `[Border █ Padding ░]` |
| Margin    | Space outside border | `Margin [Border █] Margin` |

### Box-Sizing Property

```css
/* Default (content-box) */
div {
 width: 300px;   /* Content width only */
 padding: 20px;  /* Added to total width */
}
/* Border-box sizing */
div {
 box-sizing: border-box; /* Includes padding + border in width */
 width: 300px;           /* Total width remains 300px */
 padding: 20px;
}
```

## 3. CSS Specificity Hierarchy

### Specificity Calculation Table

| Selector Type | Example | Specificity Value |
|---------------|---------|-------------------|
| Inline Style  | `style="..."` | 1000 |
| ID            | `#header` | 100 |
| Class/Attribute/Pseudo-class | `.menu`, `[type="text"]`, `:hover` | 10 |
| Elements/Pseudo-elements | `div`, `::after` | 1 |
| Universal Selector | `*` | 0 |

### Specificity Examples

1. `#nav li.active > a` = 100 + 1 + 10 + 1 = **112**
2. `body.home .hero-img` = 1 + 10 + 10 = **21**
3. `div#main .content p:first-child` = 100 + 1 + 10 + 1 + 10 = **122**

### !important Rule

```css
.warning {
 color: red !important; /* Overrides all other declarations */
}
```

**Recommendation**: Use sparingly - creates maintenance challenges

## 4. Advanced Selector Techniques

### Attribute Selectors

| Selector | Matches | Example |
|----------|---------|---------|
| `[attr]` | Elements with attribute | `[disabled]` |
| `[attr=value]` | Exact value match | `[type="email"]` |
| `[attr^=value]` | Starts with value | `[href^="https"]` |
| `[attr$=value]` | Ends with value | `[src$=".jpg"]` |
| `[attr*=value]` | Contains value | `[class*="btn-"]` |

### Pseudo-class Deep Dive

```css
/* Form states */
input:disabled { opacity: 0.5; }
input:checked + label { font-weight: bold; }
/* Structural */
li:nth-child(3n+1) { /* Every 3rd item starting at 1 */ }
tr:nth-of-type(even) { background: #f5f5f5; }
div:last-child { margin-bottom: 0; }
/* Interactive */
button:active { transform: scale(0.98); }
a:focus-visible { outline: 2px solid blue; }
```

## 5. Flexbox Layout System

### Container Properties Breakdown

```css
.container {
 display: flex;
 flex-direction: row;      /* Main axis direction */
 flex-wrap: wrap;          /* Allow wrapping */
 justify-content: center;  /* Main axis alignment */
 align-items: stretch;     /* Cross axis alignment */
 gap: 20px;                /* Space between items */
}
```

### Item Properties Deep Dive

```css
.item {
 order: 2;            /* Display order (default 0) */
 flex: 1 1 200px;     /* flex-grow | flex-shrink | flex-basis */
 align-self: flex-end; /* Override container alignment */
}
```

### Common Flex Patterns

1. **Equal Height Columns**:

  ```css
  .row {
    display: flex;
  }
  .col {
    flex: 1; /* Equally distribute space */
  }
  ```

2. **Responsive Navigation**:

  ```css
  .nav {
    display: flex;
    flex-wrap: wrap;
    gap: 1rem;
  }
  ```

## 6. CSS Grid System Mastery

### Grid Template Definition

```css
.container {
 display: grid;
 grid-template-columns:
   [sidebar] 200px
   [main] 1fr
   [aside] 300px;
 grid-template-rows:
   100px
   auto
   100px;
 grid-template-areas:
   "header header header"
   "sidebar main aside"
   "footer footer footer";
 gap: 1rem;
}
```

### Grid Placement Techniques

```css
.header {
 grid-area: header;
 grid-column: 1 / -1; /* Span all columns */
}
.sidebar {
 grid-row: 2 / 4; /* Span multiple rows */
}
.item {
 grid-column: main; /* Use named grid lines */
}
```

### Responsive Grid Example

```css
.grid {
 display: grid;
 grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
 gap: 20px;
}
```

## 7. CSS Cascade & Inheritance

### Cascade Factors (Priority Order)

1. Importance (`!important`)
2. Origin (user > author > browser)
3. Specificity
4. Source Order (last declaration wins)

### Inheritance Control

```css
.parent {
 font-size: 1.2rem;
 border: 1px solid #ccc;
}
.child {
 font-size: inherit;   /* Inherits 1.2rem */
 border: initial;      /* Resets to default */
 color: unset;         /* Resets inheritance */
}
```

## 8. Advanced Layout Techniques

### CSS Custom Properties

```css
:root {
 --primary-color: #2196F3;
 --spacing-unit: 8px;
}
.button {
 background: var(--primary-color);
 padding: calc(var(--spacing-unit) * 2);
}
```

### Modern Layout Approach

```css
/* Combine Grid and Flexbox */
.container {
 display: grid;
 grid-template-columns: 1fr 3fr;
}
.sidebar {
 display: flex;
 flex-direction: column;
 gap: 1rem;
}
```

## Best Practices Checklist

1. **Mobile First**: Use min-width media queries
2. **Semantic Class Names**: `.card-header` vs `.blue-box`
3. **Responsive Images**: Use `srcset` and `picture`
4. **Performance**: Limit complex selectors
5. **Accessibility**: Test contrast ratios
6. **Maintainability**: Use CSS methodologies (BEM, SMACSS)

```css
/* BEM Methodology Example */
.block {}
.block__element {}
.block--modifier {}
```
