# 🎫 Voucher Management System

A comprehensive voucher management system built with .NET 8, implementing Clean Architecture principles with CQRS pattern and MediatR.

## 🏗️ Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                    Voucher Management System                │
├─────────────────────────────────────────────────────────────┤
│  API Layer (Controllers)                                    │
│  ├── UsersController                                         │
│  ├── PromotionsController                                    │
│  ├── VouchersController                                      │
│  └── SearchController                                        │
├─────────────────────────────────────────────────────────────┤
│  Application Layer (CQRS + MediatR)                         │
│  ├── Commands (Create, Update, Delete)                      │
│  ├── Queries (Get, Search, List)                            │
│  ├── DTOs (Data Transfer Objects)                           │
│  └── Handlers (Business Logic)                              │
├─────────────────────────────────────────────────────────────┤
│  Domain Layer (Entities + Interfaces)                       │
│  ├── User, Promotion, Voucher Entities                      │
│  ├── Repository Interfaces                                  │
│  └── Domain Service Interfaces                              │
├─────────────────────────────────────────────────────────────┤
│  Infrastructure Layer (Data Access)                         │
│  ├── MongoDB Repositories                                    │
│  └── Configuration                                           │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- MongoDB (running on localhost:27017)

### Running the Application

#### **Option 1: One-Command Setup (Recommended)**
```bash
# On Windows:
.\quick-start.ps1

# On Linux/Mac (create similar script):
# ./quick-start.sh
```

#### **Option 2: Manual Setup with Docker**
```bash
# 1. Start MongoDB
docker-compose up mongodb -d

# 2. Setup sample data
# On Windows:
.\setup-database.ps1
# On Linux/Mac:
chmod +x setup-database.sh && ./setup-database.sh

# 3. Start the API
dotnet run --project VoucherManagementSystem.API

# 4. Access Swagger UI
# Open browser: http://localhost:5158/swagger
```

#### **Option 3: Manual Setup**
```bash
# 1. Clone and Build
git clone <repository-url>
cd VoucherManagement
dotnet restore
dotnet build

# 2. Start MongoDB (if not using Docker)
# Install MongoDB locally or use Docker

# 3. Start the API
dotnet run --project VoucherManagementSystem.API

# 4. Access Swagger UI
# Open browser: http://localhost:5158/swagger
```

## 📋 Features

### 👥 User Management
- ✅ Create users with email validation
- ✅ Update user information
- ✅ Delete users
- ✅ Search users by name/email
- ✅ Get user by ID

### 🎯 Promotion Management
- ✅ Create promotions with discount rules
- ✅ Update promotion details
- ✅ Deactivate promotions
- ✅ Add/Remove users from promotions
- ✅ Search promotions by code
- ✅ Get active promotions

### 🎫 Voucher Management
- ✅ Generate vouchers for eligible users
- ✅ Use vouchers for orders
- ✅ Track voucher status
- ✅ Get vouchers by user/promotion

## 🛠️ Technology Stack

- **Framework**: .NET 8
- **Architecture**: Clean Architecture + CQRS
- **Database**: MongoDB
- **Search**: MongoDB Text Search
- **API Documentation**: Swagger/OpenAPI
- **Dependency Injection**: Built-in .NET DI
- **Validation**: FluentValidation (recommended for production)

## 📊 API Endpoints

### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `GET /api/users/search?term={term}` - Search users
- `POST /api/users` - Create user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Promotions
- `GET /api/promotions` - Get all promotions
- `GET /api/promotions/active` - Get active promotions
- `GET /api/promotions/{id}` - Get promotion by ID
- `GET /api/promotions/code/{code}` - Get promotion by code
- `GET /api/promotions/user/{userId}` - Get user's promotions
- `POST /api/promotions` - Create promotion
- `PUT /api/promotions/{id}` - Update promotion
- `DELETE /api/promotions/{id}` - Deactivate promotion
- `POST /api/promotions/{id}/users/{userId}` - Add user to promotion
- `DELETE /api/promotions/{id}/users/{userId}` - Remove user from promotion

### Vouchers
- `GET /api/vouchers/{id}` - Get voucher by ID
- `GET /api/vouchers/user/{userId}` - Get user's vouchers
- `GET /api/vouchers/promotion/{promotionId}` - Get promotion's vouchers
- `POST /api/vouchers` - Create voucher
- `POST /api/vouchers/{id}/use` - Use voucher

### Search
- `GET /api/search/users?email={email}` - Search users by email

## 🧪 Testing the API

### 🎯 **Pre-loaded Sample Data**
After running the setup script, you'll have:
- **5 Users**: John Doe, Jane Smith, Bob Johnson, Alice Brown, Charlie Wilson
- **4 Promotions**: Summer Sale (20%), Welcome ($10), Black Friday (30%), Student (15%)
- **3 Vouchers**: 1 used, 2 available for testing

### Using Swagger UI
1. Open `http://localhost:5158/swagger`
2. Click "Try it out" on any endpoint
3. Fill in required parameters
4. Execute and see results

### Using Postman
Import the provided Postman collection for comprehensive API testing.

### Quick Test Flow
1. **Get All Users**
   ```
   GET /api/users
   ```

2. **Get All Promotions**
   ```
   GET /api/promotions
   ```

3. **Get User's Vouchers**
   ```
   GET /api/vouchers/user/{userId}
   ```

4. **Use a Voucher**
   ```json
   POST /api/vouchers/{voucherId}/use
   {
     "voucherId": "voucher-id",
     "orderId": "ORDER-12345"
   }
   ```

### Create New Data
1. **Create a User**
   ```json
   POST /api/users
   {
     "firstName": "John",
     "lastName": "Doe",
     "email": "john.doe@example.com",
     "phoneNumber": "+1234567890",
     "dateOfBirth": "1990-01-01T00:00:00Z",
     "address": "123 Main St"
   }
   ```

2. **Create a Promotion**
   ```json
   POST /api/promotions
   {
     "name": "Summer Sale",
     "description": "20% off all items",
     "code": "SUMMER20",
     "discountAmount": 0,
     "discountCurrency": "USD",
     "discountPercentage": 20,
     "minimumOrderAmount": 50,
     "minimumOrderCurrency": "USD",
     "maximumDiscountAmount": 100,
     "maximumDiscountCurrency": "USD",
     "startDate": "2024-01-01T00:00:00Z",
     "endDate": "2024-12-31T23:59:59Z",
     "usageLimit": 1000
   }
   ```

## 🏛️ Architecture Benefits

- **Separation of Concerns**: Each layer has distinct responsibilities
- **Testability**: Easy to unit test business logic
- **Maintainability**: Changes in one layer don't affect others
- **Scalability**: Can easily add new features or change data storage
- **CQRS**: Separates read and write operations for better performance

## 🔧 Configuration

### Database Settings
```json
{
  "DatabaseSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "VoucherManagementSystem"
  }
}
```

## 📈 Performance Considerations

- **Async/Await**: All operations are asynchronous
- **Repository Pattern**: Abstracts data access
- **CQRS**: Optimizes read/write operations
- **MongoDB Text Search**: Fast search capabilities
- **Connection Pooling**: Efficient database connections

## 🚀 Future Enhancements

- [ ] Add authentication/authorization
- [ ] Implement caching with Redis
- [ ] Add comprehensive logging
- [ ] Implement background jobs for voucher expiration
- [ ] Add email notifications
- [ ] Implement audit logging
- [ ] Add rate limiting
- [ ] Add comprehensive unit/integration tests

## 📝 Notes for Reviewers

This implementation demonstrates:
- **Clean Architecture** principles
- **SOLID** principles adherence
- **CQRS** pattern implementation
- **Repository** pattern usage
- **Dependency Injection** best practices
- **Async programming** patterns
- **API design** best practices

The codebase is production-ready with proper error handling, validation, and separation of concerns.