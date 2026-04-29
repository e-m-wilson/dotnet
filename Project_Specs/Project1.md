# Project Specification: Full-Stack .NET Web Application

## Overview
In this project, you will design and build a full-stack web application using **C# and .NET technologies**. The application will include:

- A **RESTful Web API** built with ASP.NET
- A **data layer** using Entity Framework (EF) Core
- A **front-end interface** built with HTML, CSS, and JavaScript

Your goal is to demonstrate your understanding of backend development, database integration, API design, and responsive front-end implementation.

---

## Project Requirements

### 1. Backend: ASP.NET Web API

You must create a RESTful API using ASP.NET that exposes endpoints for interacting with your application’s data.

#### Sub-requirements:
- **Project Setup**
  - Create a layered .NET Web API - using multi-project solution
  - Keep Organized folders (Controllers, Models, Data, Services, etc.)
  
- **Routing & Controllers**
  - Implement the following RESTful routes (GET, POST, PUT, DELETE)
  - Use proper route naming conventions
  - Handle HTTP status codes appropriately (200, 201, 400, 404, etc.)

- **Data Transfer**
  - Use DTOs (Data Transfer Objects) where appropriate

- **Error Handling**
  - Implement basic error handling and logging

---

### 2. Data Layer: Entity Framework Core

You will use EF Core to manage database interactions.

#### Sub-requirements:
- **Database Setup**
  - Configure a database connection
  - Use code-first migrations to create/update schema

- **Models**
  - Define at least 3 entity models with appropriate properties

- **CRUD Operations**
  - Implement Create, Read, Update, Delete operations via EF Core
  - Use LINQ queries to retrieve and manipulate data

- **Relationships**
  - Implement each relationship (one-to-many and many-to-many)

---

### 3. Front-End: HTML, CSS, JavaScript

You must build a client-side interface that interacts with your API.

#### Sub-requirements:

##### HTML Structure
- Use semantic HTML elements
- Organize content clearly (forms, lists, navigation)

##### CSS Styling
- Apply consistent styling across the application
- Use classes and reusable styles

##### Responsive Design Practices
- Ensure the layout adapts to different screen sizes

**Key Concepts:**
- Use **CSS Flexbox**
  - Align and distribute elements within containers
  - Handle layout direction (row vs column)
- Optional: Use CSS Grid for advanced layouts
- Implement media queries for responsiveness

##### JavaScript Functionality
- Fetch data from the API using `fetch` or `axios`
- Dynamically update the DOM
- Handle user input (forms, buttons)

---

### 4. Code Quality & Organization

#### Sub-requirements:
- Follow consistent naming conventions
- Write clean, readable, and maintainable code
- Separate concerns (controllers vs services vs data access)
- Comment code where necessary

---

## Deliverables

- Source code (backend + frontend)
- ERD

---

## Evaluation Criteria

- Functionality (Does it work as expected?)
- Code quality and structure
- Proper use of .NET, EF Core, and REST principles
- Front-end usability and responsiveness
- Completeness of requirements

---

## Summary

This project is designed to help you integrate multiple core development skills:
- Backend API development with ASP.NET
- Database management with EF Core
- Front-end design with HTML/CSS/JS
- Responsive design using Flexbox
- Full-stack application architecture

Focus on building a clean, functional, and well-structured application.