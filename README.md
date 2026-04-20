# 🛒 E-Commerce Web API (Secure Backend)

A robust and scalable E-Commerce Backend built with **ASP.NET Core Web API**, focusing on security, clean architecture, and professional user management.

---

## 🚀 Features

### 🔐 Security & Identity
- **Full Identity System**: User registration and login managed via **ASP.NET Core Identity**.
- **JWT Authentication**: Secure access using **JSON Web Tokens** for stateless authentication.
- **Role-Based Authorization**: Distinct permissions for **Admin** (Manage products/orders) and **Customer** (Browse/Shop).

### 📦 Core Functionality
- **Product Management**: Complete CRUD with categories, search, and price filtering.
- **Shopping Cart**: Fully functional cart linked to the authenticated user.
- **Order Processing**: Create orders from cart items with status tracking (Pending, Shipped, etc.).
- **Data Integrity**: Used **Entity Framework Core** with SQL Server and handled JSON cycles.

---

## 🛠️ Tech Stack

* **Framework:** .NET 8 / ASP.NET Core Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Security:** JWT (JSON Web Tokens)
* **Documentation:** Swagger UI (OpenAPI)

---

## ⚙️ Project Structure

- **Controllers**: Handles API Endpoints (Account, Product, Cart, Order, Category).
- **Services**: Business logic layer.
- **Repositories**: Data access layer using **Repository Pattern**.
- **DTOs**: Data Transfer Objects for clean request/response handling.
