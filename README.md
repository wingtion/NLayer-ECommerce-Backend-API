# 🛒 N-Layer E-Commerce Backend API

Modern, secure, and scalable RESTful API built with **.NET 8** and **Clean Architecture** principles.

## Features
*   **N-Tier Architecture:** Core, Repository, Service, and API layers separation.
*   **Authentication & Authorization:** Secure login system using **ASP.NET Identity** and **JWT (JSON Web Tokens)**.
*   **Data Access:** Entity Framework Core with **Code-First** approach.
*   **Design Patterns:** Generic Repository, Unit of Work, Dependency Injection.
*   **Data Validation:** Global validation pipeline using **FluentValidation**.
*   **Object Mapping:** Clean DTO transformations with **AutoMapper**.
*   **API Documentation:** Interactive API testing via **Swagger UI**.
*   **Standardized Responses:** Global custom response wrapper for consistent API outputs.

## Tech Stack
*   **Framework:** .NET 8.0 (ASP.NET Core Web API)
*   **Database:** MSSQL Server (LocalDB)
*   **ORM:** Entity Framework Core 8
*   **Libraries:** AutoMapper, FluentValidation, Serilog 

## ⚙️ Getting Started

### Prerequisites
*   .NET 8 SDK
*   SQL Server (LocalDB or Express)

### Installation
1.  Clone the repository:
    ```bash
    git clone https://github.com/KULLANICI_ADIN/NLayer.ECommerce.API.git
    ```
2.  Navigate to the API folder and update `appsettings.json` with your SQL Connection String.
3.  Run migrations to create the database:
    ```bash
    Update-Database
    ```
4.  Run the project and visit `https://localhost:7xxx/swagger` to test endpoints.

## 🔐 How to Test (Swagger)
1.  **Register:** Create a new user via `/api/Auth/register`.
2.  **Login:** Get your Access Token via `/api/Auth/login`.
3.  **Authorize:** Click the "Lock" icon in Swagger and paste the token as `Bearer YOUR_TOKEN`.
4.  **Enjoy:** Now you can manage Products and Categories!
