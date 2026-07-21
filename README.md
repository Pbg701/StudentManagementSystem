# Student Management System

A comprehensive Student Management System built with ASP.NET Core Web API, following Clean Architecture principles and enterprise-grade best practices.

## 📋 Project Overview

This project demonstrates a full-featured Student Management System with:

- **Clean Architecture** with proper separation of concerns (Domain, Application, Infrastructure, API)
- **JWT Authentication** for secure API endpoints
- **Global Exception Handling** with custom middleware
- **Structured Logging** with Serilog
- **Swagger API Documentation** with JWT support
- **Pagination and Search** functionality
- **Audit Fields** (CreatedDate, UpdatedDate, IsDeleted)
- **FluentValidation** for input validation
- **AutoMapper** for DTO mapping
- **Unit Testing** with xUnit and Moq
- **Docker Support** for containerization
- **CORS Support** for React frontend integration

## 🚀 Prerequisites

- **.NET SDK 10.0.201** (or later)
- SQL Server / SQL Server Express
- Visual Studio 2022 (17.14 or later) or Visual Studio Code
- Docker Desktop (optional)

## 🛠️ Technology Stack

### Backend

- **Framework:** ASP.NET Core Web API
- **.NET SDK:** 10.0.201
- **ORM:** Entity Framework Core 9
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **Validation:** FluentValidation 11
- **Mapping:** AutoMapper 16
- **Logging:** Serilog
- **Testing:** xUnit & Moq
- **API Documentation:** Swagger (Swashbuckle)
- **Containerization:** Docker (Optional)

### Architecture

- **Domain Layer:** Entities, Interfaces, Exceptions
- **Application Layer:** DTOs, Services, Validators, Mappings
- **Infrastructure Layer:** Data, Repositories, Unit of Work
- **API Layer:** Controllers, Middleware, Authentication

## ▶️ Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/Pbg701/StudentManagementSystem.git
cd StudentManagementSystem
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Create Database

```bash
dotnet ef database update --project StudentManagementSystem.Infrastructure --startup-project StudentManagementSystem.Api
```

### 4. Run the API

```bash
dotnet run --project StudentManagementSystem.Api
```

### 5. Open Swagger

```
https://localhost:7133/swagger
```

or

```
http://localhost:5197/swagger
```

## 📂 Project Structure
StudentManagementSystem/
├── StudentManagementSystem.API/ # Presentation Layer
│ ├── Controllers/ # API Endpoints
│ │ ├── AuthController.cs
│ │ └── StudentsController.cs
│ ├── Middleware/ # Custom Middleware
│ │ └── ExceptionHandlingMiddleware.cs
│ ├── Program.cs # Application Entry Point
│ ├── appsettings.json # Configuration
│ └── appsettings.Development.json
├── StudentManagementSystem.Application/ # Application Layer
│ ├── DTOs/ # Data Transfer Objects
│ │ ├── CreateStudentDto.cs
│ │ ├── UpdateStudentDto.cs
│ │ ├── StudentDto.cs
│ │ ├── PaginatedResultDto.cs
│ │ ├── LoginDto.cs
│ │ └── ErrorResponseDto.cs
│ ├── Interfaces/ # Service Interfaces
│ │ ├── IStudentService.cs
│ │ └── IAuthService.cs
│ ├── Services/ # Business Logic
│ │ ├── StudentService.cs
│ │ └── AuthService.cs
│ ├── Validators/ # FluentValidation
│ │ ├── CreateStudentDtoValidator.cs
│ │ └── UpdateStudentDtoValidator.cs
│ ├── Mappings/ # AutoMapper Profiles
│ │ └── MappingProfile.cs
│ └── Common/ # Common Utilities
│ └── Constants.cs
├── StudentManagementSystem.Domain/ # Domain Layer
│ ├── Entities/ # Domain Entities
│ │ └── Student.cs
│ ├── Interfaces/ # Repository Interfaces
│ │ ├── IStudentRepository.cs
│ │ └── IUnitOfWork.cs
│ ├── Exceptions/ # Custom Exceptions
│ │ ├── StudentNotFoundException.cs
│ │ └── ValidationException.cs
│ └── Common/ # Common Domain
│ └── BaseEntity.cs
├── StudentManagementSystem.Infrastructure/ # Infrastructure Layer
│ ├── Data/ # DbContext
│ │ └── ApplicationDbContext.cs
│ ├── Repositories/ # Repository Implementation
│ │ ├── StudentRepository.cs
│ │ └── UnitOfWork.cs
│ └── Migrations/ # EF Core Migrations
│ └── InitialCreate.cs
├── StudentManagementSystem.Tests/ # Test Project
│ ├── UnitTests/ # Unit Tests
│ │ └── StudentServiceTests.cs
│ └── IntegrationTests/ # Integration Tests
├── .dockerignore
├── Dockerfile # Docker Configuration
├── docker-compose.yml # Docker Compose
├── StudentManagementSystem.sln # Solution File
├── .gitignore
└── README.md # Documentation


## 👨‍💻 Author

**Prashant Gaikwad**

Software Engineer | ASP.NET Core | C# | Entity Framework Core | SQL Server | Flutter

**GitHub:** https://github.com/Pbg701
