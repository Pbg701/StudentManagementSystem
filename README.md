 Student Management System
A comprehensive full-stack Student Management System built with ASP.NET Core Web API and React with Material-UI, following Clean Architecture principles and enterprise-grade best practices.

📋 Project Overview
This project demonstrates a production-ready Student Management System with:

Backend Features
Clean Architecture with proper separation of concerns (Domain, Application, Infrastructure, API)

JWT Authentication for secure API endpoints

Global Exception Handling with custom middleware

Structured Logging with Serilog

Swagger API Documentation with JWT support

Pagination and Search functionality

Audit Fields (CreatedDate, UpdatedDate, IsDeleted)

FluentValidation for input validation

AutoMapper for DTO mapping

Unit Testing with xUnit and Moq

Docker Support for containerization

CORS Support for React frontend integration

Frontend Features
React 18 with TypeScript

Material-UI v5 for professional UI components

JWT Authentication with protected routes

CRUD Operations (Create, Read, Update, Delete)

Responsive Design for all screen sizes

Form Validation with real-time feedback

Toast Notifications for user feedback

Loading States for better UX

Error Handling with graceful degradation

🚀 Prerequisites
Backend
.NET SDK 10.0.201 or later

SQL Server / SQL Server Express

Visual Studio 2022 or Visual Studio Code

Docker Desktop (optional)

Frontend
Node.js 18+ and npm

Modern web browser

🛠️ Technology Stack
Backend Stack
Technology	Version	Purpose
ASP.NET Core Web API	10.0	REST API Framework
Entity Framework Core	9.0	ORM
SQL Server	2022	Database
JWT Bearer Auth	Latest	Authentication
FluentValidation	11.0	Input Validation
AutoMapper	16.0	Object Mapping
Serilog	Latest	Structured Logging
Swashbuckle	6.5	API Documentation
xUnit & Moq	Latest	Unit Testing
Docker	Latest	Containerization
Frontend Stack
Technology	Version	Purpose
React	18.0	UI Framework
TypeScript	5.0	Type Safety
Material-UI	5.14	UI Components
Axios	1.6	HTTP Client
React Router DOM	6.20	Routing
Emotion	11.11	CSS-in-JS
📂 Project Structure
text
StudentManagementSystem/
├── 
│   ├── StudentManagementSystem.API/           # Presentation Layer
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   └── StudentsController.cs
│   │   ├── Middleware/
│   │   │   └── ExceptionHandlingMiddleware.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json
│   │
│   ├── StudentManagementSystem.Application/   # Application Layer
│   │   ├── DTOs/
│   │   │   ├── CreateStudentDto.cs
│   │   │   ├── UpdateStudentDto.cs
│   │   │   ├── StudentDto.cs
│   │   │   ├── PaginatedResultDto.cs
│   │   │   ├── LoginDto.cs
│   │   │   └── ErrorResponseDto.cs
│   │   ├── Interfaces/
│   │   │   ├── IStudentService.cs
│   │   │   └── IAuthService.cs
│   │   ├── Services/
│   │   │   ├── StudentService.cs
│   │   │   └── AuthService.cs
│   │   ├── Validators/
│   │   │   ├── CreateStudentDtoValidator.cs
│   │   │   └── UpdateStudentDtoValidator.cs
│   │   ├── Mappings/
│   │   │   └── MappingProfile.cs
│   │   └── Common/
│   │       └── Constants.cs
│   │
│   ├── StudentManagementSystem.Domain/        # Domain Layer
│   │   ├── Entities/
│   │   │   └── Student.cs
│   │   ├── Interfaces/
│   │   │   ├── IStudentRepository.cs
│   │   │   └── IUnitOfWork.cs
│   │   ├── Exceptions/
│   │   │   ├── StudentNotFoundException.cs
│   │   │   └── ValidationException.cs
│   │   └── Common/
│   │       └── BaseEntity.cs
│   │
│   ├── StudentManagementSystem.Infrastructure/ # Infrastructure Layer
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Repositories/
│   │   │   ├── StudentRepository.cs
│   │   │   └── UnitOfWork.cs
│   │   └── Migrations/
│   │       └── InitialCreate.cs
│   │
│   ├── StudentManagementSystem.Tests/         # Test Project
│   │   ├── UnitTests/
│   │   │   └── StudentServiceTests.cs
│   │   └── IntegrationTests/
│   │
│   ├── Dockerfile
│   ├── docker-compose.yml
│   └── StudentManagementSystem.sln
│
└── Frontend/
    ├── student-management-ui/
    │   ├── src/
    │   │   ├── api/
    │   │   │   └── apiClient.ts
    │   │   ├── components/
    │   │   │   ├── Login.tsx
    │   │   │   ├── StudentList.tsx
    │   │   │   └── StudentForm.tsx
    │   │   ├── types/
    │   │   │   └── index.ts
    │   │   ├── App.tsx
    │   │   ├── App.css
    │   │   └── index.tsx
    │   ├── package.json
    │   ├── tsconfig.json
    │   └── README.md
▶️ Installation & Running
Backend Setup
1. Clone the Repository
bash
git clone https://github.com/Pbg701/StudentManagementSystem.git
cd StudentManagementSystem
2. Restore Dependencies
bash
cd Backend
dotnet restore
3. Configure Database Connection
Update appsettings.json in StudentManagementSystem.API:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "Key": "YourSuperSecretKeyForJWTTokenGeneration1234567890",
    "Issuer": "StudentManagementAPI",
    "Audience": "StudentManagementClient",
    "DurationInMinutes": 60
  }
}
4. Apply Migrations
bash
dotnet ef database update --project StudentManagementSystem.Infrastructure --startup-project StudentManagementSystem.API
5. Run the Backend
bash
dotnet run --project StudentManagementSystem.API
The API will be available at:

HTTPS: https://localhost:7133

HTTP: http://localhost:5197

Swagger: https://localhost:7133/swagger

Frontend Setup
1. Navigate to Frontend Directory
bash
cd Frontend/student-management-ui
2. Install Dependencies
bash
npm install
3. Configure API URL
Update src/api/apiClient.ts with your backend URL:

typescript
const API_BASE_URL = 'https://localhost:7133'; // Your backend API URL
4. Run the Frontend
bash
npm start
The React app will be available at http://localhost:3000

Docker Setup (Optional)
Run with Docker Compose
bash
docker-compose up -d
This will start:

SQL Server container

Backend API container

Frontend React container

Access the application at:

Frontend: http://localhost:3000

Backend API: http://localhost:7133

Swagger: http://localhost:7133/swagger

🔐 Default Login Credentials
Username	Password	Role
admin	admin123	Administrator
user	user123	User
📝 API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/Auth/login	Authenticate user and get JWT token
Students
Method	Endpoint	Description	Auth Required
GET	/api/Students	Get all students	Yes
GET	/api/Students/{id}	Get student by ID	Yes
POST	/api/Students	Create new student	Yes
PUT	/api/Students/{id}	Update existing student	Yes
DELETE	/api/Students/{id}	Delete student	Yes
GET	/api/Students/paginated	Get paginated students	Yes
GET	/api/Students/search	Search students	Yes
🧪 Testing
Backend Tests
bash
cd Backend
dotnet test
Frontend Tests
bash
cd Frontend/student-management-ui
npm test
🔒 Security Features
JWT Token Authentication with expiration

Role-based Authorization (Admin/User)

Input Validation using FluentValidation

SQL Injection Prevention via Entity Framework Core

CORS Policy configured for frontend

Password Hashing using BCrypt

Audit Logging for all operations

Global Exception Handling with structured responses

Request/Response Logging with Serilog

📊 Database Schema
Student Table
Column	Type	Description
Id	INT	Primary Key, Auto Increment
Name	NVARCHAR(100)	Student's full name
Email	NVARCHAR(100)	Student's email (unique)
Age	INT	Student's age (1-120)
Course	NVARCHAR(100)	Student's course
CreatedDate	DATETIME	Record creation timestamp
UpdatedDate	DATETIME	Record update timestamp
IsDeleted	BIT	Soft delete flag
🎯 Key Features Demonstrated
Backend
✅ Clean Architecture implementation

✅ Repository pattern with Unit of Work

✅ DTOs and AutoMapper for data transfer

✅ FluentValidation for request validation

✅ Global exception handling middleware

✅ Structured logging with Serilog

✅ JWT authentication and authorization

✅ Swagger/OpenAPI documentation

✅ Pagination and search functionality

✅ Soft delete support

✅ Audit fields (CreatedDate, UpdatedDate)

✅ Unit testing with xUnit and Moq

✅ Docker containerization

Frontend
✅ Modern React with TypeScript

✅ Professional Material-UI design

✅ JWT token management

✅ Protected routes

✅ CRUD operations UI

✅ Form validation

✅ Toast notifications

✅ Loading states

✅ Responsive design

✅ Error handling

✅ API integration with Axios

🤝 Contributing
Fork the repository

Create your feature branch (git checkout -b feature/AmazingFeature)

Commit your changes (git commit -m 'Add some AmazingFeature')

Push to the branch (git push origin feature/AmazingFeature)

Open a Pull Request

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.

👨‍💻 Author
Prashant Gaikwad

GitHub: @Pbg701

LinkedIn: Prashant Gaikwad

Email: prashantgaikwad701@gmail.com

🙏 Acknowledgments
Microsoft for ASP.NET Core

React Team for the amazing UI library

Material-UI for the beautiful components

All open-source contributors

📸 Screenshots
Add screenshots here

🎥 Demo
Add demo video link here

⚡ Quick Start
Backend
bash
# Clone repo
git clone https://github.com/Pbg701/StudentManagementSystem.git
cd StudentManagementSystem

# Restore packages
dotnet restore

# Update database
dotnet ef database update --project StudentManagementSystem.Infrastructure --startup-project StudentManagementSystem.API

# Run API
dotnet run --project StudentManagementSystem.API
Frontend
bash
cd ../Frontend/student-management-ui

# Install dependencies
npm install

# Update API URL in src/api/apiClient.ts
# Change API_BASE_URL to 'https://localhost:7133'

# Run React app
npm start
Default User
Username: admin

Password: password

