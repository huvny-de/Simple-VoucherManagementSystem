db.auth('admin', 'password123');

use VoucherManagementSystem;

try { db.users.dropIndex('email_1'); } catch(e) {}
try { db.users.dropIndex('email.value_1'); } catch(e) {}
try { db.users.dropIndex('Email.Value_1'); } catch(e) {}
try { db.promotions.dropIndex('code_1'); } catch(e) {}
try { db.vouchers.dropIndex('code_1'); } catch(e) {}

db.users.deleteMany({});
db.promotions.deleteMany({});
db.vouchers.deleteMany({});

print('Cleared existing data and old indexes');

const users = [
  { firstName: 'John', lastName: 'Doe', email: 'john.doe@example.com', phoneNumber: '+1234567890', dateOfBirth: new Date('1990-01-01'), address: '123 Main St, New York', isActive: true, createdAt: new Date(), isDeleted: false },
  { firstName: 'Jane', lastName: 'Smith', email: 'jane.smith@example.com', phoneNumber: '+1234567891', dateOfBirth: new Date('1985-05-15'), address: '456 Oak Ave, Los Angeles', isActive: true, createdAt: new Date(), isDeleted: false },
  { firstName: 'Bob', lastName: 'Johnson', email: 'bob.johnson@example.com', phoneNumber: '+1234567892', dateOfBirth: new Date('1992-08-20'), address: '789 Pine Rd, Chicago', isActive: true, createdAt: new Date(), isDeleted: false },
  { firstName: 'Alice', lastName: 'Brown', email: 'alice.brown@example.com', phoneNumber: '+1234567893', dateOfBirth: new Date('1988-12-10'), address: '321 Elm St, Boston', isActive: true, createdAt: new Date(), isDeleted: false },
  { firstName: 'Charlie', lastName: 'Wilson', email: 'charlie.wilson@example.com', phoneNumber: '+1234567894', dateOfBirth: new Date('1995-03-25'), address: '654 Maple Dr, Seattle', isActive: true, createdAt: new Date(), isDeleted: false }
];

const insertedUsers = db.users.insertMany(users);
print('Inserted ' + insertedUsers.insertedCount + ' users');

const promotions = [
  { name: 'Summer Sale', description: '20% off all items', code: 'SUMMER20', discountAmount: 0, discountCurrency: 'USD', discountPercentage: 20, minimumOrderAmount: 50, minimumOrderCurrency: 'USD', maximumDiscountAmount: 100, maximumDiscountCurrency: 'USD', startDate: new Date('2024-01-01'), endDate: new Date('2024-12-31'), usageLimit: 1000, usedCount: 0, isActive: true, applicableUserIds: [], createdAt: new Date(), isDeleted: false },
  { name: 'New Customer Welcome', description: '$10 off first order', code: 'WELCOME10', discountAmount: 10, discountCurrency: 'USD', discountPercentage: 0, minimumOrderAmount: 25, minimumOrderCurrency: 'USD', maximumDiscountAmount: 10, maximumDiscountCurrency: 'USD', startDate: new Date('2024-01-01'), endDate: new Date('2024-12-31'), usageLimit: 500, usedCount: 0, isActive: true, applicableUserIds: [], createdAt: new Date(), isDeleted: false },
  { name: 'Black Friday Special', description: '30% off everything', code: 'BLACKFRIDAY30', discountAmount: 0, discountCurrency: 'USD', discountPercentage: 30, minimumOrderAmount: 100, minimumOrderCurrency: 'USD', maximumDiscountAmount: 200, maximumDiscountCurrency: 'USD', startDate: new Date('2024-11-24'), endDate: new Date('2024-11-30'), usageLimit: 2000, usedCount: 0, isActive: true, applicableUserIds: [], createdAt: new Date(), isDeleted: false }
];

const insertedPromotions = db.promotions.insertMany(promotions);
print('Inserted ' + insertedPromotions.insertedCount + ' promotions');

const userList = db.users.find().toArray();
const promotionList = db.promotions.find().toArray();

const vouchers = [
  { userId: userList[0]._id.toString(), promotionId: promotionList[0]._id.toString(), code: 'VOUCHER-SUMMER-JOHN-001', discountAmount: 0, discountCurrency: 'USD', orderAmount: 100, orderCurrency: 'USD', finalDiscountAmount: 20, finalDiscountCurrency: 'USD', usedAt: new Date(), isUsed: true, orderId: 'ORDER-2024-001', createdAt: new Date(), isDeleted: false },
  { userId: userList[1]._id.toString(), promotionId: promotionList[0]._id.toString(), code: 'VOUCHER-SUMMER-JANE-002', discountAmount: 0, discountCurrency: 'USD', orderAmount: 80, orderCurrency: 'USD', finalDiscountAmount: 16, finalDiscountCurrency: 'USD', usedAt: null, isUsed: false, orderId: '', createdAt: new Date(), isDeleted: false },
  { userId: userList[0]._id.toString(), promotionId: promotionList[1]._id.toString(), code: 'VOUCHER-WELCOME-JOHN-003', discountAmount: 10, discountCurrency: 'USD', orderAmount: 50, orderCurrency: 'USD', finalDiscountAmount: 10, finalDiscountCurrency: 'USD', usedAt: null, isUsed: false, orderId: '', createdAt: new Date(), isDeleted: false },
  { userId: userList[2]._id.toString(), promotionId: promotionList[2]._id.toString(), code: 'VOUCHER-BF-BOB-004', discountAmount: 0, discountCurrency: 'USD', orderAmount: 150, orderCurrency: 'USD', finalDiscountAmount: 45, finalDiscountCurrency: 'USD', usedAt: null, isUsed: false, orderId: '', createdAt: new Date(), isDeleted: false }
];

const insertedVouchers = db.vouchers.insertMany(vouchers);
print('Inserted ' + insertedVouchers.insertedCount + ' vouchers');

db.users.createIndex({ 'email': 1 }, { unique: true });
db.promotions.createIndex({ 'code': 1 }, { unique: true });
db.vouchers.createIndex({ 'code': 1 }, { unique: true });

print('Created indexes');

print('Summary: ' + db.users.countDocuments() + ' users, ' + db.promotions.countDocuments() + ' promotions, ' + db.vouchers.countDocuments() + ' vouchers');
