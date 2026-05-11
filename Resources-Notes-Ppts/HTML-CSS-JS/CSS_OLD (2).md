# HTML 

## 1. HTML Document Structure

**Basic Template**:

```html
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Document Title</title>
</head>
<body>
<!-- Visible Content -->
</body>
</html>
```

**Key Components**:

- `<!DOCTYPE html>`: Declares HTML5 document type
- `<html>`: Root element wrapping all content (lang attribute specifies language)
- `<head>`: Contains metadata, scripts, and styles (not visible)
- `<meta charset="UTF-8">`: Sets character encoding to Unicode
- `<title>`: Defines browser tab title (SEO important)
- `<body>`: Holds all visible page content

---

## 2. Document Object Model (DOM)

**Definition**: Tree-like structure representing HTML document  
**Example**:

```html
<html>
<head>
<title>DOM Example</title>
</head>
<body>
<h1>Header</h1>
<p>Paragraph</p>
</body>
</html>
```

**DOM Tree**:

- html (root)
- head
  - title
    - "DOM Example"
- body
  - h1
    - "Header"
  - p
    - "Paragraph"

---

## 3. Elements & Attributes

**Element**: Tag + Content  

```html
<p class="intro">This is a paragraph</p>
```

**Attribute**: Provides additional information (always in opening tag)

```html
<img src="logo.jpg" alt="Company Logo" width="200">
```

**Common Attributes**:

- `id`: Unique identifier
- `class`: CSS class selector
- `style`: Inline CSS
- `title`: Tooltip text

---

## 4. Inline vs Block Elements

| Block Elements          | Inline Elements         |
|-------------------------|-------------------------|
| `<div>`                 | `<span>`                |
| `<p>`                   | `<a>`                   |
| `<h1>-<h6>`            | `<img>`                 |
| `<ul>`, `<ol>`, `<li>` | `<strong>`, `<em>`      |
| `<section>`             | `<input>`               |
| `<article>`             | `<button>`              |
**Example**:

```html
<div style="border: 1px solid">
   Block element (full width)
<span style="color: red">Inline element</span>
</div>
```

---

## 5. Common Tags & Examples

### Basic Content Tags

```html
<h1>Main Heading</h1>  <!-- Only one h1 per page -->
<h2>Subheading</h2>
<p>Paragraph text with <em>emphasis</em> and <strong>strong importance</strong>.</p>
<ul>
<li>Unordered List Item</li>
</ul>
<ol>
<li>Ordered List Item</li>
</ol>
<a href="https://example.com" target="_blank">External Link</a>
<img src="image.jpg" alt="Accessible Description">
```

### Semantic HTML5 Tags

```html
<header>Site Header</header>
<nav>Navigation Links</nav>
<main>
<article>Independent Content</article>
<section>Thematic Grouping</section>
</main>
<footer>Copyright Info</footer>
```

---

## 6. Input Elements & Types

**Basic Input**:

```html
<input type="text" id="name" name="username" placeholder="Enter name">
```

**Common Types**:

```html
<!-- Text Variations -->
<input type="password" placeholder="Password">
<input type="email" required>
<!-- Selection -->
<input type="checkbox" id="agree"> <label for="agree">I agree</label>
<input type="radio" name="gender" value="male"> Male
<!-- Specialized -->
<input type="date">
<input type="color">
<input type="file" accept=".pdf,.docx">
<input type="range" min="0" max="100">
<!-- HTML5 Validation -->
<input type="number" min="18" max="99">
<input type="url" pattern="https://.*">
```

---

## 7. Form Elements & Attributes

**Complete Form Example**:

```html
<form action="/submit" method="POST" enctype="multipart/form-data">
<fieldset>
<legend>Contact Info</legend>
<label for="email">Email:</label>
<input type="email" id="email" name="email" required>
<label for="bio">Bio:</label>
<textarea id="bio" name="bio" rows="4"></textarea>
</fieldset>
<label for="country">Country:</label>
<select id="country" name="country">
<option value="us">United States</option>
<option value="ca">Canada</option>
</select>
<input type="submit" value="Send">
</form>
```

**Key Attributes**:

- `action`: Submission endpoint URL
- `method`: HTTP verb (GET/POST)
- `enctype`: Encoding type (required for file uploads)
- `for` (label): Associates label with input via `id`

---

## 8. Select & Multi-Select

**Single Select**:

```html
<select name="fruit">
<option value="apple">Apple</option>
<option value="orange" selected>Orange</option>
</select>
```

**Multi-Select**:

```html
<select name="skills" multiple size="4">
<option value="html">HTML</option>
<option value="css">CSS</option>
<option value="js">JavaScript</option>
</select>
```

**Usage**: Ctrl/Cmd + Click to select multiple options
---

## 9. Form Submission

**Process**:

1. User clicks submit button
2. Browser collects form data
3. Data sent to `action` URL via specified `method`
4. Server processes request
**GET vs POST**:

- **GET**: Data in URL (bookmarkable, limited size)

 ```
 /submit?name=John&email=j@example.com
 ```

- **POST**: Data in request body (secure, larger payloads)

---

## 10. HTML5 Validation

**Built-in Validation Attributes**:

```html
<!-- Required Field -->
<input type="text" required>
<!-- Pattern Matching -->
<input pattern="[A-Za-z]{3}" title="3 letters">
<!-- Number Ranges -->
<input type="number" min="1" max="10">
<!-- Custom Validation Message -->
<input type="email" oninvalid="this.setCustomValidity('Valid email required')">
```

**Validation Types**:

- Email: `type="email"`
- URL: `type="url"`
- Tel: `type="tel"` (with pattern attribute)
- Date: `type="date"` (browser-native picker)
**Custom Validation**:

```html
<input type="text" id="username"
      pattern="[a-zA-Z0-9]{4,10}"
      title="4-10 alphanumeric characters">
```

---

# Key Concepts Summary

| Concept              | Key Points                                                                 |
|----------------------|---------------------------------------------------------------------------|
| **Semantic HTML**    | Use appropriate tags for content meaning (`<article>`, `<nav>`)           |
| **Accessibility**    | Always include `alt` for images, use `<label>` with `for` attribute       |
| **Form Security**    | Combine client-side (HTML5) and server-side validation                   |
| **DOM Manipulation** | JavaScript can modify DOM after page load (e.g., `document.getElementById()`) |
| **Responsive Images**| Use `srcset` and `sizes` attributes for adaptive images                   |
| **SEO Best Practices**| Proper use of heading hierarchy, meta tags, and semantic elements        |

```html
<!-- Advanced HTML5 Example -->
<input type="text"
      id="username"
      name="username"
      placeholder="Enter username"
      required
      minlength="4"
      maxlength="20"
      pattern="[a-zA-Z0-9_]+"
      title="Letters, numbers, and underscores only">
```
