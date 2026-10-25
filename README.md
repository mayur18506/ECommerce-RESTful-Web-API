# E-Commerce RESTful Web API 🛒

A secure, scalable backend REST API built for e-commerce operations including user authentication, role-based access control, product catalog management, shopping cart workflows, and order processing.

## 🚀 Tech Stack
* **Framework:** ASP.NET Core Web API (.NET 8)
* **ORM & Database:** Entity Framework Core (Code-First), MS SQL Server, LINQ
* **Security:** JWT (JSON Web Tokens) Authentication & Role-Based Authorization (`Admin` / `Customer`) & BCrypt Password Hashing
* **API Documentation:** Swagger UI (OpenAPI)

## ✨ Key Features
* **Auth Module (`/api/Auth`):** User Registration & Login with JWT Bearer token generation.
* **Product Catalog (`/api/Products`):** Public product browsing & category filtering; Admin-only CRUD operations.
* **Shopping Cart (`/api/Cart`):** Authenticated users can add, update, view, and remove items from their cart.
* **Order Processing (`/api/Orders`):** Checkout workflow with automatic stock validation, inventory deduction, and order history tracking.

## ⚙️ How to Run Locally
1. Clone the repository.
2. Update the SQL Server connection string in `appsettings.json`.
3. Open **Package Manager Console** and run:
   ```powershell
   Update-Database
