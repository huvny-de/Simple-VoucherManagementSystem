# setup-database.ps1 - PowerShell script to setup MongoDB with sample data

Write-Host "🗄️ Setting up Voucher Management System Database..." -ForegroundColor Cyan

# Wait for MongoDB to be ready
Write-Host "⏳ Waiting for MongoDB to be ready..." -ForegroundColor Yellow
do {
    try {
        $result = docker exec voucher-mongodb mongosh --eval "db.runCommand('ping').ok" 2>$null
        if ($result -match "1") {
            break
        }
    }
    catch {
        # MongoDB not ready yet
    }
    Write-Host "   MongoDB is not ready yet, waiting..." -ForegroundColor Gray
    Start-Sleep 2
} while ($true)

Write-Host "✅ MongoDB is ready!" -ForegroundColor Green

# Create database and collections with sample data
Write-Host "📊 Creating database and sample data..." -ForegroundColor Cyan

$mongoScript = @"
use VoucherManagementSystem;

// Clear existing data
db.users.deleteMany({});
db.promotions.deleteMany({});
db.vouchers.deleteMany({});

print('🗑️ Cleared existing data');

// Insert sample users
const users = [
  {
    firstName: 'John',
    lastName: 'Doe',
    email: { value: 'john.doe@example.com' },
    phoneNumber: '+1234567890',
    dateOfBirth: new Date('1990-01-01'),
    address: '123 Main St, New York',
    isActive: true,
    createdAt: new Date(),
    isDeleted: false
  },
  {
    firstName: 'Jane',
    lastName: 'Smith',
    email: { value: 'jane.smith@example.com' },
    phoneNumber: '+1234567891',
    dateOfBirth: new Date('1985-05-15'),
    address: '456 Oak Ave, Los Angeles',
    isActive: true,
    createdAt: new Date(),
    isDeleted: false
  },
  {
    firstName: 'Bob',
    lastName: 'Johnson',
    email: { value: 'bob.johnson@example.com' },
    phoneNumber: '+1234567892',
    dateOfBirth: new Date('1992-08-20'),
    address: '789 Pine Rd, Chicago',
    isActive: true,
    createdAt: new Date(),
    isDeleted: false
  },
  {
    firstName: 'Alice',
    lastName: 'Brown',
    email: { value: 'alice.brown@example.com' },
    phoneNumber: '+1234567893',
    dateOfBirth: new Date('1988-12-10'),
    address: '321 Elm St, Boston',
    isActive: true,
    createdAt: new Date(),
    isDeleted: false
  },
  {
    firstName: 'Charlie',
    lastName: 'Wilson',
    email: { value: 'charlie.wilson@example.com' },
    phoneNumber: '+1234567894',
    dateOfBirth: new Date('1995-03-25'),
    address: '654 Maple Dr, Seattle',
    isActive: true,
    createdAt: new Date(),
    isDeleted: false
  }
];

const insertedUsers = db.users.insertMany(users);
print('👥 Inserted ' + insertedUsers.insertedCount + ' users');

// Insert sample promotions
const promotions = [
  {
    name: 'Summer Sale',
    description: '20% off all items',
    code: 'SUMMER20',
    discountAmount: { amount: 0, currency: 'USD' },
    discountPercentage: 20,
    minimumOrderAmount: { amount: 50, currency: 'USD' },
    maximumDiscountAmount: { amount: 100, currency: 'USD' },
    startDate: new Date('2024-01-01'),
    endDate: new Date('2024-12-31'),
    usageLimit: 1000,
    usedCount: 0,
    isActive: true,
    applicableUserIds: [],
    createdAt: new Date(),
    isDeleted: false
  },
  {
    name: 'New Customer Welcome',
    description: '$10 off first order',
    code: 'WELCOME10',
    discountAmount: { amount: 10, currency: 'USD' },
    discountPercentage: 0,
    minimumOrderAmount: { amount: 25, currency: 'USD' },
    maximumDiscountAmount: { amount: 10, currency: 'USD' },
    startDate: new Date('2024-01-01'),
    endDate: new Date('2024-12-31'),
    usageLimit: 500,
    usedCount: 0,
    isActive: true,
    applicableUserIds: [],
    createdAt: new Date(),
    isDeleted: false
  },
  {
    name: 'Black Friday Special',
    description: '30% off everything',
    code: 'BLACKFRIDAY30',
    discountAmount: { amount: 0, currency: 'USD' },
    discountPercentage: 30,
    minimumOrderAmount: { amount: 100, currency: 'USD' },
    maximumDiscountAmount: { amount: 200, currency: 'USD' },
    startDate: new Date('2024-11-24'),
    endDate: new Date('2024-11-30'),
    usageLimit: 2000,
    usedCount: 0,
    isActive: true,
    applicableUserIds: [],
    createdAt: new Date(),
    isDeleted: false
  },
  {
    name: 'Student Discount',
    description: '15% off for students',
    code: 'STUDENT15',
    discountAmount: { amount: 0, currency: 'USD' },
    discountPercentage: 15,
    minimumOrderAmount: { amount: 30, currency: 'USD' },
    maximumDiscountAmount: { amount: 50, currency: 'USD' },
    startDate: new Date('2024-01-01'),
    endDate: new Date('2024-12-31'),
    usageLimit: 300,
    usedCount: 0,
    isActive: true,
    applicableUserIds: [],
    createdAt: new Date(),
    isDeleted: false
  }
];

const insertedPromotions = db.promotions.insertMany(promotions);
print('🎯 Inserted ' + insertedPromotions.insertedCount + ' promotions');

// Add some users to promotions
const userList = db.users.find({}).toArray();
const promotionList = db.promotions.find({}).toArray();

// Add first 3 users to Summer Sale promotion
db.promotions.updateOne(
  { code: 'SUMMER20' },
  { `$set: { applicableUserIds: userList.slice(0, 3).map(u => u._id.toString()) } }
);

// Add first 2 users to Welcome promotion
db.promotions.updateOne(
  { code: 'WELCOME10' },
  { `$set: { applicableUserIds: userList.slice(0, 2).map(u => u._id.toString()) } }
);

// Add all users to Black Friday promotion
db.promotions.updateOne(
  { code: 'BLACKFRIDAY30' },
  { `$set: { applicableUserIds: userList.map(u => u._id.toString()) } }
);

print('🔗 Added users to promotions');

// Create some sample vouchers
const vouchers = [
  {
    userId: userList[0]._id.toString(),
    promotionId: promotionList[0]._id.toString(),
    code: 'VOUCHER-SUMMER-JOHN-001',
    discountAmount: { amount: 0, currency: 'USD' },
    orderAmount: { amount: 100, currency: 'USD' },
    finalDiscountAmount: { amount: 20, currency: 'USD' },
    usedAt: new Date(),
    isUsed: true,
    orderId: 'ORDER-2024-001',
    createdAt: new Date(),
    isDeleted: false
  },
  {
    userId: userList[1]._id.toString(),
    promotionId: promotionList[0]._id.toString(),
    code: 'VOUCHER-SUMMER-JANE-002',
    discountAmount: { amount: 0, currency: 'USD' },
    orderAmount: { amount: 80, currency: 'USD' },
    finalDiscountAmount: { amount: 16, currency: 'USD' },
    usedAt: null,
    isUsed: false,
    orderId: '',
    createdAt: new Date(),
    isDeleted: false
  },
  {
    userId: userList[0]._id.toString(),
    promotionId: promotionList[1]._id.toString(),
    code: 'VOUCHER-WELCOME-JOHN-003',
    discountAmount: { amount: 10, currency: 'USD' },
    orderAmount: { amount: 50, currency: 'USD' },
    finalDiscountAmount: { amount: 10, currency: 'USD' },
    usedAt: null,
    isUsed: false,
    orderId: '',
    createdAt: new Date(),
    isDeleted: false
  }
];

const insertedVouchers = db.vouchers.insertMany(vouchers);
print('🎫 Inserted ' + insertedVouchers.insertedCount + ' vouchers');

// Create indexes for better performance
print('🔍 Creating indexes...');

// Users indexes
db.users.createIndex({ 'email.value': 1 }, { unique: true });
db.users.createIndex({ 'isActive': 1 });
db.users.createIndex({ 'createdAt': -1 });

// Promotions indexes
db.promotions.createIndex({ 'code': 1 }, { unique: true });
db.promotions.createIndex({ 'isActive': 1 });
db.promotions.createIndex({ 'startDate': 1, 'endDate': 1 });
db.promotions.createIndex({ 'applicableUserIds': 1 });

// Vouchers indexes
db.vouchers.createIndex({ 'userId': 1 });
db.vouchers.createIndex({ 'promotionId': 1 });
db.vouchers.createIndex({ 'code': 1 }, { unique: true });
db.vouchers.createIndex({ 'isUsed': 1 });
db.vouchers.createIndex({ 'userId': 1, 'isUsed': 1 });

print('✅ Indexes created successfully!');

// Display summary
print('\\n📊 Database Setup Summary:');
print('👥 Users: ' + db.users.countDocuments());
print('🎯 Promotions: ' + db.promotions.countDocuments());
print('🎫 Vouchers: ' + db.vouchers.countDocuments());
print('\\n🎉 Database setup completed successfully!');
print('\\n🚀 You can now test the API endpoints:');
print('   - GET /api/users');
print('   - GET /api/promotions');
print('   - GET /api/vouchers');
print('   - Swagger UI: http://localhost:5158/swagger');
"@

# Execute the MongoDB script
docker exec voucher-mongodb mongosh --eval $mongoScript

Write-Host ""
Write-Host "✅ Database setup completed!" -ForegroundColor Green
Write-Host ""
Write-Host "🎯 Sample Data Summary:" -ForegroundColor Cyan
Write-Host "   👥 Users: 5 (John, Jane, Bob, Alice, Charlie)" -ForegroundColor White
Write-Host "   🎯 Promotions: 4 (Summer Sale, Welcome, Black Friday, Student)" -ForegroundColor White
Write-Host "   🎫 Vouchers: 3 (1 used, 2 unused)" -ForegroundColor White
Write-Host ""
Write-Host "🚀 Ready to test! Open http://localhost:5158/swagger" -ForegroundColor Yellow
