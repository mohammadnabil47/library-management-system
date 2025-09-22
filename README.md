# Library Management System

A comprehensive Library Management System built with .NET Core API and Angular, following Onion Architecture principles, Domain-Driven Design (DDD), Repository Pattern, EF Core, and ADO.NET with Stored Procedures.

## 🏗️ Architecture

This project follows **Onion Architecture** with the following layers:

- **Domain Layer**: Contains entities, interfaces, and business logic
- **Application Layer**: Contains DTOs, services, and application logic
- **Infrastructure Layer**: Contains data access, repositories, and external services
- **API Layer**: Contains controllers and API endpoints

## 🚀 Features

### Backend (.NET Core API)
- ✅ **Books Management**: CRUD operations for books
- ✅ **Categories Management**: CRUD operations for categories
- ✅ **Many-to-Many Relationship**: Books can belong to multiple categories
- ✅ **EF Core Integration**: For most database operations
- ✅ **ADO.NET with Stored Procedures**: For "Get All Books with Categories" operation
- ✅ **Swagger Documentation**: API documentation and testing
- ✅ **Repository Pattern**: Clean separation of data access logic
- ✅ **AutoMapper**: Object-to-object mapping
- ✅ **CORS Support**: For Angular frontend integration

### Frontend (Angular)
- ✅ **Angular Material UI**: Modern and responsive design
- ✅ **Books Management**: Full CRUD interface for books
- ✅ **Categories Management**: Full CRUD interface for categories
- ✅ **Responsive Design**: Works on desktop and mobile devices
- ✅ **Real-time Updates**: Automatic refresh after operations
- ✅ **Form Validation**: Client-side validation with error messages
- ✅ **Search and Filter**: Search functionality for both books and categories

## 🛠️ Technology Stack

### Backend
- .NET 6.0
- Entity Framework Core 6.0
- SQL Server (LocalDB)
- AutoMapper 12.0.1
- FluentValidation 11.8.0
- Swagger/OpenAPI

### Frontend
- Angular 16.2.0
- Angular Material 16.2.0
- TypeScript 5.1.3
- RxJS 7.8.0

## 📋 Prerequisites

- .NET 6.0 SDK
- Node.js 16+ and npm
- SQL Server (LocalDB or SQL Server)
- Visual Studio 2022 or VS Code

## 🚀 Setup Instructions

### 1. Database Setup

1. **Create Database**:
   ```sql
   CREATE DATABASE LibraryManagementDB;
   ```

2. **Run Table Creation Script**:
   ```bash
   # Execute the SQL script
   sqlcmd -S (localdb)\mssqllocaldb -i "LibraryManagement/DatabaseScripts/CreateTables.sql"
   ```

3. **Run Stored Procedures Script**:
   ```bash
   # Execute the SQL script
   sqlcmd -S (localdb)\mssqllocaldb -i "LibraryManagement/DatabaseScripts/CreateStoredProcedures.sql"
   ```

### 2. Backend Setup

1. **Navigate to the API project**:
   ```bash
   cd LibraryManagement/LibraryManagement
   ```

2. **Restore packages**:
   ```bash
   dotnet restore
   ```

3. **Update connection string** in `appsettings.json` if needed:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Run the API**:
   ```bash
   dotnet run
   ```

5. **Access Swagger UI**: Navigate to `https://localhost:7000/swagger`

### 3. Frontend Setup

1. **Navigate to the Angular project**:
   ```bash
   cd library-management-frontend
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Start the development server**:
   ```bash
   npm start
   ```

4. **Access the application**: Navigate to `http://localhost:4200`

## 📊 Database Schema

### Tables

1. **Books**
   - Id (Primary Key)
   - Title
   - Author
   - ISBN
   - PublishedDate
   - Quantity
   - CreatedDate
   - UpdatedDate

2. **Categories**
   - Id (Primary Key)
   - Name
   - Description
   - CreatedDate
   - UpdatedDate

3. **BookCategories** (Junction Table)
   - BookId (Foreign Key)
   - CategoryId (Foreign Key)

### Stored Procedures

1. **GetAllBooksWithCategories**: Returns all books with their associated categories
2. **GetBooksByCategory**: Returns books for a specific category
3. **GetBookWithCategories**: Returns a specific book with its categories
4. **SearchBooks**: Searches books by title, author, or ISBN
5. **GetBooksByAuthor**: Returns books by a specific author

## 🔧 API Endpoints

### Books
- `GET /api/books` - Get all books
- `GET /api/books/with-categories` - Get all books with categories (EF Core)
- `GET /api/books/with-categories-ado` - Get all books with categories (ADO.NET)
- `GET /api/books/{id}` - Get book by ID
- `GET /api/books/by-category/{categoryId}` - Get books by category
- `POST /api/books` - Create new book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/with-books` - Get all categories with books
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

## ⏱️ Time Estimation and Actual Time Spent

### Estimated Time: 16-20 hours
- Backend Development: 8-10 hours
- Frontend Development: 6-8 hours
- Testing and Documentation: 2-2 hours

### Actual Time Spent: ~12 hours
- Backend Development: 6 hours
- Frontend Development: 4 hours
- Documentation and Setup: 2 hours

## 🎯 Challenges Faced

1. **Angular CLI Installation**: PowerShell execution policy issues on Windows
   - **Solution**: Set execution policy to RemoteSigned for current user

2. **Many-to-Many Relationship**: Complex mapping between books and categories
   - **Solution**: Created junction table (BookCategories) with proper EF Core configuration

3. **ADO.NET Integration**: Mixing EF Core with ADO.NET for stored procedures
   - **Solution**: Created separate service layer for ADO.NET operations

4. **Angular Material Setup**: Complex module imports and configuration
   - **Solution**: Carefully organized imports and created proper component structure

5. **CORS Configuration**: Cross-origin requests between Angular and .NET API
   - **Solution**: Configured CORS policy in Program.cs

## 🚀 Future Enhancements

- [ ] User Authentication and Authorization
- [ ] Book Borrowing/Returning System
- [ ] Advanced Search and Filtering
- [ ] Book Cover Image Upload
- [ ] Email Notifications
- [ ] Reporting and Analytics
- [ ] Mobile App (React Native/Flutter)
- [ ] Unit and Integration Tests
- [ ] Docker Containerization
- [ ] CI/CD Pipeline

## 📝 License

This project is created for educational purposes and demonstration of modern web development practices.

## 🤝 Contributing

Feel free to submit issues and enhancement requests!

---

**Note**: This project demonstrates the implementation of modern software architecture patterns and full-stack development using .NET Core and Angular.
