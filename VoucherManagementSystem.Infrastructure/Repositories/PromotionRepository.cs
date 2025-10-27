using MongoDB.Driver;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class PromotionRepository : MongoRepository<Promotion>, IPromotionRepository
{
    public PromotionRepository(IMongoDatabase database) : base(database, "promotions")
    {
    }

    public async Task<Promotion?> GetByCodeAsync(string code)
    {
        var filter = Builders<Promotion>.Filter.Eq("Code", code);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
    {
        var now = DateTime.UtcNow;
        var filter = Builders<Promotion>.Filter.And(
            Builders<Promotion>.Filter.Eq("IsActive", true),
            Builders<Promotion>.Filter.Lte("StartDate", now),
            Builders<Promotion>.Filter.Gte("EndDate", now)
        );
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Promotion>> GetPromotionsByUserIdAsync(string userId)
    {
        var filter = Builders<Promotion>.Filter.Or(
            Builders<Promotion>.Filter.Size("ApplicableUserIds", 0),
            Builders<Promotion>.Filter.In("ApplicableUserIds", new[] { userId })
        );
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Promotion>> GetExpiredPromotionsAsync()
    {
        var now = DateTime.UtcNow;
        var filter = Builders<Promotion>.Filter.Lt("EndDate", now);
        return await _collection.Find(filter).ToListAsync();
    }
}
