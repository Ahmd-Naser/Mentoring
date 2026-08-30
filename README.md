# Mentoring Platform API 🚀

A robust, highly scalable RESTful API built with **ASP.NET Core** and **Entity Framework Core**. This platform is designed to manage mentoring programs, track trainee problem-solving progress, manage study groups, and handle code submissions efficiently.

## 🏗️ Architecture

The project strictly follows the **Clean Architecture** principles to ensure separation of concerns, maintainability, and testability. The solution is divided into four main layers:

*   **`Mentoring.Api`**: The presentation layer containing Controllers, Dependency Injection setups, and application entry points.
*   **`Mentoring.Application`**: The use-case layer containing Services, Interfaces, DTOs (Contracts), Validators, and Mapping configurations.
*   **`Mentoring.Core`**: The domain layer containing Entities, Enums, Custom Errors, and Global Constants.
*   **`Mentoring.EF`**: The infrastructure/data access layer containing the `ApplicationDbContext`, Migrations, Entity Configurations, and JWT Providers.

## ✨ Key Features

*   **🔐 Advanced Authentication & Authorization**: 
    *   Secure Login/Register with JWT (JSON Web Tokens).
    *   Refresh Token mechanism for seamless user experience.
    *   Email Confirmation & Password Reset via HTML email templates.
    *   Role-Based Access Control (RBAC).
*   **👥 Group Management**: 
    *   Create and manage mentoring groups.
    *   Assign trainees to specific groups.
*   **🧩 Problem & Submission Tracking**: 
    *   Manage algorithmic problems with varying difficulty levels.
    *   Assign problems to groups/trainees.
    *   Track submissions, verdicts (Accepted, Wrong Answer, etc.), and code links.
    *   Monitor trainee progress and statistics.
*   **🛡️ Validation & Error Handling**: 
    *   Strong request validation using FluentValidation.
    *   Standardized custom `Result` and `Error` wrapper classes for consistent API responses.

## 🌐 API Endpoints Overview

Detailed and interactive API documentation is automatically generated. Run the application and navigate to `/scalar/v1` to explore and test all endpoints.

Here is a high-level overview of the main controllers:

*   **Auth (`/api/auth`)**: Handles user registration, login, JWT generation, refresh tokens, and password resets.
*   **Groups (`/api/groups`)**: Manages mentoring study groups, assigning trainees, and group-specific configurations.
*   **Problems (`/api/problems`)**: CRUD operations for algorithmic problems, including difficulty levels and status.
*   **Submissions (`/api/submissions`)**: Records and tracks trainee code submissions, verdicts, and code links.
*   **Trainee Problems (`/api/traineeproblems`)**: Tracks individual trainee progress, time spent, and reviews on specific problems.
*   
## 🛠️ Tech Stack

*   **Framework**: .NET 10 / ASP.NET Core Web API
*   **ORM**: Entity Framework Core
*   **Database**: Microsoft SQL Server
*   **Authentication**: ASP.NET Core Identity & JWT Bearer
*   **Mapping**: Mapster / AutoMapper

## 🚀 Getting Started

### Prerequisites
*   [.NET SDK](https://dotnet.microsoft.com/download) (v8.0 or later)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation & Setup

1. **Clone the repository**:
   git clone https://github.com/yourusername/Mentoring.git
   cd Mentoring-master

2. **Configure Environment Variables**:
   Navigate to `Mentoring.Api` and update `appsettings.json` with your database and JWT configurations:
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=MentoringDB;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "JwtOptions": {
       "Key": "Your_Super_Secret_Key_Here_Must_Be_Long",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience"
     }
   }

3. **Apply Database Migrations**:
   dotnet ef database update --project Mentoring.EF --startup-project Mentoring.Api

4. **Run the Application**:
   cd Mentoring.Api
   dotnet run

## 📂 Folder Structure Highlights

- `Templates/`: Contains `EmailConfirmation.html` and `ForgetPassword.html` for stylized email communications.
- `Errors/`: Domain-specific error definitions ensuring explicit and readable error states.
- `Abstractions/`: Contains the `Result<T>` pattern implementations to avoid throwing exceptions for business logic failures.
