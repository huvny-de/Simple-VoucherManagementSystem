using MongoDB.Driver;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoDatabase database) : base(database, "users")
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var filter = CombineWithSoftDelete(Builders<User>.Filter.Eq("Email", email));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        var filter = CombineWithSoftDelete(Builders<User>.Filter.Eq("IsActive", true));
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
    {
        var searchFilter = Builders<User>.Filter.Or(
            Builders<User>.Filter.Regex("FirstName", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<User>.Filter.Regex("LastName", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<User>.Filter.Regex("Email", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<User>.Filter.Regex("PhoneNumber", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
        );
        var filter = CombineWithSoftDelete(searchFilter);
        return await _collection.Find(filter).ToListAsync();
    }
}
