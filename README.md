## Library Management System Project 📚📖🧍‍♂️🧍🏻‍♀️🏛️📈

📝 About Project
The Library Management System is a RESTful API built with .NET to efficiently manage libraries, people, and books. It provides full CRUD operations for all entities and follows the Clean Architecture pattern. The system utilizes MediatR for handling commands and queries. The project utilizes Docker for containerization and Docker Compose for managing multi-service orchestration, including the API and database.

## Features

### 📖 Library Management  
- Both **libraries** and **individuals** can manage book ownership.  
- Users can **add and remove books** from libraries and personal collections.  

### 🔄 Soft Delete Implementation  
- Ensures **data security** by marking records as **deleted** instead of permanently removing them.  

### 🔗 Entity Relationships  
- **Many-to-Many Relationships** exist between:  
  - **People & Books**  
  - **Libraries & Books**  

### 🔐 Authentication & Authorization  
- Uses **JWT Tokens** for securing API endpoints.  
- Supports **Role-Based Access Control (RBAC)**.  

### 🚀 Caching Mechanism  
- Enhances **performance** by caching frequently accessed data.  

### 🔄 Database Transactions  
- Ensures **data integrity** with proper **transaction management**.  

### 📦 Database Migrations  
- Supports **version-controlled schema changes** using **Entity Framework Core Migrations**.  

### 🐳 **Docker & Docker Compose**  
- The application is **fully containerized** using **Docker** for easy deployment.  
- **Docker Compose** orchestrates multiple services (**API, Database**) ensuring seamless integration.  
- Provides **environment isolation**, making setup and execution consistent across different machines.  
- Uses **Dockerfiles** to define the **API service** and **database dependencies**.  


Technologies Used

1) .NET (ASP.NET Core)
2) Entity Framework Core (EF Core)
3) MediatR for CQRS pattern
4) LINQ for querying collections efficiently
5) JWT Authentication
6) Memory Caching
7) SQL Server / PostgreSQL (configurable database support)
8) FluentValidation for request validation
9) Dependency Injection
10) Rate Limiting 
11) Docker & Docker Compose 


## ⚠️ Known Issues

### Migration Issues  
I encountered a problem with database migrations, which led to inconsistencies. To resolve this, I removed the previous migrations and generated new ones.  

### Issue with Deleting People and Libraries by ID  
There is an issue when attempting to delete **People** and **Libraries** by their ID. Although I have reviewed the implementation and debugged the code, I have not yet identified the root cause.


## 🐛 Find a Bug?  
If you found any issues or want to contribute improvements, feel free to:  
📧 **Contact:** [asad_babayev@outlook.com](mailto:asad_babayev@outlook.com)  
📌 **Submit an Issue:** Use the **Issues** tab above.  
🔄 **Contribute a PR:** If submitting a fix, please reference the related issue.
