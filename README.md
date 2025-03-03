## Library Management System Project 📚📖🧍‍♂️🧍🏻‍♀️🏛️📈

About Project
The Library Management System is a RESTful API built with .NET that manages libraries, people, and books efficiently. It supports full CRUD operations for all classes.
The system is built following the Clean Architecture pattern and uses MediatR for handling commands and queries.

Features

Library Management

Both libraries and people can own books.
Books can be added and removed from both entities.

Soft Delete Implementation
Ensures data security by marking records as deleted instead of permanently removing them.

Entity Relationships
Many-to-Many Relationship between People and Books, and between Libraries and Books.

Authentication & Authorization
Uses JWT Tokens to secure endpoints.
Supports Roles for access control.

Caching Mechanism
Improves performance by caching frequently accessed data.

Database Transactions
Ensures data integrity with proper transaction management.

Database Migrations
Supports version-controlled schema changes using Entity Framework Core Migrations.


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

## Find a bug?  
If you found any problem or would like to improve this project, please feel free to reach out to me at **asad_babayev@outlook.com** or submit an issue using the issues tab above. If you would like to submit a PR with a fix, please reference the issue you are addressing.
