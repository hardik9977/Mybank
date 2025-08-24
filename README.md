# MyBank API

A comprehensive banking system API built with .NET 6, providing secure user authentication, account management, transaction processing, and fund transfers.

## 🏦 Features

### Authentication & Security
- **JWT-based Authentication** with secure token management
- **Password Hashing** using BCrypt for enhanced security
- **Login Logging** for audit trails
- **Cookie-based Token Storage** for seamless user experience

### Account Management
- **Multiple Account Types**: Savings and Checking accounts
- **Account Status Tracking**: Active/Inactive status management
- **Currency Support**: Multi-currency support (default: INR)
- **Unique Account Numbers**: Auto-generated unique account identifiers

### Banking Operations
- **Transaction Processing**: Complete transaction history and management
- **Fund Transfers**: Secure inter-account and inter-bank transfers
- **Beneficiary Management**: Add and manage transfer beneficiaries
- **Balance Tracking**: Real-time balance updates and monitoring

## 🛠️ Technology Stack

- **Framework**: .NET 6
- **Database**: MySQL with Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Password Security**: BCrypt.Net-Next
- **API Documentation**: Swagger/OpenAPI
- **Database Provider**: Pomelo.EntityFrameworkCore.MySql

## 📁 Project Structure

```
API/
├── Controllers/          # API endpoints
│   ├── AuthController.cs
│   ├── AccountController.cs
│   ├── TransactionController.cs
│   ├── TransferController.cs
│   └── BeneficiaryController.cs
├── Entity/              # Database models
│   ├── User.cs
│   ├── Account.cs
│   ├── Transaction.cs
│   ├── Transfer.cs
│   ├── Beneficiary.cs
│   └── LoginLogs.cs
├── Repository/          # Data access layer
├── Services/           # Business logic services
├── Dtos/              # Data transfer objects
├── Context/           # Database context
├── Migrations/        # Entity Framework migrations
└── Properties/        # Project properties
```

## 🚀 Getting Started

### Prerequisites

- .NET 6 SDK
- MySQL Server
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd API
   ```

2. **Configure Database**
   - Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=bank;User=root;Password=your_password;"
   }
   ```

3. **Configure JWT Settings**
   - Update JWT configuration in `appsettings.json`:
   ```json
   "JwtSettings": {
     "SecretKey": "your-secret-key-here",
     "Issuer": "https://api.yourdomain.com",
     "Audience": "YourAppUsers",
     "ExpiryMinutes": 60
   }
   ```

4. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access Swagger Documentation**
   - Navigate to `https://localhost:7001/swagger` (or your configured port)

## 📋 API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout

### Account Management
- `GET /api/account` - Get user accounts
- `POST /api/account` - Create new account
- `GET /api/account/{id}` - Get specific account details

### Transactions
- `GET /api/transaction` - Get transaction history
- `POST /api/transaction` - Create new transaction

### Transfers
- `POST /api/transfer` - Initiate fund transfer
- `GET /api/transfer` - Get transfer history

### Beneficiaries
- `GET /api/beneficiary` - Get user beneficiaries
- `POST /api/beneficiary` - Add new beneficiary
- `DELETE /api/beneficiary/{id}` - Remove beneficiary

## 🔐 Security Features

- **JWT Authentication**: Secure token-based authentication
- **Password Hashing**: BCrypt password encryption
- **Input Validation**: Comprehensive data validation
- **SQL Injection Protection**: Entity Framework parameterized queries
- **CORS Configuration**: Configurable cross-origin resource sharing

## 🗄️ Database Schema

### Core Entities
- **User**: Customer information and authentication
- **Account**: Bank accounts with balance and status
- **Transaction**: Financial transaction records
- **Transfer**: Inter-account transfer operations
- **Beneficiary**: Transfer recipient management
- **LoginLogs**: Authentication audit trail

## 🔧 Configuration

### Environment Variables
- `ConnectionStrings:DefaultConnection` - Database connection string
- `JwtSettings:SecretKey` - JWT signing key
- `JwtSettings:Issuer` - JWT issuer
- `JwtSettings:Audience` - JWT audience
- `JwtSettings:ExpiryMinutes` - Token expiration time

## 📝 Development

### Adding New Features
1. Create entity models in the `Entity/` folder
2. Add corresponding DTOs in the `Dtos/` folder
3. Implement repository pattern in the `Repository/` folder
4. Create business logic in the `Services/` folder
5. Add API endpoints in the `Controllers/` folder
6. Update database with migrations

### Code Style
- Follow C# naming conventions
- Use async/await for database operations
- Implement proper error handling
- Add XML documentation for public APIs

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions, please contact the development team or create an issue in the repository.