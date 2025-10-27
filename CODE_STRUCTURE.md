# 📁 Code Structure

```
VoucherManagementSystem/
├── API/
│   ├── Controllers/
│   │   ├── BaseController.cs
│   │   ├── UsersController.cs
│   │   ├── PromotionsController.cs
│   │   ├── VouchersController.cs
│   │   └── SearchController.cs
│   └── Middleware/
│       └── GlobalExceptionHandlerMiddleware.cs
│
├── Application/
│   ├── Users/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Validators/
│   │   └── DTOs/
│   ├── Promotions/
│   ├── Vouchers/
│   ├── Common/
│   │   ├── Behaviors/
│   │   ├── Mappers/
│   │   ├── Constants/
│   │   └── ServiceCollections.cs
│   └── Services/
│       ├── IUserService.cs
│       ├── UserService.cs
│       ├── IPromotionService.cs
│       ├── PromotionService.cs
│       ├── IVoucherService.cs
│       └── VoucherService.cs
│
├── Domain/
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   ├── User.cs
│   │   ├── Promotion.cs
│   │   └── Voucher.cs
│   └── Interfaces/
│       ├── IRepository.cs
│       ├── IUserRepository.cs
│       ├── IPromotionRepository.cs
│       └── IVoucherRepository.cs
│
└── Infrastructure/
    ├── Repositories/
    │   ├── BaseRepository.cs
    │   ├── MongoRepository.cs
    │   ├── UserRepository.cs
    │   ├── PromotionRepository.cs
    │   └── VoucherRepository.cs
    └── ServiceCollections.cs
```
